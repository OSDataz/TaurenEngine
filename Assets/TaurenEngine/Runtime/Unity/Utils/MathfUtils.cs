/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/26 11:38:17
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Runtime.Unity
{
    public static class MathfUtils
    {
		#region 角度计算
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