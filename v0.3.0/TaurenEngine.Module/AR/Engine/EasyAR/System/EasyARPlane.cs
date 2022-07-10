/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

#if EasyAR
using easyar;
#endif
using UnityEngine;

namespace TaurenEngine.AR
{
	/// <summary>
	/// EasyAR 平面识别
	/// </summary>
	internal class EasyARPlane : EasyARSystemBase, ISystemPlane
	{
		public EasyARPlane(EasyAREx easyArEx) : base(easyArEx)
		{
			IsAvailable = true;// 需要先启动，可能第一次不成功，第二次检测才对
		}

		protected override void Init()
		{
#if EasyAR
			IsAvailable = _arEngine.cameraDevice.Device?.Type() == typeof(MotionTrackerCameraDevice);
			//Debugx.Log($"EasyARPlane {_arEngine.cameraDevice.Device?.Type()}");
#endif

			base.Init();
		}

		public bool GetPanelPos(Vector2 screenPoint, ref Pose pose)
		{
#if EasyAR
			Vector3 screenPoint3 = screenPoint;

			screenPoint.x /= Screen.width;
			screenPoint.y /= Screen.height;

			var points = _arEngine.cameraDevice.HitTestAgainstHorizontalPlane(screenPoint);
			if (points.Count > 0)
			{
				var ray = _arEngine.Camera.ScreenPointToRay(screenPoint3);
				var vec = ray.origin + ray.direction * (points[0].y - ray.origin.y) / ray.direction.y;

				pose = new Pose(vec, Quaternion.Euler(Vector3.one * (_arEngine.arAssembly.CameraRoot.position - vec).magnitude));
				return true;
			}
#endif
			return false;
		}
	}
}