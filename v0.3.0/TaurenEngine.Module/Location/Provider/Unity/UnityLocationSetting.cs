/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/22 14:30:15
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.LocationEx
{
	[Serializable]
	public class UnityLocationSetting
	{
		/// <summary>
		/// Unity 可接受的位置测量的最低精度，以米为单位。
		/// </summary>
		public float desiredAccuracyInMeters = 10f;
		/// <summary>
		/// Unity 连续两次位置更新之间的最小距离以米为单位。
		/// </summary>
		public float updateDistanceInMeters = 10f;
	}
}