/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

#if EasyAR
using easyar;
#endif
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.AR
{
    /// <summary>
    /// EasyAR 扩展使用
    /// </summary>
    internal class EasyAREx : AREngineBase
    {
	    public GameObject worldRoot;
#if EasyAR
        public ARSession arSession;
	    public ARAssembly arAssembly;
	    public VIOCameraDeviceUnion cameraDevice;
#endif

	    private EasyARPlane _easyArPlane;
	    private EasyARImage _easyArImage;

        public override void Startup()
        {
            new EasyARComponent().InitComponent(this);

            _easyArPlane = new EasyARPlane(this);
            _easyArImage = new EasyARImage(this);
        }

        public override void Close()
        {
	        new EasyARComponent().Destroy(this);
        }

        public override void OnDestroy()
        {
	        if (IsDestroyed)
		        return;

	        _easyArPlane = null;
	        _easyArImage = null;

            worldRoot = null;

#if EasyAR
	        arSession = null;
	        arAssembly = null;
	        cameraDevice = null;
#endif

            base.OnDestroy();
        }

        public override void ResetDirection()
        {
#if EasyAR
            GameObject oEasyAROrigin = EasyARController.Instance.gameObject;

	        var cCameraWorldDirection = oEasyAROrigin.GetNewComponent<CameraWorldDirection>();
	        cCameraWorldDirection.WorldRoot = worldRoot.transform;
#endif
        }

        public override bool IsAvailable
        {
            get
            {
#if EasyAR
                if (Application.platform == RuntimePlatform.Android)
		            return ARCoreCameraDevice.isAvailable() || MotionTrackerCameraDevice.isAvailable();

                if (Application.platform == RuntimePlatform.IPhonePlayer)
		            return ARKitCameraDevice.isAvailable();
#endif

#if EasyAR && UNITY_EDITOR_WIN
                return MotionTrackerCameraDevice.isAvailable();
#else
                return false;
#endif
            }
        }
        public override AREngineType EngineType => AREngineType.EasyAR;

        public override Camera Camera
        {
	        get
	        {
#if EasyAR
		        return arAssembly.Camera;
#else
		        return null;
#endif
	        }
        }

        public override ISystemPlane SystemPlane => _easyArPlane;
        public override ISystemImage SystemImage => _easyArImage;

        public override bool IsMotionDeviceTracking => true;

#if EasyAR
        public GameObject EasyAROrigin => arSession.gameObject;
#endif
    }

    internal class EasyARComponent : ARComponent
    {
        public void InitComponent(EasyAREx easyArEx)
        {
	        if (easyArEx.isStartup)
		        return;

	        easyArEx.isStartup = true;

            TDebug.Log("Init EasyAR Component");

            var setting = AREngineSetting.Instance.easyArSetting;

            CreateARCamera();
#if EasyAR
            var oWorldRoot = UnityHelper.GetNewGameObject("EasyARWorldRoot");
            var cWorldRootController = oWorldRoot.GetNewComponent<WorldRootController>();
            easyArEx.worldRoot = oWorldRoot;
            
            var oEasyAROrigin = UnityHelper.GetNewGameObject("EasyAROrigin");
            oEasyAROrigin.GetNewComponent<EasyARController>();
            var cARSession = oEasyAROrigin.GetNewComponent<ARSession>();
            cARSession.CenterMode = ARSession.ARCenterMode.WorldRoot;
            cARSession.WorldRootController = cWorldRootController;
            easyArEx.arSession = cARSession;
            easyArEx.arAssembly = cARSession.Assembly;

            var oRenderCamera = UnityHelper.GetNewGameObject("RenderCamera");
            oRenderCamera.transform.SetParent(oEasyAROrigin.transform);
            oRenderCamera.GetNewComponent<RenderCameraController>();
            oRenderCamera.GetNewComponent<CameraImageRenderer>();

            var oVIOCameraDevice = UnityHelper.GetNewGameObject("VIOCameraDevice");
            oVIOCameraDevice.transform.SetParent(oEasyAROrigin.transform);
            easyArEx.cameraDevice = oVIOCameraDevice.GetNewComponent<VIOCameraDeviceUnion>();
            easyArEx.cameraDevice.DeviceStrategy = setting.deviceStrategy;
            easyArEx.cameraDevice.DesiredMotionTrackerParameters.FPS = setting.fps;
            easyArEx.cameraDevice.DesiredMotionTrackerParameters.FocusMode = setting.focusMode;
            easyArEx.cameraDevice.DesiredMotionTrackerParameters.Resolution = setting.resolution;
            easyArEx.cameraDevice.DesiredMotionTrackerParameters.TrackingMode = setting.trackingMode;
            easyArEx.cameraDevice.DesiredMotionTrackerParameters.MinQualityLevel = setting.minQualityLevel;
#endif

            easyArEx.ResetDirection();
        }

        public void Destroy(EasyAREx easyArEx)
        {
	        if (!easyArEx.isStartup)
		        return;

	        easyArEx.isStartup = false;

#if EasyAR
	        UnityHelper.DestroyGameObject("EasyARWorldRoot");
            UnityHelper.DestroyGameObject("EasyAROrigin");
#endif
        }
    }
}