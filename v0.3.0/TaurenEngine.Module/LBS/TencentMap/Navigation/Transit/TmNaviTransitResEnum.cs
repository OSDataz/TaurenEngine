/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

namespace TaurenEngine.LBS
{
    public enum TmNaviTransitResVehicle
    {
        /// <summary>
        /// 公共汽车
        /// </summary>
        BUS,
        /// <summary>
        /// 地铁
        /// </summary>
        SUBWAY,
        /// <summary>
        /// 火车
        /// </summary>
        RAIL
    }

    public enum TmNaviTransitResRunningStatus
    {
        /// <summary>
        /// 300：正常
        /// </summary>
        Common = 300,
        /// <summary>
        /// 301：可能错过末班车
        /// </summary>
        MayMiss = 301,
        /// <summary>
        /// 302：首班车还未发出
        /// </summary>
        NoDeparture = 302,
        /// <summary>
        /// 303：停运
        /// </summary>
        Decommissioning = 303
    }
}