/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 8:36:32
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.MoveEx
{
	/// <summary>
	/// 减速移动
	/// </summary>
	internal class MoveDecreaseSpeed : MovePosition
	{
		/// <summary> 最小速度 </summary>
		private float _speedMin;
		/// <summary> 最大速度 </summary>
		private float _speedMax;
		/// <summary> 减速比例（0-1） </summary>
		private float _moveRate;

		public void SetData(float speedMin, float speedMax, float moveRate)
		{
			_speedMin = speedMin;
			_speedMax = speedMax;
			_moveRate = moveRate;
		}

		public override bool FixedUpdate()
		{
			if (CheckCompleted())
				return true;

			var pos = target.localPosition;
			var vec = GetEndPos() - pos;
			float magnitude = vec.magnitude;

			_speed = magnitude * _moveRate;
			if (_speed > _speedMax) _speed = _speedMax;
			else if (_speed < _speedMin) _speed = _speedMin;

			float value = _speed * Time.fixedDeltaTime;
			value = value > magnitude ? 1 : value / magnitude;

			pos.x += vec.x * value;
			pos.y += vec.y * value;
			pos.z += vec.z * value;
			target.localPosition = pos;

			return false;
		}
	}
}