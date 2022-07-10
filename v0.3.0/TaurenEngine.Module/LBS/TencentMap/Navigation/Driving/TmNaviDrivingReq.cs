/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.LocationEx;

namespace TaurenEngine.LBS
{
    public class TmNaviDrivingReq : TmNaviRequest
    {
        /// <summary>
        /// 起点POI ID，传入后，优先级高于from（坐标）
        /// </summary>
        public string from_poi;

        /// <summary>
        /// 【from辅助参数】在起点位置时的车头方向，数值型，取值范围0至360（0度代表正北，顺时针一周360度）
        /// 传入车头方向，对于车辆所在道路的判断非常重要，直接影响路线计算的效果
        /// </summary>
        public float heading = -1;

        /// <summary>
        /// 【from辅助参数】速度，单位：米/秒，默认3。 当速度低于1.39米/秒时，heading将被忽略
        /// </summary>
        public float speed = -1;

        /// <summary>
        /// 【from辅助参数】定位精度，单位：米，取>0数值，默认5。 当定位精度>30米时heading参数将被忽略
        /// </summary>
        public int accuracy = -1;

        /// <summary>
        /// [from辅助参数] 起点道路类型，可选值：
        /// </summary>
        public TmNaviDrivingReqRoadType road_type = TmNaviDrivingReqRoadType.Default;

        /// <summary>
        /// 起点轨迹：
        /// 在真实的场景中，易受各种环境及设备精度影响，导致定位点产生误差，传入起点前段轨迹，可有效提升准确度。
        /// 优先级： 高于from参数
        /// </summary>
        public List<TmNaviDrivingReqTrack> from_track;

        /// <summary>
        /// 途经点，格式：lat1,lng1;lat2,lng2;… 最大支持16个
        /// </summary>
        public List<Location> waypoints;

        /// <summary>
        /// 策略参数
        /// </summary>
        public TmNaviDrivingReqPolicy policy;

        /// <summary>
        /// 避让区域：支持32个避让区域，每个区域最多可有9个顶点（如果是四边形则有4个坐标点，如果是五边形则有5个坐标点）
        /// 参数格式：
        /// 纬度在前，经度在后，用半角逗号 “,” 分隔，小数点后不超过6位，各经纬度之间用半角分号 “;” 分隔。各区域多边形间，用竖线符号分隔 “|”
        /// </summary>
        public List<Location> avoid_polygons;

        /// <summary>
        /// 车牌号，填入后，路线引擎会根据车牌对限行区域进行避让，不填则不不考虑限行问题
        /// </summary>
        public string plate_number;

        /// <summary>
        /// 车辆类型（影响限行规则）
        /// </summary>
        public TmNaviDrivingReqCarType cartype = TmNaviDrivingReqCarType.Default;

        /// <summary>
        /// 是否返回多方案
        /// </summary>
        public TmNaviDrivingReqGetMp get_mp = TmNaviDrivingReqGetMp.One;

        /// <summary>
        /// 不返回路线引导信息，可使回包数据量更小，取值：
        /// 0[默认] 返回路线引导信息
        /// 1不返回
        /// </summary>
        public bool no_step = false;

        public override string ToUrl()
        {
            var url = base.ToUrl();

            if (!string.IsNullOrEmpty(from_poi))
                url += $"&from_poi={from_poi}";

            if (!heading.Equals(-1))
                url += $"&heading={heading}";

            if (!speed.Equals(-1))
                url += $"&speed={speed}";

            if (accuracy != -1)
                url += $"&accuracy={accuracy}";

            if (road_type != TmNaviDrivingReqRoadType.Default)
                url += $"&road_type={(int)road_type}";

            if (from_track?.Count > 0)
            {
                var str = "";
                foreach (var item in from_track)
                {
                    if (str != "")
                        str += ";";

                    str += item.ToUrl();
                }

                url += $"&from_track={str}";
            }

            if (waypoints?.Count > 0)
            {
                var str = "";
                foreach (var item in waypoints)
                {
                    if (str != "")
                        str += ";";

                    str += $"{item.latitude},{item.longitude}";
                }

                url += $"&waypoints={str}";
            }

            if (policy != null)
            {
                url += $"&policy={policy.ToUrl()}";
            }

            if (avoid_polygons?.Count > 0)
            {
                var str = "";
                foreach (var item in avoid_polygons)
                {
                    if (str != "")
                        str += ";";

                    str += $"{item.latitude},{item.longitude}";
                }

                url += $"&avoid_polygons={str}";
            }

            if (!string.IsNullOrEmpty(plate_number))
                url += $"&plate_number={plate_number}";

            if (cartype != TmNaviDrivingReqCarType.Default)
                url += $"&cartype={(int)cartype}";

            if (get_mp != TmNaviDrivingReqGetMp.One)
                url += $"&get_mp={(int)get_mp}";

            if (no_step)
                url += "no_step=1";

            return url;
        }
    }

    /// <summary>
    /// 1.轨迹中最多支持传入50个点。
    /// 2.每个点之间英文分号分隔，时间顺序由旧到新（第一个点最早获取，最后一个点最新得到）
    /// 3.每个点中的信息用英文逗号分隔，并按以下顺序传入：
    /// 纬度,经度,速度,精度,运动方向,设备方向,时间;第2个点;第2个点……
    /// </summary>
    public class TmNaviDrivingReqTrack
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public double latitude;
        /// <summary>
        /// 经度
        /// </summary>
        public double longitude;
        /// <summary>
        /// 速度：GPS速度，单位 米/秒，取不到传-1
        /// </summary>
        public float speed;
        /// <summary>
        /// 精度：GPS精度, 单位毫米，取不TmFromCarType到传-1
        /// </summary>
        public float accuracy;
        /// <summary>
        /// 运动方向： gps方向，正北为0, 顺时针夹角，[0-360)，获取不到时传-1
        /// </summary>
        public float moveDir;
        /// <summary>
        /// 设备方向：正北为0, 顺时针夹角，[0-360)，取不到传-1
        /// </summary>
        public float phoneDir;
        /// <summary>
        /// 时间：定位获取该点的时间，Unix时间戳，取不到传0
        /// </summary>
        public string time;

        public string ToUrl()
        {
            return $"{latitude},{longitude},{speed},{accuracy},{moveDir},{phoneDir},{time}";
        }
    }

    public class TmNaviDrivingReqPolicy
    {
        public TmNaviDrivingReqFromPolicy policy;

        public List<TmNaviDrivingReqPolicyMore> policyMore;

        public string ToUrl()
        {
            string str = policy.ToString();

            if (policyMore?.Count > 0)
            {
                foreach (var item in policyMore)
                {
                    str += $",{item}";
                }
            }

            return str;
        }
    }
}