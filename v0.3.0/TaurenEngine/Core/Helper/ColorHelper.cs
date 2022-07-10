/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/22 21:09:11
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
    public static class ColorHelper
    {
        public static Color ToColor(int r, int g, int b, float a = 1)
        {
            float max = byte.MaxValue;
            return new Color(r / max, g / max, b / max, a);
        }
	}
}