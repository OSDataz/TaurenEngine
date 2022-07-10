/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using UnityEngine;
#if ARFoundation
using UnityEngine.XR.ARFoundation;
#endif

namespace TaurenEngine.AR
{
    /// <summary>
    /// 环境光照
    /// </summary>
    internal class ARFoundationLight : ARFoundationSystemBase, ISystemLight
    {
#if ARFoundation
        private ARCameraManager _arCameraManager;
        private LightEstimation _saveLightEstimation;
        private Light[] _lights;
#endif

	    public ARFoundationLight(ARFoundationEx arFoundationEx) : base(arFoundationEx)
        {
            IsAvailable = true;
        }

        protected override void Init()
        {
#if ARFoundation
            _lights = GameObject.FindObjectsOfType<Light>();
            if (_lights.Length == 0)
                return;

            _arCameraManager = _arEngine.arCameraManager;
            _saveLightEstimation = _arCameraManager.currentLightEstimation;
            _arCameraManager.requestedLightEstimation = LightEstimation.AmbientIntensity | LightEstimation.AmbientColor;

            IsAvailable = (_arCameraManager.subsystem?.subsystemDescriptor?.supportsAverageBrightness ?? false)
                          && (_arCameraManager.subsystem?.subsystemDescriptor?.supportsAverageColorTemperature ?? false)
                          && (_arCameraManager.subsystem?.subsystemDescriptor?.supportsColorCorrection ?? false);
#endif

            base.Init();
        }

#if ARFoundation
        public override void Enable()
        {
            base.Enable();

            if (_arCameraManager != null)
                _arCameraManager.frameReceived += FrameChanged;
        }

        public override void Disable()
        {
            base.Disable();

	        if (_arCameraManager != null)
                _arCameraManager.frameReceived -= FrameChanged;
        }

        public override void Destroy()
        {
            if (IsDestroyed)
                return;


            if (_arCameraManager != null)
            {
                _arCameraManager.requestedLightEstimation = _saveLightEstimation;
                _arCameraManager = null;
            }

			_lights = null;

            base.Destroy();
        }

        private void FrameChanged(ARCameraFrameEventArgs args)
        {
            // 物理环境的估计亮度（如果有）。
            if (args.lightEstimation.averageBrightness.HasValue)
            {
                Brightness = args.lightEstimation.averageBrightness.Value;
                foreach (var light in _lights)
                {
                    light.intensity = Brightness.Value;
                }
            }
            else
            {
                Brightness = null;
            }

            // 物理环境的估计色温（如果有）。
            if (args.lightEstimation.averageColorTemperature.HasValue)
            {
                ColorTemperature = args.lightEstimation.averageColorTemperature.Value;
                foreach (var light in _lights)
                {
                    light.colorTemperature = ColorTemperature.Value;
                }
            }
            else
            {
                ColorTemperature = null;
            }

            // 物理环境的估计色彩校正值（如果有）。
            if (args.lightEstimation.colorCorrection.HasValue)
            {
                ColorCorrection = args.lightEstimation.colorCorrection.Value;
                foreach (var light in _lights)
                {
                    light.color = ColorCorrection.Value;
                }
            }
            else
            {
                ColorCorrection = null;
            }
        }

        /// <summary>
        /// 物理环境的估计亮度（如果有）。
        /// </summary>
        public float? Brightness { get; private set; }
        /// <summary>
        /// 物理环境的估计色温（如果有）。
        /// </summary>
        public float? ColorTemperature { get; private set; }
        /// <summary>
        /// 物理环境的估计色彩校正值（如果有）。
        /// </summary>
        public Color? ColorCorrection { get; private set; }
#endif
    }
}