/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:56:51
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine
{
    public static class ColorUtility
    {
        /// <summary>
        /// RGBA转Color数据
        /// </summary>
        /// <param name="r">0-255</param>
        /// <param name="g">0-255</param>
        /// <param name="b">0-255</param>
        /// <param name="a">0-1</param>
        /// <returns></returns>
        public static Color ToColor(int r, int g, int b, float a = 1)
        {
            return new Color(r / byte.MaxValue, g / byte.MaxValue, b / byte.MaxValue, a);
        }
    }
}