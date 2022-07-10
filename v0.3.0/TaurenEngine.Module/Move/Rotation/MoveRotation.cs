/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 8:36:19
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.MoveEx
{
	/// <summary>
	/// 位置旋转
	/// </summary>
	internal class MoveRotation : MoveBase
	{
		/// <summary> 转身速度（0表示无旋转） </summary>
		private float _turnSpeed = 0.0f;
		/// <summary> 旋转朝向 </summary>
		private Quaternion _turnRotQua;

		/// <summary> 是否同时移动 </summary>
		public bool isMoveMeanwhile = false;

		public override void SetTargetPos(Vector3 pos)
		{
			base.SetTargetPos(pos);

			if (!isMoveMeanwhile)
				UpdateCalcRot();
		}

		public void SetData(float turnSpeed)
		{
			_turnSpeed = turnSpeed;
		}

		protected override bool CheckCompleted()
		{
			var rot = target.localRotation;
			return (rot.x > _turnRotQua.x ? rot.x - _turnRotQua.x : _turnRotQua.x - rot.x)
				+ (rot.y > _turnRotQua.y ? rot.y - _turnRotQua.y : _turnRotQua.y - rot.y)
				+ (rot.z > _turnRotQua.z ? rot.z - _turnRotQua.z : _turnRotQua.x - rot.z)
				+ (rot.w > _turnRotQua.w ? rot.w - _turnRotQua.w : _turnRotQua.w - rot.w) < 0.01f;
		}

		public override bool FixedUpdate()
		{
			if (isMoveMeanwhile)
				UpdateCalcRot();

			if (CheckCompleted())
			{
				target.LookAt(_targetPos);
				return true;
			}

			target.localRotation = Quaternion.Lerp(target.localRotation, _turnRotQua, _turnSpeed);
			return false;
		}

		private void UpdateCalcRot()
		{
			_turnRotQua = Quaternion.LookRotation(_targetPos - target.transform.localPosition, Vector3.up);
		}
	}
}