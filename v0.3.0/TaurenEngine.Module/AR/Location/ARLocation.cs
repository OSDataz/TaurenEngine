/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.AR
{
	public class ARLocation : SingletonBehaviour<ARLocation>
	{
		public readonly ARPosition ArPosition = new ARPosition();
		public readonly GpsLocation GpsLocation = new GpsLocation();

		void Awake()
		{
			Startup();
		}

		void Update()
		{
			if (_isBind)
			{
				if (!GpsLocation.UpdateReset())
					ArPosition.UpdateReset();
			}
		}

		protected override void OnDestroy()
		{
			if (IsDestroyed)
				return;

			ArPosition.Destroy();
			GpsLocation.Destroy();

			base.OnDestroy();
		}

		internal void Startup()
		{
			if (!AREngineSetting.Instance.useLocation)
				return;

			TryBind();

			GpsLocation.TryStartup();
		}

		internal void Close()
		{
			_isBind = false;
			isMoveCamera = false;

			ArPosition.Clear();
			GpsLocation.Clear();
		}

		#region 绑定对象
		private bool _isBind = false;

		internal void TryBind()
		{
			var proxy = AREngine.Instance.Proxy;
			if (proxy.Status != RunningStatus.Ready)
				return;

#if UNITY_EDITOR
			_isBind = true;
#else
			_isBind = proxy.Status == RunningStatus.Ready;
#endif

			if (_isBind)
			{
#if UNITY_EDITOR
				var bindTransform = Camera.main.transform;
#else
				var bindTransform = proxy.CameraTransform;
#endif

				isMoveCamera = proxy.EngineBase.IsMotionDeviceTracking;

				if (bindTransform)
				{
					ArPosition.SetBind(bindTransform);
					GpsLocation.SetBind(bindTransform);

					TDebug.Log("ARLocation绑定成功");
				}
				else
				{
					_isBind = false;
					TDebug.Error("ARLocation绑定异常，绑定对象未找到");
				}
			}
			else
			{
				TDebug.Log("ARLocation绑定失败");
			}
		}

		public bool IsBind => _isBind;

		private bool isMoveCamera
		{
			set
			{
				ArPosition.isMoveCamera = value;
				GpsLocation.isMoveCamera = value;
			}
		}
		#endregion
	}
}