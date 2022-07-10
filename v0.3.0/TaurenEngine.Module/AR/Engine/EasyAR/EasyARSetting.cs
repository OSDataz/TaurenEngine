/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using System;
#if EasyAR
using easyar;
#endif
using UnityEngine;

namespace TaurenEngine.AR
{
	/// <summary>
	/// EasyAR引擎设置
	/// </summary>
	[Serializable]
	public class EasyARSetting
	{
#if EasyAR
		[Tooltip("选择VIO设备的策略")]
		public VIOCameraDeviceUnion.DeviceChooseStrategy deviceStrategy = VIOCameraDeviceUnion.DeviceChooseStrategy.EasyARMotionTrackerFirst;

		[Tooltip("设备图像帧率是")]
		public MotionTrackerCameraDeviceFPS fps = MotionTrackerCameraDeviceFPS.Camera_FPS_30;
		[Tooltip("对焦模式")]
		public MotionTrackerCameraDeviceFocusMode focusMode = MotionTrackerCameraDeviceFocusMode.Continousauto;
		[Tooltip("标准分辨率")]
		public MotionTrackerCameraDeviceResolution resolution = MotionTrackerCameraDeviceResolution.Resolution_1280;
		[Tooltip("设备追踪模式")]
		public MotionTrackerCameraDeviceTrackingMode trackingMode = MotionTrackerCameraDeviceTrackingMode.SLAM;
		[Tooltip("设备性能等级")]
		public MotionTrackerCameraDeviceQualityLevel minQualityLevel = MotionTrackerCameraDeviceQualityLevel.Good;
#endif
	}
}