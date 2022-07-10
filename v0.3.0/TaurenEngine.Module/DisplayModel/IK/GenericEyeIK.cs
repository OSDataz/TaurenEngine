/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/27 19:44:05
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.DisplayModel
{
	public class GenericEyeIK : GenericIK
	{
		/// <summary>
		/// 眼睛水平最大转动角度
		/// </summary>
		public float angleX = 4;
		/// <summary>
		/// 眼睛垂直最大转动角度
		/// </summary>
		public float angleY = 1;

		/// <summary>
		/// 依赖于头部是否恢复
		/// </summary>
		public bool needRecoverEye;

		protected override void RotateToTarget(Vector3 lookTarget)
		{
			if (needRecoverEye)
			{
				quaternion = Quaternion.RotateTowards(quaternion, Quaternion.identity, recoverSpeed * Time.deltaTime);
			}
			else
			{
				// 世界坐标系的目标向量
				var baseTargetDir = lookTarget - bone.position;
				// 转到本地坐标系的目标向量，一定要用BoneParent转换。。。一开始没注意到这里，总是转不对，经常被模型翻白眼。。。
				var targetDir = bone.parent.InverseTransformDirection(baseTargetDir);
				// 本地坐标系下的蓝色轴方向
				var forwardTemp = bone.parent.InverseTransformDirection(bone.forward);
				// 以下计算都在本地坐标系下计算
				// 计算一个本地坐标系下，绿色轴到目标向量的旋转轴 rotateAxis
				var rotateAxis = Vector3.Cross(LocalTransformAxis, targetDir);
				// 转动角度
				var targetAngle = Vector3.Angle(targetDir, LocalTransformAxis);
				// 根据旋转轴 和 本地坐标系蓝色轴 计算一个权重，用于在AngleX和AngleY中做插值（很魔性的计算。。。）
				// 这里一定要做先归一化，再点乘
				float cosAAA = Mathf.Abs(Vector3.Dot(rotateAxis.normalized, forwardTemp.normalized));
				// 平滑过渡一下
				float angle = Mathf.SmoothStep(angleX, angleY, cosAAA);

				if (targetAngle > angle)
				{
					targetAngle = angle;
				}

				Quaternion targetQuaternion = Quaternion.AngleAxis(targetAngle, rotateAxis);
				quaternion = Quaternion.RotateTowards(quaternion, targetQuaternion, speed * Time.deltaTime);
			}

			bone.localRotation = quaternion * bone.localRotation;
		}
	}
}