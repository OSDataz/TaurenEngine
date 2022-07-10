/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.MaterialEx;
using UnityEngine;

namespace TaurenEngine.AR
{
    public class AuxiliaryLine : MonoBehaviour
    {
        /// <summary>
        /// 创建摄像机连接的辅助线
        /// </summary>
        /// <param name="name">辅助线对象名，不要重复</param>
        /// <returns></returns>
        public static AuxiliaryLine CreateCameraLine(string name)
        {
            var lineGo = new GameObject(name);
            var line = lineGo.AddComponent<AuxiliaryLine>();
            line.CameraTransform = AREngine.Instance.Proxy.CameraTransform;

            return line;
        }

        private LineRenderer _renderer;
        private Transform _toGoal;

        public Transform CameraTransform { private get; set; }

        public Transform ToGoal
        {
            get => _toGoal;
            set
            {
                _toGoal = value;
                gameObject.SetActive(_toGoal != null);
            }
        }

        void Awake()
        {
            _renderer = gameObject.AddComponent<LineRenderer>();
            _renderer.material = MaterialHelper.GetMaterial("Sprites/Default");
            _renderer.positionCount = 2;
            _renderer.startWidth = 0.001f;
            _renderer.endWidth = 0.01f;
            _renderer.startColor = Color.green;
            _renderer.endColor = Color.red;

            gameObject.SetActive(false);
        }

        void Update()
        {
            _renderer.SetPosition(0, CameraTransform.position + CameraTransform.forward * 0.5f);
            _renderer.SetPosition(1, _toGoal.position);
        }
    }
}