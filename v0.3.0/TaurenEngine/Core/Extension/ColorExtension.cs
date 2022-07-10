/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/24 13:53:16
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
    public static class ColorExtension
    {
        /// <summary>
		/// 字符串转化为指定颜色值（#ff0000）
		/// </summary>
		/// <param name="object"></param>
		/// <returns></returns>
		public static Color ToColor(this string @object)
        {
            return ColorUtility.TryParseHtmlString(@object, out var c) ? c : default;
        }
    }
}