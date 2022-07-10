/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using TaurenEngine.Core;
using TaurenEngine.LocationEx;

namespace TaurenEngine.LBS
{
    public class TmNaviRequest
    {
        /// <summary>
        /// 【必填】方案类型
        /// </summary>
        public TmNaviReqTravelType traveType;

        /// <summary>
        /// 【必填】版本签
        /// </summary>
        public TmNaviReqVersion version;

        /// <summary>
        /// 【必填】起点位置坐标
        /// </summary>
        public Location from;

        /// <summary>
        /// 【必填】终点位置坐标，格式：lat,lng
        /// </summary>
        public Location to;

        /// <summary>
        /// 终点POI ID（可通过腾讯位置服务地点搜索服务得到），当目的地为较大园区、小区时，会以引导点做为终点（如出入口等），体验更优。
        /// 该参数优先级高于to（坐标），但是当目的地无引导点数据或POI ID失效时，仍会使用to（坐标）作为终点
        /// </summary>
        public string to_poi;

        /// <summary>
        /// 【必填】开发key
        /// </summary>
        public string key;

        /// <summary>
        /// 回调函数
        /// </summary>
        public string callback;

        /// <summary>
        /// 返回值类型：json、jsonp
        /// </summary>
        public TmNaviReqOutput output = TmNaviReqOutput.None;

        public virtual string ToUrl()
        {
            var url = $"https://apis.map.qq.com/ws/direction/{version}/{traveType}/?from={from.latitude},{from.longitude}&to={to.latitude},{to.longitude}&key={key}";

            if (!string.IsNullOrEmpty(to_poi))
                url += $"&to_poi={to_poi}";

            if (!string.IsNullOrEmpty(callback))
                url += $"&callback={callback}";

            if (output != TmNaviReqOutput.None)
                url += $"&output={output}";
            TDebug.Log(url);
            return url;
        }
    }
}
