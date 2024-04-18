/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 8:36:43
 *└────────────────────────┘*/

using UnityEngine;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 匀速移动
	/// </summary>
	internal class MoveUniformSpeed : MovePosition
	{
		public void SetData(float speed)
		{
			_speed = speed;
		}

		public override bool FixedUpdate()
		{
			if (CheckCompleted())
				return true;

			var pos = target.localPosition;
			var vec = GetEndPos() - pos;
			float value = vec.magnitude;
			var speed = Mathf.Min(value, _speed * Time.fixedDeltaTime);
			value = speed / value;

			pos.x += vec.x * value;
			pos.y += vec.y * value;
			pos.z += vec.z * value;
			target.localPosition = pos;

			return false;
		}
	}
}