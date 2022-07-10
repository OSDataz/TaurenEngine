/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Core;
using TaurenEngine.LocationEx;
using TaurenEngine.MoveEx;
using UnityEngine;

namespace TaurenEngine.AR
{
	/// <summary>
	/// 管理所有AR对象
	/// </summary>
	public class ARObjectManager : Singleton<ARObjectManager>
	{
		private MoveManager _moveMgr;
		private ARLocation _arLocation;

		public ARObjectManager()
		{
			_moveMgr = MoveManager.Instance;
			_arLocation = ARLocation.Instance;

			_arContainer = new GameObject("ARObjectContainer").transform;
			//_arContainer.position = Vector3.zero;
			_arContainer.eulerAngles = Vector3.zero;

			//DebugUnity.Instance.SetLabelPosition("ARContainer Pos", _arContainer.transform);

			InitGps();
		}

		internal void Startup()
		{
			EventCenter.Dispatcher.AddEvent(ARPosition.EventArReset, ArResetHandler);
			EventCenter.Dispatcher.AddEvent(ARPosition.EventArError, ArErrorHandler);
			EventCenter.Dispatcher.AddEvent(ARPosition.EventArRecovery, ArRecoveryHandler);

			InitGps();
		}

		public void Clear()
		{
			EventCenter.Dispatcher.RemoveEvent(ARPosition.EventArReset, ArResetHandler);
			EventCenter.Dispatcher.RemoveEvent(ARPosition.EventArError, ArErrorHandler);
			EventCenter.Dispatcher.RemoveEvent(ARPosition.EventArRecovery, ArRecoveryHandler);

			_arObjects.ForEach(item => item.Destroy());
			_arObjects.Clear();

			SetState(ARContainerState.Normal);
		}

		#region 全局控制接口
		/// <summary>
		/// AR容器复位
		/// </summary>
		public void Reset()
		{
			if (!_arLocation.IsBind)
				return;

			UpdatePositionAuto();
			SetState(ARContainerState.Reset);
		}

		/// <summary>
		///  AR容器复位Y坐标
		/// </summary>
		public void ResetY()
		{
			if (!_arLocation.IsBind)
				return;

			ResetPositionY();
		}

		/// <summary>
		/// 将AR容器锁定在绑定对象（摄像机）上。
		/// <para>【注意】锁定后要记得解锁！！！</para>
		/// </summary>
		/// <param name="isLock"></param>
		public void Lock(bool isLock)
		{
			if (!_arLocation.IsBind)
				return;

			if (isLock)
			{
				SetState(ARContainerState.Lock);
			}
			else
			{
				SetState(ARContainerState.Normal);
			}
		}
		#endregion

		#region AR对象管理
		private readonly Transform _arContainer;// AR对象容器
		private readonly List<ARObject> _arObjects = new List<ARObject>();

		public ARObject FindObject(Transform transform)
		{
			return _arObjects.Find(item => item.transform == transform);
		}

		private ARObject CreateObject(Transform transform)
		{
			if (transform == null)
				return null;

			var item = FindObject(transform);
			if (item == null)
			{
				item = new ARObject();
				item.transform = transform;
				_arObjects.Add(item);
			}

			transform.SetParent(_arContainer);

			return item;
		}

		public ARObject AddObject(Transform transform)
		{
			var item = CreateObject(transform);
			if (item == null)
				return null;

			item.Location = CalcLocationByAnchor(item.Position);
			return item;
		}

		public ARObject AddObject(Transform transform, Location location)
		{
			var item = CreateObject(transform);
			if (item == null)
				return null;

			item.Location = location;
			item.Position = CalcPositionByAnchor(location);
			return item;
		}

		public ARObject RemoveObject(Transform transform)
		{
			var item = FindObject(transform);
			RemoveObject(item);

			return item;
		}

		public void RemoveObject(ARObject arObject)
		{
			if (arObject == null)
				return;

			_arObjects.Remove(arObject);
			arObject.transform.SetParent(null);
		}
		#endregion

		#region AR定位管理
		/// <summary>
		/// AR摄像机重定位
		/// </summary>
		/// <param name="data"></param>
		private void ArResetHandler(IEventData data)
		{
			if (data is EventData<Vector3> eventData)
			{
				SetState(eventData.Value);
			}
		}

		/// <summary>
		/// AR摄像机异常移动
		/// </summary>
		/// <param name="data"></param>
		private void ArErrorHandler(IEventData data)
		{
			SetState(ARContainerState.Bind);
		}

		/// <summary>
		/// AR摄像机异常恢复
		/// </summary>
		/// <param name="data"></param>
		private void ArRecoveryHandler(IEventData data)
		{
			if (_state == ARContainerState.Bind)
				SetState(ARContainerState.Normal);
		}
		#endregion

		#region GPS管理
		private GpsLocation _gpsLocation;
		private Vector3 _reConPos;

		private void InitGps()
		{
			if (!AREngineSetting.Instance.useLocation)
				return;

			_gpsLocation = _arLocation.GpsLocation;

			if (!_gpsLocation.IsInit)
			{
				EventCenter.Dispatcher.AddEvent(GpsLocation.EventInit, (IEventData data) =>
				{
					TDebug.Log("ARLocation准备完毕，GPS对象初始化位置");
					UpdateAnchor();
				}, true);
			}
			else
			{
				UpdateAnchor();
			}

			EventCenter.Dispatcher.AddEvent(GpsLocation.EventGpsReset, GpsResetHandler);
		}

