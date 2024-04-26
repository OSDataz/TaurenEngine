/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/10/8 19:25:21
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace Tauren.Module.Runtime
{
    public static class MoveExtension
    {
        public static void CancelTarget(this Transform @object)
            => MoveManager.Instance.CancelTarget(@object);

        #region 移动
        public static void MoveToUniformSpeed(this Transform @object, Vector3 targetPos, 
            float speed, Action completeCall = null, float range = 0.01f, bool isMoveForward = false)
            => MoveManager.Instance.MoveToUniformSpeed(@object, targetPos, speed, completeCall, range, isMoveForward);

        public static void MoveToAccelerateSpeed(this Transform @object, Vector3 targetPos,
            float speedMin, float speedMax, float moveRate, Action completeCall = null, float range = 0.01f, bool isMoveForward = false)
            => MoveManager.Instance.MoveToAccelerateSpeed(@object, targetPos, speedMin, speedMax, moveRate, completeCall, range, isMoveForward);

        public static void MoveToDecreaseSpeed(this Transform @object, Vector3 targetPos,
            float speedMin, float speedMax, float moveRate, Action completeCall = null, float range = 0.01f, bool isMoveForward = false)
            => MoveManager.Instance.MoveToDecreaseSpeed(@object, targetPos, speedMin, speedMax, moveRate, completeCall, range, isMoveForward);

        public static void MoveToVariableSpeed(this Transform @object, Vector3 targetPos,
            float speedMin, float speedMax, float moveRate, Action completeCall = null, float range = 0.01f, bool isMoveForward = false)
            => MoveManager.Instance.MoveToVariableSpeed(@object, targetPos, speedMin, speedMax, moveRate, completeCall, range, isMoveForward);

        public static void MoveTo(this Transform @object, Vector3 targetPos)
            => MoveManager.Instance.MoveTo(@object, targetPos);

        public static void CancelMove(this Transform @object)
            => MoveManager.Instance.CancelMove(@object);
        #endregion

        #region 旋转
        public static void LookAt(this Transform @object, Vector3 targetPos,
            float turnSpeed, Action completeCall = null, bool isMoveMeanwhile = false)
            => MoveManager.Instance.LookAt(@object, targetPos, turnSpeed, completeCall, isMoveMeanwhile);

        public static void CancelLookAt(this Transform @object)
            => MoveManager.Instance.CancelLookAt(@object);
        #endregion

        #region 绑定
        public static void BindTarget(this Transform @object, Transform bindTarget)
            => MoveManager.Instance.BindTarget(@object, bindTarget);

        public static void BindTarget(this Transform @object, Transform bindTarget, Vector3 offset)
            => MoveManager.Instance.BindTarget(@object, bindTarget, offset);

        public static void CancelBind(this Transform @object)
            => MoveManager.Instance.CancelBind(@object);
        #endregion
    }
}