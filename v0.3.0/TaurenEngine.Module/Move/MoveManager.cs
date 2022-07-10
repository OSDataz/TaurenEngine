/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 8:35:23
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.MoveEx
{
	/// <summary>
	/// 移动管理器
	/// </summary>
	internal sealed class MoveManager : Singleton<MoveManager>
	{
		#region 帧控制
		public MoveManager()
		{
			Start();
		}

		private IFrameUpdate _frameUpdate;

		public void Start()
		{
			_frameUpdate ??= FrameManager.Instance.GetFixedUpdate(FixedUpdate);
			_frameUpdate.Start();
		}

		public void Stop()
		{
			_frameUpdate?.Stop();
		}
		#endregion

		private void FixedUpdate()
		{
			for (int i = _moveRotations.Count - 1; i >= 0; --i)
			{
				if (_moveRotations[i].FixedUpdate())
				{
					var item = _moveRotations[i];
					_moveRotations.RemoveAt(i);
					item.completeCall?.Invoke();
				}
			}

			for (int i = _movePositions.Count - 1; i >= 0; --i)
			{
				if (_movePositions[i].FixedUpdate())
				{
					var item = _movePositions[i];
					_movePositions.RemoveAt(i);
					item.completeCall?.Invoke();
				}
			}

			if (_moveBinds.Count > 0)
				_moveBinds.ForEach(item => item.Update());
		}

		/// <summary>
		/// 取消该对象所有移动
		/// </summary>
		/// <param name="target"></param>
		public void CancelTarget(Transform target)
		{
			CancelMove(target);
			CancelLookAt(target);
			CancelBind(target);
		}

		/// <summary>
		/// 更新对象目标位置
		/// </summary>
		/// <param name="target"></param>
		/// <param name="targetPos"></param>
		public void MoveTo(Transform target, Vector3 targetPos)
		{
			var movePosition = _movePositions.Find(item => item.target == target);
			movePosition?.SetTargetPos(targetPos);

			var moveRotation = _moveRotations.Find(item => item.target == target);
			moveRotation?.SetTargetPos(targetPos);
		}

		#region 移动
		private List<MovePosition> _movePositions = new List<MovePosition>();

		/// <summary>
		/// 取消对象位移
		/// </summary>
		/// <param name="target"></param>
		public void CancelMove(Transform target)
		{
			var index = _movePositions.FindIndex(item => item.target == target);
			if (index == -1)
				return;

			_movePositions.RemoveAt(index);
		}

		private T GetMovePosition<T>(Transform target) where T : MovePosition, new()
		{
			var index = _movePositions.FindIndex(item => item.target == target);
			if (index == -1)
			{
				var moveTarget = new T();
				moveTarget.target = target;
				_movePositions.Add(moveTarget);
				return moveTarget;
			}
			else
			{
				if (_movePositions[index] is T moveTarget)
				{
				}
				else
				{
					moveTarget = new T();
					moveTarget.target = target;
					_movePositions[index] = moveTarget;
				}

				return moveTarget;
			}
		}

		/// <summary>
		/// 匀速运动
		/// </summary>
		/// <param name="target"></param>
		/// <param name="targetPos"></param>
		/// <param name="speed"></param>
		/// <param name="completeCall"></param>
		/// <param name="range"></param>
		/// <param name="isMoveForward"></param>
		public void MoveToUniformSpeed(Transform target, Vector3 targetPos, float speed, Action completeCall = null, float range = 0.01f, bool isMoveForward = false)
		{
			var moveTarget = GetMovePosition<MoveUniformSpeed>(target);
			moveTarget.completeCall = completeCall;
			moveTarget.rangePow = range * range;
			moveTarget.isMoveForward = isMoveForward;
			moveTarget.SetData(speed);
			moveTarget.SetTargetPos(targetPos);
		}

		/// <summary>
		/// 加速运动
		/// </summary>
		/// <param name="target"></param>
		/// <param name="targetPos"></param>
		/// <param name="speedMin"></param>
		/// <param name="speedMax"></param>
		/// <param name="moveRate"></param>
		/// <param name="completeCall"></param>
		/// <param name="range"></param>
		/// <param name="isMoveForward"></param>
		public void MoveToAccelerateSpeed(Transform target, Vector3 targetPos, float speedMin, float speedMax, float moveRate, Action completeCall = null, float range = 0.01f, bool isMoveForward = false)
		{
			var moveTarget = GetMovePosition<MoveAccelerateSpeed>(target);
			moveTarget.completeCall = completeCall;
			moveTarget.rangePow = range * range;
			moveTarget.isMoveForward = isMoveForward;
			moveTarget.SetData(speedMin, speedMax, moveRate);
			moveTarget.SetTargetPos(targetPos);
		}

		/// <summary>
		/// 减速运动
		/// </summary>
		/// <param name="target"></param>
		/// <param name="targetPos"></param>
		/// <param name="speedMin"></param>
		/// <param name="speedMax"></param>
		/// <param name="moveRate"></param>
		/// <param name="completeCall"></param>
		/// <param name="range"></param>
		/// <param name="isMoveForward"></param>
		public void MoveToDecreaseSpeed(Transform target, Vector3 targetPos, float speedMin, float speedMax, float moveRate, Action completeCall = null, float range = 0.01f, bool isMoveForward = false)
		{
			var moveTarget = GetMovePosition<MoveDecreaseSpeed>(target);
			moveTarget.completeCall = completeCall;
			moveTarget.rangePow = range * range;
			moveTarget.isMoveForward = isMoveForward;
			moveTarget.SetData(speedMin, speedMax, moveRate);
			moveTarget.SetTargetPos(targetPos);
		}

		/// <summary>
		/// 变速运动（先加速后减速）
		/// </summary>
		/// <param name="target"></param>
		/// <param name="targetPos"></param>
		/// <param name="speedMin"></param>
		/// <param name="speedMax"></param>
		/// <param name="moveRate"></param>
		/// <param name="completeCall"></param>
		/// <param name="range"></param>
		/// <param name="isMoveForward"></param>
		public void MoveToVariableSpeed(Transform target, Vector3 targetPos, float speedMin, float speedMax, float moveRate, Action completeCall = null, float range = 0.01f, bool isMoveForward = false)
		{
			var moveTarget = GetMovePosition<MoveVariableSpeed>(target);
			moveTarget.completeCall = completeCall;
			moveTarget.rangePow = range * range;
			moveTarget.isMoveForward = isMoveForward;
			moveTarget.SetData(speedMin, speedMax, moveRate);
			moveTarget.SetTargetPos(targetPos);
		}
		#endregion

		#region 旋转
		private List<MoveRotation> _moveRotations = new List<MoveRotation>();

		/// <summary>
		/// 取消对象旋转
		/// </summary>
		/// <param name="target"></param>
		public void CancelLookAt(Transform target)
		{
			var index = _moveRotations.FindIndex(item => item.target == target);
			if (index == -1)
				return;

			_moveRotations.RemoveAt(index);
		}

		/// <summary>
		/// 对象旋转
		/// </summary>
		/// <param name="target"></param>
		/// <param name="targetPos"></param>
		/// <param name="turnSpeed"></param>
		/// <param name="completeCall"></param>
		/// <param name="isMoveMeanwhile"></param>
		public void LookAt(Transform target, Vector3 targetPos, float turnSpeed, Action completeCall = null, bool isMoveMeanwhile = false)
		{
			var moveTarget = _moveRotations.Find(item => item.target == target);
			if (moveTarget == null)
			{
				moveTarget = new MoveRotation();
				moveTarget.target = target;
				_moveRotations.Add(moveTarget);
			}

			moveTarget.completeCall = completeCall;
			moveTarget.isMoveMeanwhile = isMoveMeanwhile;
			moveTarget.SetData(turnSpeed);
			moveTarget.SetTargetPos(targetPos);
		}
		#endregion

		#region 绑定对象
		private List<MoveBind> _moveBinds = new List<MoveBind>();

		public void CancelBind(Transform target)
		{
			var index = _moveBinds.FindIndex(item => item.target == target);
			if (index == -1)
				return;

			_moveBinds.RemoveAt(index);
		}

		private MoveBind GetMoveBind(Transform target)
		{
			var moveBind = _moveBinds.Find(item => item.target == target);
			if (moveBind == null)
			{
				moveBind = new MoveBind();
				moveBind.target = target;
				_moveBinds.Add(moveBind);
			}

			return moveBind;
		}

		public void BindTarget(Transform target, Transform bindTarget)
		{
			var moveBind = GetMoveBind(target);
			moveBind.SetData(bindTarget);
		}

		public void BindTarget(Transform target, Transform bindTarget, Vector3 offset)
		{
			var moveBind = GetMoveBind(target);
			moveBind.SetData(bindTarget, offset);
		}
		#endregion
	}
}