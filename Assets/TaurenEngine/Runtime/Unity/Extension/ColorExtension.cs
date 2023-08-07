/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:40:32
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Runtime.Unity
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