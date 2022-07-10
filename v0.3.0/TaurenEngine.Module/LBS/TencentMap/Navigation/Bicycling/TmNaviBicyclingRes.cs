/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.LBS
{
    public class TmNaviBicyclingRes : TmNaviResponse
    {
        public TmNaviBicyclingResResult result;

        public override void Parse()
        {
            result?.Parse();
        }
    }

    public class TmNaviBicyclingResResult
    {
        public List<TmNaviBicyclingResRoute> routes;

        public void Parse()
        {
            routes?.ForEach(item => item.Parse());
        }
    }

    public class TmNaviBicyclingResRoute
    {
        /// <summary>
        /// 方案交通方式，固定值：“BICYCLING”
        /// </summary>
        public TmNaviResMode mode;

        /// <summary>
        /// 方案整体距离（米）
        /// </summary>
        public float distance;

        /// <summary>
        /// 方案估算时间（分钟）
        /// </summary>
        public float duration;

        /// <summary>
        /// 方案整体方向
        /// </summary>
        public string direction;

        /// <summary>
        /// 方案路线坐标点串（该点串经过压缩）
        /// https://lbs.qq.com/service/webService/webServiceGuide/webServiceRoute#8
        /// </summary>
        public List<double> polyline;

        /// <summary>
        /// 路线步骤
        /// </summary>
        public List<TmNaviBicyclingResStep> steps;

        public void Parse()
        {
            if (polyline == null)
                return;

            MathTmNavi.PolyLineDecompression(ref polyline);
        }
    }

    public class TmNaviBicyclingResStep
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
    }
}
