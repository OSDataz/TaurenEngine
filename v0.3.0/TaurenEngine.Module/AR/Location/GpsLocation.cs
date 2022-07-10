/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.Core;
using TaurenEngine.LocationEx;
using UnityEngine;

namespace TaurenEngine.AR
{
	public class GpsLocation
	{
		#region 事件
		/// <summary>
		/// 初始化
		/// </summary>
		public const string EventInit = "GpsLocationn_EventInit";
		/// <summary>
		/// GPS坐标重定位
		/// </summary>
		public const string EventGpsReset = "GpsLocation_EventGpsReset";
		#endregion

		private bool _isStartup;

		internal void TryStartup()
		{
			_isStartup = true;
			_locationManager = LocationManager.Instance;
		}

		internal void Clear()
		{
			_isStartup = false;
			_isInit = false;

			SetBind(null);
		}

		internal bool UpdateReset()
		{
			if (!_isStartup)
				return false;

			if (!_isUpdateLocRootCalc)
				_isUpdateLocRootCalc = true;

			if (_isInit)
			{
				if (IsUpdate)
				{
					var curLoc = Location;
					var curPos = _bindTransform.position;
					if (_resetDistanceSqrtGps > 0)
					{
						var calcPos = ToPositionRoot(curLoc);
						if (calcPos.Distance2(curPos) < _resetDistanceSqrtGps)
							return false;
					}

					// 重定位
					LocationRoot = curLoc;
					PositionRoot = curPos;

					TDebug.Log($"GPS锚点重定位：\nLoc:{LocationRoot.ToString()}\nPos:{PositionRoot}");

					EventCenter.Dispatcher.Send(EventGpsReset);

					return true;
				}
			}
			else
			{
				if (IsReady)
				{
					LocationRoot = Location;
					PositionRoot = _bindTransform.position;

					IsInit = true;
					return true;
				}
			}

			return false;
		}

		internal void Destroy()
		{
			_locationManager = null;
			_bindTransform = null;
		}

		#region 绑定对象
		private Transform _bindTransform;

		internal void SetBind(Transform transform)
		{
			_bindTransform = transform;
		}

		public Transform BindTransform => _bindTransform;
		#endregion

		#region 数据源
		private LocationManager _locationManager;

		/// <summary>
		/// GPS原始定位数据
		/// </summary>
		public Location Location => Application.isEditor ? new Location(116.307472229004, 39.9841537475586, 0, false) : _locationManager.Location;

		public bool IsUpdate => _locationManager.IsUpdate;

		public bool IsReady => Application.isEditor || _locationManager.IsReady;

		public bool IsAvailable => Application.isEditor || _locationManager.IsAvailable;
		#endregion

		#region 初始化
		private bool _isInit = false;

		public bool IsInit
		{
			get => _isInit;
			private set
			{
				if (_isInit == value)
					return;

				_isInit = value;

				if (_isInit)
					EventCenter.Dispatcher.Send(EventInit);
			}
		}
		#endregion

		#region 重定位
		private float _resetDistanceSqrtGps = 25;// 默认5米

		/// <summary>
		/// GPS更新，GPS重定位检测距离
		/// </summary>
		public float ResetDistanceGps
		{
			get => Mathf.Sqrt(_resetDistanceSqrtGps);
			set { _resetDistanceSqrtGps = value * value; }
		}
		#endregion

		#region 锚点
		/// <summary>
		/// 锚点：定位转坐标
		/// </summary>
		/// <param name="loc"></param>
		/// <returns></returns>
		public Vector3 ToPositionRoot(Location loc)
		{
			return Location.GetPositionForLocation(PositionRoot, LocationRoot, loc);
		}

		/// <summary>
		/// 锚点：坐标转定位
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public Location ToLocationRoot(Vector3 pos)
		{
			return Location.GetLocationForPosition(LocationRoot, PositionRoot, pos);
		}

		/// <summary>
		/// 锚点：GPS定位
		/// </summary>
		public Location LocationRoot { get; private set; }

		/// <summary>
		/// 锚点：空间坐标
		/// </summary>
		public Vector3 PositionRoot { get; private set; }
		#endregion

		#region 模拟坐标
		public Location SimLocation = new Location();
		public bool UseSimLocation = false;
		#endregion

		#region 实时计算坐标
		internal bool isMoveCamera = false;
		private bool _isUpdateLocRootCalc = false;
		private Location _lastLocRootCalc;

		/// <summary>
		/// 基于锚点，计算当前绑定对象GPS定位（玩家定位）
		/// </summary>
		public Location LocationRootCalc
		{
			get
			{
				if (!_isInit || !isMoveCamera || _bindTransform == null)
					return Location;

				if (_isUpdateLocRootCalc)
				{
					_lastLocRootCalc = ToLocationRoot(_bindTransform.position);
					_isUpdateLocRootCalc = false;
				}

				return _lastLocRootCalc;
			}
		}

		/// <summary>
		/// 基于锚点，计算当前绑定对象空间坐标
		/// </summary>
		public Vector3 PositionRootCalc => ToPositionRoot(Location);
		#endregion

		#region 综合坐标
		/// <summary>
		/// 根据综合情况（AR引擎）获取定位
		/// </summary>
		/// <returns></returns>
		public Location LocationAuto => UseSimLocation ? SimLocation : LocationRootCalc;

		/// <summary>
		/// 锚点：定位转坐标
		/// </summary>
		/// <param name="loc"></param>
		/// <returns></returns>
		public Vector3 ToPositionAuto(Location loc)
		{
			return Location.GetPositionForLocation(_bindTransform.position, LocationAuto, loc);
		}

		/// <summary>
		/// 锚点：坐标转定位
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		public Location ToLocationAuto(Vector3 pos)
		{
			return Location.GetLocationForPosition(LocationAuto, _bindTransform.position, pos);
		}
		#endregion
	}
}