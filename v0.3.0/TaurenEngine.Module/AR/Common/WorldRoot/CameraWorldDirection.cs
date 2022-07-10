/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.Core;
using TaurenEngine.HardwareEx;
using UnityEngine;

namespace TaurenEngine.AR
{
    /// <summary>
    /// 摄像机世界方位调整
    /// </summary>
    public class CameraWorldDirection : MonoBehaviour
    {
        public Transform WorldRoot { get; set; }

        private NorthDevice _northDevice;

        private int _count = 0;// 记录Y轴旋转角度总数

        void Awake()
        {
            _northDevice = NorthDevice.Instance;
        }

        void OnEnable()
        {
            _count = 0;
            NorthDevice.Instance.SetEnabled(this, true);
        }

        void Update()
        {
            if (_count >= 10)
            {
                var vec = WorldRoot.eulerAngles;
                vec.y = _northDevice.Update();
                WorldRoot.eulerAngles = vec;

                TDebug.Log("世界方向旋转：" + vec);

                Destroy(this);
            }
            else
            {
                _count += 1;
                _northDevice.Update();
            }
        }

        void OnDisable()
        {
            NorthDevice.Instance.SetEnabled(this, false);
        }

        void OnDestroy()
        {
            TDebug.Log("CameraWorldDirection OnDestroy");

            WorldRoot = null;
            _northDevice = null;
        }
    }
}