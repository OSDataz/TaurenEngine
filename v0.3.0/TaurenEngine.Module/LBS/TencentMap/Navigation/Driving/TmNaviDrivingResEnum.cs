/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

namespace TaurenEngine.LBS
{
    public enum TmNaviDrivingResRestrictionStatus
    {
        /// <summary>
        /// 途经没有限行城市，或路线方案未涉及限行区域
        /// </summary>
        None = 0,
        /// <summary>
        /// 途经包含有限行的城市
        /// </summary>
        Limit = 1,
        /// <summary>
        /// [设置车牌] 已避让限行
        /// </summary>
        Avoid = 2,
        /// <summary>
        /// [设置车牌] 无法避开限行区域（本方案包含限行路段）
        /// </summary>
        Cannot = 3
    }
}
