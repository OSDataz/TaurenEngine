/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 11:01:31
 *└────────────────────────┘*/

using System.Text.RegularExpressions;

namespace TaurenEngine
{
	public static partial class StringExtension
	{
		/// <summary>
		/// 截取字符串
		/// </summary>
		/// <param name="object"></param>
		/// <param name="match">匹配文本</param>
		/// <param name="useLastIndex">是否从后往前查询</param>
		/// <param name="toSubBefore">获取匹配文本0之前文本</param>
		/// <returns></returns>
		public static string Substring(this string @object, string match, bool useLastIndex = false, bool toSubBefore = true)
		{
			int idx;
			if (useLastIndex) idx = @object.LastIndexOf(match);
			else idx = @object.IndexOf(match);

			if (idx == -1)
				return string.Empty;

			if (toSubBefore) return @object.Substring(0, idx);
			else return @object.Substring(idx + match.Length);
		}

		/// <summary>
		/// 截取字符串
		/// </summary>
		/// <param name="object"></param>
		/// <param name="startMatch"></param>
		/// <param name="endMatch"></param>
		/// <param name="useLastIndex">是否从后往前查询</param>
		/// <returns></returns>
		public static string Substring(this string @object, string startMatch, string endMatch, bool useLastIndex = false)
		{
			if (useLastIndex)
			{
				var eIdx = @object.LastIndexOf(endMatch);
				if (eIdx == -1)
					return string.Empty;

				var idx = @object.LastIndexOf(startMatch, eIdx - endMatch.Length);
				if (idx == -1)
					return string.Empty;

				idx += startMatch.Length;
				return @object.Substring(idx, eIdx - idx);
			}
			else
			{
				var idx = @object.IndexOf(startMatch);
				if (idx == -1)
					return string.Empty;

				idx += startMatch.Length;
				var eIdx = @object.IndexOf(endMatch, idx);
				if (eIdx == -1)
					return string.Empty;

				return @object.Substring(idx, eIdx - idx);
			}
		}

		/// <summary>
		/// 是否是int型数字
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsNumericInt(this string value)
		{
			return Regex.IsMatch(value, @"^\d*$");
		}
	}
}