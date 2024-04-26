/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/12 14:36:10
 *└────────────────────────┘*/

using UnityEngine;

namespace Tauren.Core.Runtime
{
	public static class Vector3Extension
	{
		public static float DistanceSquare(this Vector3 value, Vector3 vec)
		{
			var num1 = value.x - vec.x;
			var num2 = value.y - vec.y;
			var num3 = value.z - vec.z;
			return num1 * num1 + num2 * num2 + num3 * num3;
		}

		public static float Distance(this Vector3 value, Vector3 vec)
		{
			return Mathf.Sqrt(DistanceSquare(value, vec));
		}
	}
}