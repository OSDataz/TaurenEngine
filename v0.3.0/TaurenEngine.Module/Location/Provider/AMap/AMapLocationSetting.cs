/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/22 14:30:02
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.LocationEx
{
	[Serializable]
	public class AMapLocationSetting
	{
		/// <summary>
		/// AMap 定位模式
		/// </summary>
		public AMapLocationMode aMapLocationModel = AMapLocationMode.Hight_Accuracy;
		/// <summary>
		/// AMap 定位间隔时间 ms（最小间隔支持为1000ms）
		/// </summary>
		public long interval = 1000;
	}

	public enum AMapLocationMode
	{
		/// <summary>
		/// 低功耗模式
		/// </summary>
		Battery_Saving,
		/// <summary>
		/// 仅设备模式
		/// </summary>
		Device_Sensors,
		/// <summary>
		/// 高精度模式
		/// </summary>
		Hight_Accuracy
	}
}