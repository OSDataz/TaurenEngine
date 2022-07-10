/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

namespace TaurenEngine.LBS
{
    public enum TmNaviDrivingReqRoadType
    {
        /// <summary>
        /// [默认] 不考虑起点道路类型
        /// </summary>
        Default = 0,
        /// <summary>
        /// 在桥上
        /// </summary>
        Bridge = 1,
        /// <summary>
        /// 在桥下
        /// </summary>
        UnderBridge = 2,
        /// <summary>
        /// 在主路
        /// </summary>
        MainRoad = 3,
        /// <summary>
        /// 在辅路
        /// </summary>
        SideRoad = 4,
        /// <summary>
        /// 在对面
        /// </summary>
        OppositeSide = 5,
        /// <summary>
        /// 桥下主路
        /// </summary>
        UnderBridgeMainRoad = 6,
        /// <summary>
        /// 桥下辅路
        /// </summary>
        UnderBridgeSideRoad = 7
    }

    public enum TmNaviDrivingReqFromPolicy
    {
        /// <summary>
        /// [默认]参考实时路况，时间最短
        /// </summary>
        LEAST_TIME,
        /// <summary>
        /// 网约车场景 – 接乘客
        /// </summary>
        PICKUP,
        /// <summary>
        /// 网约车场景 – 送乘客
        /// </summary>
        TRIP
    }

    public enum TmNaviDrivingReqPolicyMore
    {
        /// <summary>
        /// 参考实时路况
        /// </summary>
        REAL_TRAFFIC,
        /// <summary>
        /// 少收费
        /// </summary>
        LEAST_FEE,
        /// <summary>
        /// 不走高速
        /// </summary>
        AVOID_HIGHWAY,
        /// <summary>
        /// 该策略会通过终点坐标查找所在地点（如小区/大厦等），并使用地点出入口做为目的地，使路径更为合理
        /// </summary>
        NAV_POINT_FIRST
    }

    public enum TmNaviDrivingReqCarType
    {
        /// <summary>
        /// 默认]普通汽车
        /// </summary>
        Default = 0,
        /// <summary>
        /// 新能源
        /// </summary>
        NewEnergy = 1
    }

    public enum TmNaviDrivingReqGetMp
    {
        /// <summary>
        /// [默认]仅返回一条路线方案
        /// </summary>
        One = 0,
        /// <summary>
        /// 返回多方案（最多可返回三条方案供用户备选）
        /// </summary>
        More = 1
    }
}
