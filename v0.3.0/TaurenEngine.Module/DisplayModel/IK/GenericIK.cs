/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/27 19:40:16
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.DisplayModel
{
	public abstract class GenericIK
	{
		/// <summary>
		/// IK骨骼
		/// </summary>
		public Transform bone;

		/// <summary>
		/// 骨骼转动速度
		/// </summary>
		public float speed = 30;
		/// <summary>
		/// 骨骼恢复速度
		/// </summary>
		public float recoverSpeed = 20;

		/// <summary>
		/// 当前转动四元数
		/// </summary>
		protected Quaternion quaternion = Quaternion.identity;

		protected abstract void RotateToTarget(Vector3 lookTarget);

		private Vector3 Axis => Vector3.up;
		protected Vector3 TransformAxis => bone.rotation * Axis;
		protected Vector3 LocalTransformAxis => bone.localRotation * Axis;
	}
}