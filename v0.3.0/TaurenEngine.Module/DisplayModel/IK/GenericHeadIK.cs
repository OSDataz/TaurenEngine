/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/27 19:43:35
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.DisplayModel
{
	public class GenericHeadIK : GenericIK
	{
		/// <summary>
		/// 头与观察点超过angleMove之后，头开始转动
		/// </summary>
		public float angleMove = 4;
		/// <summary>
		/// 头一直可以转到angleStay，之后保持直到超过AngleRecover
		/// </summary>
		public float angleStay = 20;
		/// <summary>
		/// 恢复角度
		/// </summary>
		public float angleRecover = 90;

		/// <summary>
		/// 恢复中
		/// </summary>
		public bool inRecover;

		protected override void RotateToTarget(Vector3 lookTarget)
		{
			var baseTargetDir = lookTarget = bone.position;
			var angle = Vector3.Angle(TransformAxis, baseTargetDir);

			if (angle > angleMove && angle <= angleRecover)
			{
				// 旋转轴
				inRecover = false;
				var axis = Vector3.Cross(TransformAxis, baseTargetDir).normalized;
				angle = Mathf.Min(angle - angleMove, angleStay - angleMove);// 超出AngleStay，维持头的这个旋转

				// 真正的旋转
				var realQua = Quaternion.AngleAxis(angle, axis);

				// 这里后面说，主要是做一个保持头部水平的矫正
				Vector3 realBlue = realQua * bone.forward;
				float realAngle = 90 - Vector3.Angle(realBlue, Vector3.up);

				//矫正Bone 的 蓝色轴，使之水平
				//Quaternion quaternionTemp = Quaternion.AngleAxis(CorrectAngle * axisTemp.y, Bone.up);
				var correctQua = Quaternion.AngleAxis(realAngle, bone.up);

				//quaternionCorrect * quaternion == 先转quaternion, 再转矫正quaternionCorrect 
				realQua = correctQua * realQua;

				quaternion = Quaternion.RotateTowards(quaternion, realQua, speed * Time.deltaTime);
			}
			else
			{
				// 恢复到正常角度
				inRecover = true;
				quaternion = Quaternion.RotateTowards(quaternion, Quaternion.identity, recoverSpeed * Time.deltaTime);
			}

			bone.rotation = quaternion * bone.rotation;
		}
	}
}