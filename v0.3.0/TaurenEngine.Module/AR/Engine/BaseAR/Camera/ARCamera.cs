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
    public class ARCamera : MonoBehaviour
    {
        private ICameraControl _cameraControl;
        private CameraControlType _cameraControlType = CameraControlType.None;

        private Camera _camera;

        private void Awake()
        {
            TDebug.Log("ARCamera Awake");

            _cameraControlType = CameraControlType.Unsupported;

            if (_cameraControlType == CameraControlType.Unsupported)
            {
                _cameraControl = new CameraGyroControl();
                if (_cameraControl.IsAvailable)
                    _cameraControlType = CameraControlType.Gyroscope;
                else
                    TDebug.Log("不支持陀螺仪");
            }

            if (_cameraControlType == CameraControlType.Unsupported)
            {
                _cameraControl = new CameraAcceControl();
                if (_cameraControl.IsAvailable)
                    _cameraControlType = CameraControlType.Accelerometer;
                else
                    TDebug.Log("不支持加速度传感器");
            }

            if (_cameraControlType != CameraControlType.Unsupported)
            {
                _cameraControl.Awake();
            }
            else
            {
                _cameraControl = null;
                enabled = false;
            }
        }

        void OnEnable()
        {
            _cameraControl?.OnEnable();
        }

        void Start()
        {
            _cameraControl?.Start();
        }

        void Update()
        {
            _cameraControl?.Update();
        }

        void OnDisable()
        {
            _cameraControl?.OnDisable();
        }

        void OnDestroy()
        {
            _cameraControl?.OnDestroy();
        }

        public Camera Camera
        {
            get => _camera;
            set
            {
                TDebug.Log("AR Camera 设置 Camera");
                _camera = value;

                if (_cameraControl != null)
                    _cameraControl.CameraTransform = _camera.transform;
            }
        }
        public CameraControlType CameraControlType => _cameraControlType;
        public bool IsAvailable => _cameraControl?.IsAvailable ?? false;
    }

    public enum CameraControlType
    {
        None,
        /// <summary> 不支持任意摄像机控制器，无IMU </summary>
        Unsupported,
        /// <summary> 依赖陀螺仪传感器控制 </summary>
        Gyroscope,
        /// <summary> 依赖加速度传感器控制 </summary>
        Accelerometer
    }

    internal interface ICameraControl
    {
        void Awake();
        void OnEnable();
        void Start();
        void Update();
        void OnDisable();
        void OnDestroy();

        bool IsAvailable { get; }
        Transform CameraTransform { set; }
    }
}