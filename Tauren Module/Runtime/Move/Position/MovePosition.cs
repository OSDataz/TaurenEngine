/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 8:36:37
 *└────────────────────────┘*/

using Tauren.Core.Runtime;
using UnityEngine;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 位置移动
	/// </summary>
	internal abstract class MovePosition : MoveBase
	{
		/// <summary> 当前移速 </summary>
		protected float _speed;

		/// <summary> 目标点相应范围半径（平方） </summary>
		public float rangePow = 0.0001f;
		/// <summary> 是否向朝向前方移动（注意：需要和旋转一起使用） </summary>
		public bool isMoveForward = false;

		/// <summary>
		/// 检测是否抵达
		/// </summary>
		/// <returns></returns>
		protected override bool CheckCompleted()
		{
			return target.localPosition.DistanceSquare(_targetPos) <= rangePow;
		}

		protected Vector3 GetEndPos()
		{
			return isMoveForward ?
				target.localPosition + target.forward * Vector3.Distance(target.localPosition, _targetPos) : _targetPos;
		}
	}
}