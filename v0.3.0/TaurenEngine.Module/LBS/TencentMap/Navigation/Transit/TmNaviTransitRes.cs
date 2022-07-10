/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.LBS
{
    public class TmNaviTransitRes : TmNaviResponse
    {
        /// <summary>
        /// 计算结果
        /// </summary>
        public TmNaviTransitResResult result;

        //public override void ParseJData(JObject jData)
        //{
        //    if (jData == null)
        //        return;

        //    base.ParseJData(jData);

        //    foreach (var item in jData)
        //    {
        //        if (item.Key == "result")
        //        {
        //            result = new TmNaviTransitResResult();
        //            result.ParseJData(item.Value);
        //        }
        //    }
        //}

        public override void Parse()
        {
            result?.Parse();
        }
    }

    public class TmNaviTransitResResult
    {
        /// <summary>
        /// 路线方案
        /// </summary>
        public List<TmNaviTransitResRoute> routes;

        //public void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    jData = jData["routes"];
        //    if (jData == null)
        //        return;

        //    routes = new List<TmNaviTransitResRoute>();

        //    foreach (var item in jData)
        //    {
        //        var route = new TmNaviTransitResRoute();
        //        route.ParseJData(item);
        //        routes.Add(route);
        //    }
        //}

        public void Parse()
        {
            routes?.ForEach(item => item.Parse());
        }
    }

    public class TmNaviTransitResRoute
    {
        /// <summary>
        /// 方案整体距离（米）
        /// </summary>
        public float distance;

        /// <summary>
        /// 方案估算时间（分钟）
        /// </summary>
        public float duration;

        /// <summary>
        /// 整体路线的外接矩形范围，可在地图显示时使用，
        /// 通过矩形西南+东北两个端点坐标定义而面，示例：
        /// “39.901405,116.334023,39.940289,116.451720”
        /// </summary>
        public string bounds;

        /// <summary>
        /// 一条完整公交路线可能会包含多种公共交通工具，各交通工具的换乘由步行路线串联起来，形成这样的结构（即 steps数组的结构）：
        /// [步行, 公交, 步行, 公交, 步行(到终点)]
        /// </summary>
        public List<TmNaviTransitResStep> steps;

        //public void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    distance = jData.Value<float>("distance");
        //    duration = jData.Value<float>("duration");
        //    bounds = jData.Value<string>("bounds");

        //    jData = jData["steps"];
        //    if (jData != null)
        //    {
        //        steps = new List<TmNaviTransitResStep>();

        //        foreach (var item in jData)
        //        {
        //            switch (item.Value<TmNaviResMode>("mode"))
        //            {
        //                case TmNaviResMode.WALKING:
        //                    var walkStep = new TmNaviTransitResWalking();
        //                    walkStep.ParseJData(item);
        //                    steps.Add(walkStep);
        //                    break;

        //                case TmNaviResMode.TRANSIT:
        //                    var transitStep = new TmNaviTransitResTransit();
        //                    transitStep.ParseJData(item);
        //                    steps.Add(transitStep);
        //                    break;
        //            }
        //        }
        //    }
        //}

        public void Parse()
        {
            steps?.ForEach(item => item.Parse());
        }
    }

    #region 公共
    public abstract class TmNaviTransitResStep
    {
        /// <summary>
        /// 本段交通方式，取值：
        /// WALKING：步行
        /// TRANSIT：公共交通工具
        /// 不同的方式，返回不同的数据结构，须根据该参数值来判断以哪种结构进行解析，各类具体定义见下文
        /// </summary>
        public TmNaviResMode mode;

        //public virtual void ParseJData(JToken jData)
        //{
        //    mode = jData.Value<TmNaviResMode>("mode");
        //}

        public abstract void Parse();
    }
    #endregion

    #region 步行
    /// <summary>
    /// 交通方式，固定值：“WALKING”，通过本参数判断数据结构类型
    /// </summary>
    public class TmNaviTransitResWalking : TmNaviTransitResStep
    {
        /// <summary>
        /// 本段step距离（米）
        /// </summary>
        public float distance;

        /// <summary>
        /// 估算时间（分钟）
        /// </summary>
        public float duration;

        /// <summary>
        /// 方案整体方向描述，如：“南”
        /// </summary>
        public string direction;

        /// <summary>
        /// 方案路线坐标点串（该点串经过压缩）
        /// https://lbs.qq.com/service/webService/webServiceGuide/webServiceRoute#8
        /// </summary>
        public List<double> polyline;

        /// <summary>
        /// 分路段诱导信息
        /// </summary>
        public List<TmNaviTransitResWalkingStep> steps;

        //public override void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    base.ParseJData(jData);

        //    distance = jData.Value<float>("distance");
        //    duration = jData.Value<float>("duration");
        //    direction = jData.Value<string>("direction");

        //    var jPolyline = jData["polyline"];
        //    if (jPolyline != null)
        //    {
        //        polyline = new List<double>();
        //        foreach (var item in jPolyline)
        //        {
        //            polyline.Add(item.Value<double>());
        //        }
        //    }

        //    var jStep = jData["steps"];
        //    if (jStep != null)
        //    {
        //        steps = new List<TmNaviTransitResWalkingStep>();
        //        foreach (var item in jPolyline)
        //        {
        //            var iData = new TmNaviTransitResWalkingStep();
        //            iData.ParseJData(item);
        //            steps.Add(iData);
        //        }
        //    }
        //}

        public override void Parse()
        {
            if (polyline == null)
                return;

            MathTmNavi.PolyLineDecompression(ref polyline);
        }
    }

    public class TmNaviTransitResWalkingStep
    {
        /// <summary>
        /// 路线方案（格式待定）
        /// </summary>
        public dynamic routes;

        /// <summary>
        /// 诱导描述，如 “沿东华门大街向西行驶74米”
        /// </summary>
        public string instruction;

        /// <summary>
        /// 本路段点串在polyline中的数组下标位置，格式：
        /// “polyline_idx”:[起始下标位置，结束下标位置]
        /// 详细使用见下文《polyline_idx说明》
        /// </summary>
        public List<int> polyline_idx;

        /// <summary>
        /// 本段路名
        /// </summary>
        public string road_name;

        /// <summary>
        /// 本段主要方向描述
        /// </summary>
        public string dir_desc;

        /// <summary>
        /// 本段路线距离
        /// </summary>
        public string distance;

        /// <summary>
        /// 本段末尾动作：如：左转调头
        /// </summary>
        public string act_desc;

        //public void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    routes = jData.Value<dynamic>("routes");
        //    instruction = jData.Value<string>("instruction");

        //    var jIdx = jData["polyline_idx"];
        //    if (jIdx != null)
        //    {
        //        polyline_idx = new List<int>();
        //        foreach (var item in jIdx)
        //        {
        //            polyline_idx.Add(item.Value<int>());
        //        }
        //    }

        //    road_name = jData.Value<string>("road_name");
        //    dir_desc = jData.Value<string>("dir_desc");
        //    distance = jData.Value<string>("distance");
        //    act_desc = jData.Value<string>("act_desc");
        //}
    }
    #endregion

    #region 公交交通
    public class TmNaviTransitResTransit : TmNaviTransitResStep
    {
        /// <summary>
        /// lines线路信息，因为起点到终点，可能存在多条线路可选，所以lines为数组，
        /// 而多条线路经过站点相同，所以数组第一会给出较完整信息，其它项侧只返回路线名称等简要信息。
        /// </summary>
        public List<TmNaviTransitResVehicleLine> lines;

        //public override void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    jData = jData["lines"];
        //    if (jData == null)
        //        return;

        //    lines = new List<TmNaviTransitResVehicleLine>();
        //    foreach (var item in jData)
        //    {
        //        switch (item.Value<TmNaviTransitResVehicle>("vehicle"))
        //        {
        //            case TmNaviTransitResVehicle.BUS:
        //                var busLine = new TmNaviTransitResBusLine();
        //                busLine.ParseJData(item);
        //                lines.Add(busLine);
        //                break;

        //            case TmNaviTransitResVehicle.SUBWAY:
        //                var subwayLine = new TmNaviTransitResSubwayLine();
        //                subwayLine.ParseJData(item);
        //                lines.Add(subwayLine);
        //                break;

        //            case TmNaviTransitResVehicle.RAIL:
        //                var railLine = new TmNaviTransitResRailLine();
        //                railLine.ParseJData(item);
        //                lines.Add(railLine);
        //                break;
        //        }
        //    }
        //}

        public override void Parse()
        {
            lines?.ForEach(item => item.Parse());
        }
    }

    public class TmNaviTransitResVehicleLine
    {
        /// <summary>
        /// 交通工具
        /// </summary>
        public TmNaviTransitResVehicle vehicle;

        /// <summary>
        /// 线路名称
        /// </summary>
        public string title;

        /// <summary>
        /// 经停站数
        /// </summary>
        public int station_count;

        /// <summary>
        /// 线路运营状态
        /// </summary>
        public TmNaviTransitResRunningStatus running_status;

        /// <summary>
        /// 预估费用，单位：元，返回-1时为缺少票价信息
        /// </summary>
        public float price;

        /// <summary>
        /// 路线距离
        /// </summary>
        public float distance;

        /// <summary>
        /// 路线估算时间（单位：分钟）
        /// </summary>
        public float duration;

        /// <summary>
        /// 线路坐标点串，可用于在地图中绘制路线
        /// （坐标串经过压缩，解压与使用，请参考下文）
        /// </summary>
        public List<double> polyline;

        /// <summary>
        /// 公交终点站（用于指示方向）
        /// </summary>
        public TmNaviTransitResDestination destination;

        /// <summary>
        /// 首班车时间
        /// </summary>
        public string start_time;

        /// <summary>
        /// 末班车时间
        /// </summary>
        public string end_time;

        /// <summary>
        /// 经停站列表
        /// </summary>
        public List<TmNaviTransitResStation> stations;

        //public virtual void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    vehicle = jData.Value<TmNaviTransitResVehicle>("vehicle");
        //    title = jData.Value<string>("title");
        //    station_count = jData.Value<int>("station_count");
        //    running_status = jData.Value<TmNaviTransitResRunningStatus>("running_status");
        //    price = jData.Value<float>("price");
        //    distance = jData.Value<float>("distance");
        //    duration = jData.Value<float>("duration");

        //    var token = jData["polyline"];
        //    if (token != null)
        //    {
        //        polyline = new List<double>();
        //        foreach (var item in token)
        //        {
        //            polyline.Add(item.Value<double>());
        //        }
        //    }

        //    token = jData["destination"];
        //    if (token != null)
        //    {
        //        destination = new TmNaviTransitResDestination();
        //        destination.ParseJData(token);
        //    }

        //    start_time = jData.Value<string>("start_time");
        //    end_time = jData.Value<string>("end_time");

        //    token = jData["stations"];
        //    if (token != null)
        //    {
        //        stations = new List<TmNaviTransitResStation>();
        //        foreach (var item in token)
        //        {
        //            var station = new TmNaviTransitResStation();
        //            station.ParseJData(item);
        //            stations.Add(station);
        //        }
        //    }
        //}

        public void Parse()
        {
            if (polyline == null)
                return;

            MathTmNavi.PolyLineDecompression(ref polyline);
        }
    }

    public class TmNaviTransitResDestination
    {
        /// <summary>
        /// 站点唯一标识
        /// </summary>
        public string id;

        /// <summary>
        /// 终点站名
        /// </summary>
        public string title;

        //public void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    id = jData.Value<string>("id");
        //    title = jData.Value<string>("title");
        //}
    }

    public class TmNaviTransitResStation
    {
        /// <summary>
        /// 站点唯一标识
        /// </summary>
        public string id;

        /// <summary>
        /// 终点站名
        /// </summary>
        public string title;

        /// <summary>
        /// 站点经纬度坐标
        /// </summary>
        public TmLatLng location;

        //public virtual void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    id = jData.Value<string>("id");
        //    title = jData.Value<string>("title");

        //    var token = jData["location"];
        //    if (token != null)
        //    {
        //        location = new TmLatLng();
        //        location.ParseJData(token);
        //    }
        //}
    }
    #endregion

    #region 公共汽车
    public class TmNaviTransitResBusLine : TmNaviTransitResVehicleLine
    {
        /// <summary>
        /// 线路唯一标识
        /// </summary>
        public string id;

        /// <summary>
        /// 上车站
        /// </summary>
        public List<TmNaviTransitResStation> geton;

        /// <summary>
        /// 下车站
        /// </summary>
        public List<TmNaviTransitResStation> getoff;

        //public override void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    base.ParseJData(jData);

        //    id = jData.Value<string>("id");

        //    var token = jData["geton"];
        //    if (token != null)
        //    {
        //        geton = new List<TmNaviTransitResStation>();
        //        foreach (var item in token)
        //        {
        //            var station = new TmNaviTransitResStation();
        //            station.ParseJData(item);
        //            geton.Add(station);
        //        }
        //    }

        //    token = jData["getoff"];
        //    if (token != null)
        //    {
        //        getoff = new List<TmNaviTransitResStation>();
        //        foreach (var item in token)
        //        {
        //            var station = new TmNaviTransitResStation();
        //            station.ParseJData(item);
        //            getoff.Add(station);
        //        }
        //    }
        //}
    }
    #endregion

    #region 地铁
    public class TmNaviTransitResSubwayLine : TmNaviTransitResVehicleLine
    {
        /// <summary>
        /// 线路唯一标识
        /// </summary>
        public string id;

        /// <summary>
        /// 上车站
        /// </summary>
        public List<TmNaviTransitResStationSubway> geton;

        /// <summary>
        /// 下车站
        /// </summary>
        public List<TmNaviTransitResStationSubway> getoff;

        //public override void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    base.ParseJData(jData);

        //    id = jData.Value<string>("id");

        //    var token = jData["geton"];
        //    if (token != null)
        //    {
        //        geton = new List<TmNaviTransitResStationSubway>();
        //        foreach (var item in token)
        //        {
        //            var station = new TmNaviTransitResStationSubway();
        //            station.ParseJData(item);
        //            geton.Add(station);
        //        }
        //    }

        //    token = jData["getoff"];
        //    if (token != null)
        //    {
        //        getoff = new List<TmNaviTransitResStationSubway>();
        //        foreach (var item in token)
        //        {
        //            var station = new TmNaviTransitResStationSubway();
        //            station.ParseJData(item);
        //            getoff.Add(station);
        //        }
        //    }
        //}
    }

    public class TmNaviTransitResStationSubway : TmNaviTransitResStation
    {
        /// <summary>
        /// 出入口
        /// </summary>
        public TmNaviTransitResDestination exit;

        //public override void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    base.ParseJData(jData);

        //    var token = jData["exit"];
        //    if (token != null)
        //    {
        //        exit = new TmNaviTransitResDestination();
        //        exit.ParseJData(token);
        //    }
        //}
    }
    #endregion

    #region 火车
    public class TmNaviTransitResRailLine : TmNaviTransitResVehicleLine
    {
        /// <summary>
        /// 发车时间
        /// </summary>
        public string departure_time;

        /// <summary>
        /// 到达时间
        /// </summary>
        public string arrival_time;

        /// <summary>
        /// 耗时天数，1为当天到达，2为隔天到达，以此类推
        /// </summary>
        public float days_count;

        /// <summary>
        /// 上车站
        /// </summary>
        public List<TmNaviTransitResStation> geton;

        /// <summary>
        /// 下车站
        /// </summary>
        public List<TmNaviTransitResStation> getoff;

        //public override void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    base.ParseJData(jData);

        //    departure_time = jData.Value<string>("departure_time");
        //    arrival_time = jData.Value<string>("arrival_time");
        //    days_count = jData.Value<float>("days_count");

        //    var token = jData["geton"];
        //    if (token != null)
        //    {
        //        geton = new List<TmNaviTransitResStation>();
        //        foreach (var item in token)
        //        {
        //            var station = new TmNaviTransitResStation();
        //            station.ParseJData(item);
        //            geton.Add(station);
        //        }
        //    }

        //    token = jData["getoff"];
        //    if (token != null)
        //    {
        //        getoff = new List<TmNaviTransitResStation>();
        //        foreach (var item in token)
        //        {
        //            var station = new TmNaviTransitResStation();
        //            station.ParseJData(item);
        //            getoff.Add(station);
        //        }
        //    }
        //}
    }
    #endregion
}