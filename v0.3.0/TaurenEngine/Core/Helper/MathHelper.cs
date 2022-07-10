/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/26 11:38:17
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine.Core
{
    public static class MathHelper
    {
		#region float 公式
		/// <summary>
		/// 标准化角度值【0-360】
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static float NormalizeDegrees(float value)
		{
			return value < 0 ? value % 360 + 360 : value % 360;
		}
		#endregion

		#region double 公式
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

		/// <summary>
		/// 角度值转弧度值(float)
		/// </summary>
		/// <param name="degrees"></param>
		/// <returns></returns>
		public static float DegreesToRadians(float degrees)
        {
			return degrees * Mathf.Deg2Rad;
        }
		#endregion
	}
}