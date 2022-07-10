/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;
#if ARFoundation
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
#endif

namespace TaurenEngine.AR
{
    /// <summary>
    /// 平面检测（开启后才知道是否支持）
    /// </summary>
    internal class ARFoundationPlane : ARFoundationSystemBase, ISystemPlane
    {
#if ARFoundation
        private ARPlaneManager _arPlaneManager;
        private ARRaycastManager _arRaycastManager;

        private readonly List<ARRaycastHit> _hits = new List<ARRaycastHit>();
#endif
        public ARFoundationPlane(ARFoundationEx arFoundationEx) : base(arFoundationEx)
        {
            IsAvailable = true;
        }

        protected override void Init()
        {
#if ARFoundation
            GameObject oARSessionOrigin = _arEngine.arSessionOrigin.gameObject;
            _arPlaneManager = oARSessionOrigin.GetNewComponent<ARPlaneManager>();
            //arPlaneManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;// | PlaneDetectionMode.Vertical;
            _arPlaneManager.planePrefab = AREngineSetting.Instance.arFoundationSetting?.planePrefab;
            _arRaycastManager = oARSessionOrigin.GetNewComponent<ARRaycastManager>();

            IsAvailable = (_arPlaneManager.subsystem?.subsystemDescriptor?.supportsArbitraryPlaneDetection ?? false)
                          || (_arPlaneManager.subsystem?.subsystemDescriptor?.supportsHorizontalPlaneDetection ?? false)
                          || (_arPlaneManager.subsystem?.subsystemDescriptor?.supportsVerticalPlaneDetection ?? false);
#endif

            base.Init();
        }

#if ARFoundation
        public override void Enable()
        {
            base.Enable();

            SetPlaneActive(true);

            if (_arPlaneManager != null)
                _arPlaneManager.enabled = true;

            if (_arRaycastManager != null)
                _arRaycastManager.enabled = true;
		}

        public override void Disable()
        {
            base.Disable();

	        SetPlaneActive(false);

            if (_arPlaneManager != null)
                _arPlaneManager.enabled = false;

            if (_arRaycastManager != null)
                _arRaycastManager.enabled = false;
        }

        public override void Destroy()
        {
            if (IsDestroyed)
                return;

            _arPlaneManager = null;
            _arRaycastManager = null;

            base.Destroy();
        }

        /// <summary>
        /// 设置检测到的平面是否激活
        /// </summary>
        /// <param name="value"></param>
        private void SetPlaneActive(bool value)
        {
            if (_arPlaneManager == null)
                return;

            foreach (var arPlane in _arPlaneManager.trackables)
                arPlane.gameObject.SetActive(value);
        }
#endif

        /// <summary>
        /// 获取平面坐标映射到的坐标点
        /// </summary>
        /// <param name="screenPoint"></param>
        /// <param name="pose"></param>
        /// <returns></returns>
        public bool GetPanelPos(Vector2 screenPoint, ref Pose pose)
        {
#if ARFoundation
            if (_arRaycastManager.Raycast(screenPoint, _hits, TrackableType.PlaneWithinPolygon))
            {
                if (_hits.Count > 0)
                {
                    pose = _hits[0].pose;
                    return true;
                }
            }
#endif
	        return false;
        }
    }
}