		/// <summary>
		/// GPS更新，重定位
		/// </summary>
		/// <param name="data"></param>
		private void GpsResetHandler(IEventData data)
		{
			_reConPos = _gpsLocation.ToPositionRoot(_anchorLoc);
			UpdatePositionY();

			CheckGpsReset();
		}

		private void UpdatePositionAuto()
		{
			_reConPos = _gpsLocation.ToPositionAuto(_anchorLoc);
			UpdatePositionY();
		}

		private void UpdatePositionY()
		{
			_reConPos.y = _gpsLocation.BindTransform.position.y;
		}

		private void CheckGpsReset()
		{
			if (_reConPos.Distance2XZ(_arContainer.position) > 225)// 默认使用15米的误差
			{
				SetState(ARContainerState.Reset);
			}
		}

		private void ResetPosition()
		{
			_arContainer.position = _reConPos;
		}

		private void ResetPositionY()
		{
			var pos = _arContainer.position;
			pos.y = _gpsLocation.BindTransform.position.y;
			_arContainer.position = pos;
		}
		#endregion

		#region AR容器定位
		private Location _anchorLoc;

		private void UpdateAnchor()
		{
			_anchorLoc = _gpsLocation.LocationRoot;// 容器只会设置这一次
			_anchorLoc.altitude = 0;
			TDebug.Log($"AR对象容器绑定坐标：{_anchorLoc.ToString()}");

			SetState(ARContainerState.Normal);
			UpdatePositionY();
			ResetPosition();

			_arObjects.ForEach(item =>
			{
				if (item.useGps)
					item.Position = CalcPositionByAnchor(item.Location);
			});
		}

		public Vector3 Position => _arContainer.position;

		/// <summary>
		/// 通过AR容器的锚点计算坐标位置
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		private Vector3 CalcPositionByAnchor(Location location)
		{
			return Location.GetPositionForLocation(Vector3.zero, _anchorLoc, location);
		}

		private Location CalcLocationByAnchor(Vector3 position)
		{
			return Location.GetLocationForPosition(_anchorLoc, Vector3.zero, position);
		}
		#endregion

		#region 状态
		/// <summary>
		/// AR容器状态（只能通过状态控制AR容器）
		/// <para>优先级：Normal = Lock > Bind > Reset </para>
		/// </summary>
		private enum ARContainerState
		{
			Normal,
			Reset,
			Bind,
			Lock
		}

		private ARContainerState _state = ARContainerState.Normal;

		private bool SetState(ARContainerState state)
		{
			if (state == ARContainerState.Reset)
			{
				if (_state == ARContainerState.Normal)// 正常 - 重定位：开启重定位
				{
					TDebug.LogMix("AR容器状态", $"{_state}->{state} 开启重定位");
					_state = state;
					_moveMgr.MoveToUniformSpeed(_arContainer.transform, _reConPos, 1.0f,
						() => { SetState(ARContainerState.Normal);});
					return true;
				}
				else if (_state == ARContainerState.Bind || _state == ARContainerState.Lock)// 绑定 - 重定位：不通过
				{
					TDebug.LogMix("AR容器状态", $"{_state}->{state} 绑定中，不能重定位");
					return false;
				}
				else if (_state == ARContainerState.Reset)// 重定位 - 重定位：重定位更新
				{
					_moveMgr.MoveTo(_arContainer.transform, _reConPos);
					TDebug.LogMix("AR容器状态", $"{_state}->{state} 更新重定位");
					return true;
				}
			}
			else
			{
				if (_state == state)
					return true;

				if (_state == ARContainerState.Reset)
				{
					_moveMgr.CancelMove(_arContainer.transform);
				}

				if (state == ARContainerState.Lock)
				{
					if (_state != ARContainerState.Bind)
					{
						_moveMgr.BindTarget(_arContainer.transform, _arLocation.ArPosition.BindTransform);
					}

					TDebug.LogMix("AR容器状态", $"{_state}->{state} 开始锁定");
					_state = state;
					return true;
				}
				else if (state == ARContainerState.Bind)
				{
					if (_state == ARContainerState.Lock)
						return false;

					// 正常 - 绑定：开始绑定
					// 重定位 - 绑定：停止重定位，开始绑定

					TDebug.LogMix("AR容器状态", $"{_state}->{state} 开始绑定");
					_state = state;
					_moveMgr.BindTarget(_arContainer.transform, _arLocation.ArPosition.BindTransform);
					return true;
				}
				else if (state == ARContainerState.Normal)
				{
					if (_state == ARContainerState.Bind || _state == ARContainerState.Lock)// 绑定 - 正常：停止绑定，检测重定位
					{
						TDebug.LogMix("AR容器状态", $"{_state}->{state} 停止绑定，检测重定位");
						_state = state;
						_moveMgr.CancelTarget(_arContainer.transform);

						UpdatePositionY();
						CheckGpsReset();
						return true;
					}

					// 重定位 - 正常：停止重定位
					TDebug.LogMix("AR容器状态", $"{_state}->{state} 正常");
					_state = state;
					return true;
				}
			}

			TDebug.LogMix("AR容器状态", $"{_state}->{state} 异常");
			return false;
		}

		private void SetState(Vector3 offVec)
		{
			if (_state == ARContainerState.Bind || _state == ARContainerState.Lock)
				return;

			TDebug.LogMix("AR容器状态", $"{_state} 修复位置");

			_arContainer.position += offVec;

			if (_state == ARContainerState.Reset)
			{
				UpdatePositionY();
				SetState(_state);
			}
		}
		#endregion
	}
}