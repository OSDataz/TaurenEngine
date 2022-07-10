/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.LBS
{
    public class MathTmNavi
    {
        /// <summary>
        /// 第一个坐标为原始未被压缩过的，之后的使用前向差分进行压缩
        /// </summary>
        /// <param name="polyline"></param>
        public static void PolyLineDecompression(ref List<double> polyline)
        {
            for (int i = 2; i < polyline.Count; ++i)
            {
                polyline[i] = polyline[i - 2] + polyline[i] / 1000000;
            }
        }
    }
}
