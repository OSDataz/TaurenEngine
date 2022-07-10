/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

namespace TaurenEngine.LBS
{
    public enum TmNaviTransitReqFromPolicy
    {
        /// <summary>
        /// 时间短（默认）
        /// </summary>
        LEAST_TIME,
        /// <summary>
        /// 少换乘
        /// </summary>
        LEAST_TRANSFER,
        /// <summary>
        /// 少步行
        /// </summary>
        LEAST_WALKING
    }

    public enum TmNaviTransitReqPolicyMore
    {
        /// <summary>
        /// 不坐地铁
        /// </summary>
        NO_SUBWAY
    }
}
