/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 10:18:27
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.LocationEx
{
	[Serializable]
	public class LocationSetting
	{
		/// <summary>
		/// 定位坐标系
		/// </summary>
		public LocationType locationType = LocationType.WGS84;
		/// <summary>
		/// 忽略海拔
		/// </summary>
		public bool ignoreAltitude = true;

		public UnityLocationSetting unitySettnig = new UnityLocationSetting();
		public AMapLocationSetting aMapSetting = new AMapLocationSetting();
	}

	public enum LocationType
	{
		WGS84,
		GCJ02,
		BD09,
	}

	public enum LocationSwitch
	{
		None,
		WGS84_To_GCJ02,
		WGS84_To_BD09,
	}
}