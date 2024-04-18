/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 8:35:36
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace Tauren.Module.Runtime
{
	internal abstract class MoveBase
	{
		/// <summary>
		/// 移动对象
		/// </summary>
		public Transform target;
		/// <summary>
		/// 目标点
		/// </summary>
		protected Vector3 _targetPos;

		/// <summary>
		/// 移动完成回调
		/// </summary>
		public Action completeCall;

		public virtual void SetTargetPos(Vector3 pos)
		{
			_targetPos = pos;
		}

		protected abstract bool CheckCompleted();

		public abstract bool FixedUpdate();
	}
}