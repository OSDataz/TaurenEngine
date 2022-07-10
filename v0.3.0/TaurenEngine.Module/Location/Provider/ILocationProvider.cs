/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 10:17:50
 *└────────────────────────┘*/

namespace TaurenEngine.LocationEx
{
	internal interface ILocationProvider
	{
		void Awake();
		void OnEnable();
		void OnDisable();
		void OnDestroy();

		/// <summary>
		/// 获取定位坐标
		/// </summary>
		/// <param name="loc"></param>
		/// <returns>是否成功</returns>
		bool GetLocation(ref Location loc);

		LocationSwitch GetLocationSwitch(LocationType locationType);

		/// <summary> 定位是否准备就绪 </summary>
		bool IsReady { get; }
		/// <summary> 是否支持定位 </summary>
		bool IsAvailable { get; }
	}
}