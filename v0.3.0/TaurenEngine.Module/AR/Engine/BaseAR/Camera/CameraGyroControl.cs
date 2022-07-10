/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.HardwareEx;
using UnityEngine;

namespace TaurenEngine.AR
{
    /// <summary>
    /// 陀螺仪摄像机控制器
    /// </summary>
    internal class CameraGyroControl : ICameraControl
    {
        private Gyroscope _gyro;

        private readonly Quaternion _rotationFix = new Quaternion(0f, 0f, 1f, 0f);

        public void Awake()
        {
            _gyro = GyroDevice.Instance.Gyro;
        }

        public void OnEnable()
        {
            GyroDevice.Instance.SetEnabled(this, true);
        }

        public void Start()
        {
            CameraTransform.parent.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
        }

        public void Update()
        {
            CameraTransform.localRotation = _gyro.attitude * _rotationFix;
        }

        public void OnDisable()
        {
            GyroDevice.Instance.SetEnabled(this, false);
        }

        public void OnDestroy()
        {
            _gyro = null;
            CameraTransform = null;
        }

        private bool Ignore(Vector3 value)
        {
            float min = 0.05f;
            return Mathf.Abs(value.x) < min && Mathf.Abs(value.y) < min && Mathf.Abs(value.z) < min;
        }

        public Transform CameraTransform { private get; set; }
        public bool IsAvailable => GyroDevice.Instance.IsAvailable;
    }
}