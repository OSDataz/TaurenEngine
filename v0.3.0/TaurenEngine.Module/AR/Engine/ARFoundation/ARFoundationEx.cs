/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;
#if ARFoundation
using UnityEngine.XR.ARFoundation;
#endif

namespace TaurenEngine.AR
{
    /// <summary>
    /// ARFoundation 扩展使用
    /// </summary>
    internal class ARFoundationEx : AREngineBase
    {
#if ARFoundation
		private static bool _isAvailable = true;

        public ARSessionOrigin arSessionOrigin;
#endif

        private ARFoundationImage _arFoundationImage;
        private ARFoundationLight _arFoundationLight;
        private ARFoundationOcclusion _arFoundationOcclusion;
        private ARFoundationPlane _arFoundationPlane;

        public override void Startup()
        {
            new ARFoundationComponent().InitComponent(this);

            _arFoundationImage = new ARFoundationImage(this);
            _arFoundationLight = new ARFoundationLight(this);
            _arFoundationOcclusion = new ARFoundationOcclusion(this);
            _arFoundationPlane = new ARFoundationPlane(this);
        }

        public override void Close()
        {
	        new ARFoundationComponent().Destroy(this);
        }

        public override void Update()
        {
#if ARFoundation
            //Requirement 选择 Optional 才行
            if (ARSession.state < ARSessionState.CheckingAvailability
                || ARSession.state == ARSessionState.NeedsInstall
                || ARSession.state == ARSessionState.Installing
                )
            {
				TDebug.Log($"ARFoundation is not support: {ARSession.state}");
				_isAvailable = false;

				Close();
				OnDestroy();

				AREngine.Instance.Proxy.StartupLoop();
			}
#endif
		}

        public override void OnDestroy()
        {
            if (IsDestroyed)
                return;

            _arFoundationPlane = null;
            _arFoundationLight = null;
            _arFoundationImage = null;
            _arFoundationOcclusion = null;

#if ARFoundation
            arSessionOrigin = null;
#endif

            base.OnDestroy();
        }

        public override void ResetDirection()
        {
#if ARFoundation
            GameObject oARSessionOrigin = arSessionOrigin.gameObject;

            var cCameraWorldDirection = oARSessionOrigin.GetNewComponent<CameraWorldDirection>();
            cCameraWorldDirection.WorldRoot = oARSessionOrigin.transform;
#endif
        }

        public override bool IsAvailable
        {
	        get
	        {
#if ARFoundation
		        return _isAvailable;
#else
		        return false;
#endif
	        }
        }
        public override AREngineType EngineType => AREngineType.ARFoundation;

        public override Camera Camera
        {
	        get
	        {
#if ARFoundation
                return arSessionOrigin.camera;
#else
		        return null;
#endif
	        }
        }

        public override ISystemPlane SystemPlane => _arFoundationPlane;
        public override ISystemOcclusion SystemOcclusion => _arFoundationOcclusion;
        public override ISystemImage SystemImage => _arFoundationImage;
        public override ISystemLight SystemLight => _arFoundationLight;

        public override bool IsMotionDeviceTracking => true;

#if ARFoundation
        public ARCameraManager arCameraManager => Camera.gameObject.GetComponent<ARCameraManager>();
#endif
    }

    internal class ARFoundationComponent : ARComponent
    {
        public void InitComponent(ARFoundationEx arFoundationEx)
        {
	        if (arFoundationEx.isStartup)
		        return;

	        arFoundationEx.isStartup = true;

            TDebug.Log("Init ARFoundation Component");

#if ARFoundation
            Camera camera = CreateARCamera();

            GameObject oARSessionOrigin = UnityHelper.GetNewGameObject("ARSessionOrigin");
            arFoundationEx.arSessionOrigin = oARSessionOrigin.GetNewComponent<ARSessionOrigin>();
            arFoundationEx.arSessionOrigin.camera = camera;

            GameObject oMainCamera = camera.gameObject;
            oMainCamera.transform.SetParent(oARSessionOrigin.transform);
            oMainCamera.GetNewComponent<ARCameraManager>();
            oMainCamera.GetNewComponent<ARCameraBackground>();
            oMainCamera.GetNewComponent<ARPoseDriver>();

            GameObject oARSession = UnityHelper.GetNewGameObject("ARSession");
            oARSession.GetNewComponent<ARSession>();
            oARSession.GetNewComponent<ARInputManager>();

            arFoundationEx.ResetDirection();
#endif
        }

        public void Destroy(ARFoundationEx arFoundationEx)
        {
	        if (!arFoundationEx.isStartup)
		        return;

	        arFoundationEx.isStartup = false;

#if ARFoundation
            GameObject oMainCamera = arFoundationEx.Camera.gameObject;
            oMainCamera.transform.SetParent(null);

            oMainCamera.DestroyComponent<ARCameraBackground>();
            oMainCamera.DestroyComponent<ARCameraManager>();
            oMainCamera.DestroyComponent<ARPoseDriver>();

            UnityHelper.DestroyGameObject("ARSession");
            UnityHelper.DestroyGameObject("ARSessionOrigin");
#endif
        }
    }
}