/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.AR
{
    public class CylinderLine : MonoBehaviour
    {
        public Transform CameraTransform { private get; set; }

        private Transform _toGoal;
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
            gameObject.SetActive(false);
        }

        void Update()
        {
            var startPos = CameraTransform.position + CameraTransform.forward * 0.5f;
            var endPos = ToGoal.position;

            var rightPosition = (startPos + endPos) / 2;
            var rightRotation = endPos - startPos;
            float HalfLength = Vector3.Distance(startPos, endPos) / 2;
            float LThickness = 0.01f;//线的粗细

            transform.position = rightPosition;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, rightRotation);
            transform.localScale = new Vector3(LThickness, HalfLength, LThickness);
        }
    }
}