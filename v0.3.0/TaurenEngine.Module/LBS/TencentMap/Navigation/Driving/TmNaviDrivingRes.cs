/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// 行车返回数据
    /// </summary>
    public class TmNaviDrivingRes : TmNaviResponse
    {
        /// <summary>
        /// 搜索结果
        /// </summary>
        public TmNaviDrivingResResult result;

        public override void Parse()
        {
            result?.Parse();
        }
    }

    public class TmNaviDrivingResResult
    {
        /// <summary>
        /// 路线方案（设置get_mp=1时可返回最多3条）
        /// </summary>
        public List<TmNaviDrivingResRoute> routes;

        public void Parse()
        {
            routes?.ForEach(item => item.Parse());
        }
    }

    public class TmNaviDrivingResRoute
    {
        /// <summary>
        /// 方案交通方式，固定值：“DRIVING”
        /// </summary>
        public TmNaviResMode mode;

        /// <summary>
        /// 方案标签，表明方案特色，详细说明见下文
        /// 示例：tags:[“LEAST_LIGHT”]
        /// </summary>
        public List<string> tags;

        /// <summary>
        /// 方案总距离
        /// </summary>
        public float distance;

        /// <summary>
        /// 方案估算时间（含路况）
        /// </summary>
        public float duration;

        /// <summary>
        /// 方案途经红绿灯个数
        /// </summary>
        public int traffic_light_count;

        /// <summary>
        /// 预估过路费（仅供参考），单位：元
        /// </summary>
        public float toll;

        /// <summary>
        /// 限行信息
        /// </summary>
        public TmNaviDrivingResRestriction restriction;

        /// <summary>
        /// 方案路线坐标点串（该点串经过压缩）
        /// https://lbs.qq.com/service/webService/webServiceGuide/webServiceRoute#8
        /// </summary>
        public List<double> polyline;

        /// <summary>
        /// 途经点，顺序与输入waypoints一致 （输入waypoints时才会有此结点返回）
        /// </summary>
        public List<TmNaviDrivingResWayPoint> waypoints;

        /// <summary>
        /// 预估打车费
        /// </summary>
        public TmNaviDrivingResTaxiFare taxi_fare;

        /// <summary>
        /// 路线步骤
        /// </summary>
        public List<TmNaviDrivingResStep> steps;

        public void Parse()
        {
            if (polyline == null)
                return;

            MathTmNavi.PolyLineDecompression(ref polyline);
        }
    }

    public class TmNaviDrivingResRestriction
    {
        /// <summary>
        /// 限行状态码
        /// </summary>
        public TmNaviDrivingResRestrictionStatus status;
    }

    public class TmNaviDrivingResWayPoint
    {
        /// <summary>
        /// 途经点路名
        /// </summary>
        public string title;

        /// <summary>
        /// 途经点坐标
        /// </summary>
        public TmLatLng location;
    }

    public class TmNaviDrivingResTaxiFare
    {
        /// <summary>
        /// 预估打车费用，单位：元
        /// </summary>
        public float fare;
    }

    public class TmNaviDrivingResStep
    {
        /// <summary>
        /// 阶段路线描述
        /// </summary>
        public string instruction;

        /// <summary>
        /// 阶段路线坐标点串在方案路线坐标点串的位置
        /// </summary>
        public List<string> polyline_idx;

        /// <summary>
        /// 阶段路线路名
        /// </summary>
        public string road_name;

        /// <summary>
        /// 阶段路线方向
        /// </summary>
        public string dir_desc;

        /// <summary>
        /// 阶段路线距离
        /// </summary>
        public float distance;

        /// <summary>
        /// 阶段路线末尾动作：如：左转调头
        /// </summary>
        public string act_desc;

        /// <summary>
        /// 末尾辅助动作：如：到达终点
        /// </summary>
        public string accessorial_desc;
    }
}