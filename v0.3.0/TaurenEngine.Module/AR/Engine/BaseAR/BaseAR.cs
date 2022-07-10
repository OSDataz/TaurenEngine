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
    internal class BaseAR : AREngineBase
    {
        internal ARCamera arCamera;
        internal CameraDevice cameraDevice;

        private bool _isAvailable = true;

        public override void Startup()
        {
            new BaseARComponent().InitComponent(this);

            _isAvailable = arCamera.IsAvailable && cameraDevice.IsAvailable;

            if (_isAvailable)
                Camera.transform.position = Vector3.zero;
            else
	            TDebug.Log($"BaseAR支持情况- ARCamera：{arCamera.IsAvailable} CameraDevice：{cameraDevice.IsAvailable}");
        }

        public override void Close()
        {
	        new BaseARComponent().Destroy(this);
        }

        public override void OnEnable()
        {
            arCamera.enabled = arCamera.IsAvailable;
            cameraDevice.enabled = cameraDevice.IsAvailable;
        }

        public override void OnDisable()
        {
            if (arCamera != null)
                arCamera.enabled = false;

            if (cameraDevice != null)
                cameraDevice.enabled = false;
        }

        public override void OnDestroy()
        {
	        if (IsDestroyed)
		        return;

	        arCamera = null;
            cameraDevice = null;

            base.OnDestroy();
        }

        public override bool IsAvailable => _isAvailable;
        public override AREngineType EngineType => AREngineType.BaseAR;

        public override Camera Camera => arCamera.Camera;

        public override bool IsMotionDeviceTracking => false;
    }

    internal class BaseARComponent : ARComponent
    {
        public void InitComponent(BaseAR baseAR)
        {
	        if (baseAR.isStartup)
		        return;

	        baseAR.isStartup = true;

            TDebug.Log("Init BaseAR Component");

            Camera camera = CreateARCamera();

            GameObject oCameraCanvas = GameObjectHelper.GetOrCreateGameObject("CameraCanvas");
            var cCanvas = oCameraCanvas.GetOrAddComponent<Canvas>();
            cCanvas.renderMode = RenderMode.ScreenSpaceCamera;
            cCanvas.worldCamera = camera;
            cCanvas.additionalShaderChannels = AdditionalCanvasShaderChannels.Normal | AdditionalCanvasShaderChannels.Tangent | AdditionalCanvasShaderChannels.TexCoord1;

            GameObject oGameraDevice = GameObjectHelper.GetOrCreateGameObject("CameraDevice");
            oGameraDevice.transform.SetParent(oCameraCanvas.transform);
            baseAR.cameraDevice = oGameraDevice.GetOrAddComponent<CameraDevice>();

            GameObject oARCamera = GameObjectHelper.GetOrCreateGameObject("ARCamera");
            Camera.main.transform.SetParent(oARCamera.transform);
            baseAR.arCamera = oARCamera.GetOrAddComponent<ARCamera>();
            baseAR.arCamera.Camera = camera;
        }

        public void Destroy(BaseAR baseAR)
        {
	        if (!baseAR.isStartup)
		        return;

	        baseAR.isStartup = false;

            GameObject oMainCamera = baseAR.Camera.gameObject;
	        oMainCamera.transform.SetParent(null);

            GameObjectHelper.DestroyGameObject("CameraCanvas");
            GameObjectHelper.DestroyGameObject("ARCamera");
        }
    }
}