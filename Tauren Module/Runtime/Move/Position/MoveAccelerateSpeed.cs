/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 8:36:27
 *└────────────────────────┘*/

using UnityEngine;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 加速移动
	/// </summary>
	internal class MoveAccelerateSpeed : MovePosition
	{
		/// <summary> 最小速度 </summary>
		private float _speedMin;
		/// <summary> 最大速度 </summary>
		private float _speedMax;
		/// <summary> 加速比例（0-1） </summary>
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

			if (_speed < _speedMax)
			{
				_speed += (_speedMax - _speed) * _moveRate;
				if (_speed < _speedMin)
					_speed = _speedMin;
			}

			var pos = target.localPosition;
			var vec = GetEndPos() - pos;
			float value = _speed * Time.fixedDeltaTime;
			float magnitude = vec.magnitude;
			value = value > magnitude ? 1 : value / magnitude;

			pos.x += vec.x * value;
			pos.y += vec.y * value;
			pos.z += vec.z * value;
			target.localPosition = pos;

			return false;
		}
	}
}