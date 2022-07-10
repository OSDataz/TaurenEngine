/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 10:17:42
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.LocationEx
{
	/// <summary>
	/// 高德地图定位，GCJ02坐标系
	/// </summary>
	public class AMapLocationProvider : AndroidJavaProxy, ILocationProvider
	{
		private AndroidJavaObject _locationHandler;

		private bool _isReady = false;
		private bool _isUpdate = false;
		private Location _location = new Location();

		public AMapLocationProvider() : base("com.osdataz.jarlib.amap.IAMapLocation") { }

		public void Awake()
		{
			try
			{
				AndroidJavaClass aMapMgr = new AndroidJavaClass("com.osdataz.jarlib.amap.AMapManager");
				AndroidJavaObject aMapMgrIns = aMapMgr?.GetStatic<AndroidJavaObject>("Instance");
				_locationHandler = aMapMgrIns?.Get<AndroidJavaObject>("LocationHandler");
			}
			catch (Exception e)
			{
				TDebug.Log(e);
			}
		}

		private void StartLocation()
		{
			TDebug.Log("启动高德定位");
			_locationHandler?.Call("startLocation", this);
		}

		private void StopLocation()
		{
			TDebug.Log("停止高德定位");
			_locationHandler?.Call("stopLocation");
		}

		#region IAMapLocation 接口
		/// <summary>
		/// Bind Name
		/// </summary>
		/// <param name="longitude"></param>
		/// <param name="latitude"></param>
		/// <param name="altitude"></param>
		public void SetLocation(double longitude, double latitude, double altitude)
		{
			if (!_isReady)
			{
				_isReady = true;
				TDebug.Log($"高德初始化定位：{longitude.ToString("F4")} {latitude.ToString("F4")} {altitude.ToString("F2")}");
			}

			_isUpdate = true;

			_location.longitude = longitude;
			_location.latitude = latitude;
			_location.altitude = altitude;
		}

		/// <summary>
		/// Bind Name
		/// </summary>
		/// <returns></returns>
		public int GetAMapLocationMode()
		{
			TDebug.Log($"高德定位模式：{LocationManager.Instance.setting.aMapSetting.aMapLocationModel}");
			return (int)LocationManager.Instance.setting.aMapSetting.aMapLocationModel;
		}

		/// <summary>
		/// Bind Name
		/// </summary>
		/// <returns></returns>
		public long GetInterval()
		{
			TDebug.Log($"高德定位间隔时间：{LocationManager.Instance.setting.aMapSetting.interval}ms");
			return LocationManager.Instance.setting.aMapSetting.interval;
		}

		public void UnityLog(string message)
		{
			TDebug.Log(message);
		}
		#endregion

		public void OnEnable()
		{
			StartLocation();
		}

		public void OnDisable()
		{
			StopLocation();
			_isReady = false;
		}

		public void OnDestroy()
		{
			_locationHandler = null;
		}

		public bool GetLocation(ref Location loc)
		{
			if (_isUpdate)
			{
				_isUpdate = false;

				loc.longitude = _location.longitude;
				loc.latitude = _location.latitude;
				loc.altitude = _location.altitude;
				return true;
			}

			return false;
		}

		public LocationSwitch GetLocationSwitch(LocationType locationType)
		{
			return LocationSwitch.None;
		}

		public bool IsReady => _isReady;

		public bool IsAvailable => _locationHandler != null;
	}
}