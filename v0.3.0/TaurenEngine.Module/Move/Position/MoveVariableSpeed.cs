/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 8:36:48
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.MoveEx
{
	/// <summary>
	/// 变速移动
	/// </summary>
	internal class MoveVariableSpeed : MovePosition
	{
		/// <summary> 最小速度 </summary>
		private float _speedMin;
		/// <summary> 最大速度 </summary>
		private float _speedMax;
		/// <summary> 变速比例（0-1） </summary>
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
			float value = magnitude * _moveRate;
			if (value > _speed)
			{
				// 加速
				if (_speed < _speedMax)
				{
					_speed += (_speedMax - _speed) * _moveRate;
					if (_speed < _speedMin)
						_speed = _speedMin;
				}
			}
			else
			{
				// 减速
				_speed = value < _speedMin ? _speedMin : value;
			}

			value = _speed * Time.fixedDeltaTime;
			value = value > magnitude ? 1 : value / magnitude;

			pos.x += vec.x * value;
			pos.y += vec.y * value;
			pos.z += vec.z * value;
			target.localPosition = pos;

			return false;
		}
	}
}