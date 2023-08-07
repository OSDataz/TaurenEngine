/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/1 22:15:47
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Runtime.Core
{
	public static class MathUtils
	{
		#region 角度计算
		/// <summary>
		/// 标准化角度值【0-360】
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static float NormalizeDegrees(float value)
		{
			return value < 0 ? value % 360 + 360 : value % 360;
		}

		private static double Deg2Rad = Math.PI / 180;

		/// <summary>
		/// 角度值转弧度值(double)
		/// </summary>
		/// <param name="degrees"></param>
		/// <returns></returns>
		public static double DegreesToRadians(double degrees)
		{
			return degrees * Deg2Rad;
		}
		#endregion
	}
}