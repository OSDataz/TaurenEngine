/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.AR
{
    /// <summary>
    /// 摄像机加速度控制器
    /// </summary>
    internal class CameraAcceControl : ICameraControl
    {
        public void Awake()
        {

        }

        public void OnEnable()
        {

        }

        public void Start()
        {

        }

        public void Update()
        {

        }

        public void OnDisable()
        {

        }

        public void OnDestroy()
        {

        }

        public Transform CameraTransform { private get; set; }
        public bool IsAvailable => SystemInfo.supportsAccelerometer;
    }
}