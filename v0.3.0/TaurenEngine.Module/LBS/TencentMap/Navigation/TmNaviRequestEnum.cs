/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

namespace TaurenEngine.LBS
{
    public enum TmNaviReqTravelType
    {
        /// <summary>
        /// 支持结合实时路况、少收费、不走高速等多种偏好，精准预估到达时间（ETA）
        /// </summary>
        driving,
        /// <summary>
        /// 基于步行路线规划
        /// </summary>
        walking,
        /// <summary>
        /// 基于自行车的骑行路线
        /// </summary>
        bicycling,
        /// <summary>
        /// 支持公共汽车、地铁等多种公共交通工具的换乘方案计算
        /// </summary>
        transit
    }

    public enum TmNaviReqVersion
    {
        v1,
        v2
    }

    public enum TmNaviReqOutput
    {
        None,
        json,
        jsonp
    }
}
