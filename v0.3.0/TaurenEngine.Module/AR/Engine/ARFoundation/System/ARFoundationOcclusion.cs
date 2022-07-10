/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using UnityEngine;
#if ARFoundation
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
#endif

namespace TaurenEngine.AR
{
    /// <summary>
    /// 人物遮挡（开启后才知道是否支持）
    /// </summary>
    internal class ARFoundationOcclusion : ARFoundationSystemBase, ISystemOcclusion
    {
#if ARFoundation
        private AROcclusionManager _arOcclusionManager;
#endif

        public ARFoundationOcclusion(ARFoundationEx arFoundationEx) : base(arFoundationEx)
        {
            IsAvailable = true;
        }

        protected override void Init()
        {
#if ARFoundation
            GameObject oCamera = _arEngine.Camera.gameObject;
            _arOcclusionManager = oCamera.GetNewComponent<AROcclusionManager>();
            _arOcclusionManager.requestedHumanDepthMode = HumanSegmentationDepthMode.Best;
            _arOcclusionManager.requestedHumanStencilMode = HumanSegmentationStencilMode.Best;
            _arOcclusionManager.requestedEnvironmentDepthMode = EnvironmentDepthMode.Disabled;
            _arOcclusionManager.requestedOcclusionPreferenceMode = OcclusionPreferenceMode.PreferHumanOcclusion;

            IsAvailable = (_arOcclusionManager.descriptor?.supportsHumanSegmentationStencilImage ?? false)
                          && (_arOcclusionManager.descriptor?.supportsHumanSegmentationDepthImage ?? false);
                        //&& (arOcclusionManager.descriptor?.supportsEnvironmentDepthImage ?? false); // 4.1.0 preview有该属性
#endif

            base.Init();
        }

#if ARFoundation
        public override void Enable()
        {
            base.Enable();

            if (_arOcclusionManager != null)
                _arOcclusionManager.enabled = true;
        }

        public override void Disable()
        {
            base.Disable();

	        if (_arOcclusionManager != null)
                _arOcclusionManager.enabled = false;
        }

        public override void Destroy()
        {
            if (IsDestroyed)
                return;

            if (_arOcclusionManager != null)
            {
                GameObject.Destroy(_arOcclusionManager);
                _arOcclusionManager = null;
            }

            base.Destroy();
        }
#endif
    }
}