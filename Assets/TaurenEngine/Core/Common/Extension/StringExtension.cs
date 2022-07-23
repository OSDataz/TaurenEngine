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
		/// 从前往后查询，获取指定匹配字符串之前的部分
		/// </summary>
		/// <param name="object"></param>
		/// <param name="match"></param>
		/// <returns></returns>
		public static string SubstringBefore(this string @object, string match)
		{
			var idx = @object.IndexOf(match);
			if (idx == -1)
				return string.Empty;

			return @object.Substring(0, idx);
		}

		/// <summary>
		/// 从后往前查询，获取指定匹配字符串之前的部分
		/// </summary>
		/// <param name="object"></param>
		/// <param name="match"></param>
		/// <returns></returns>
		public static string SubstringBeforeLast(this string @object, string match)
		{
			var idx = @object.LastIndexOf(match);
			if (idx == -1)
				return string.Empty;

			return @object.Substring(0, idx);
		}

		/// <summary>
		/// 从前往后查询，获取指定匹配字符串之后的部分
		/// </summary>
		/// <param name="object"></param>
		/// <param name="match"></param>
		/// <returns></returns>
		public static string SubstringBehind(this string @object, string match)
		{
			var idx = @object.IndexOf(match);
			if (idx == -1)
				return string.Empty;

			return @object.Substring(idx + match.Length);
		}

		/// <summary>
		/// 从后往前查询，获取指定匹配字符串之后的部分
		/// </summary>
		/// <param name="object"></param>
		/// <param name="match"></param>
		/// <returns></returns>
		public static string SubstringBehindLast(this string @object, string match)
		{
			var idx = @object.LastIndexOf(match);
			if (idx == -1)
				return string.Empty;

			return @object.Substring(idx + match.Length);
		}

		/// <summary>
		/// 从前往后查询，获取指定起始字符串和截止字符串中间部分
		/// </summary>
		/// <param name="object"></param>
		/// <param name="startValue"></param>
		/// <param name="endValue"></param>
		/// <returns></returns>
		public static string Substring(this string @object, string startValue, string endValue)
		{
			var idx = @object.IndexOf(startValue);
			if (idx == -1)
				return string.Empty;

			idx += startValue.Length;
			var eIdx = @object.IndexOf(endValue, idx);
			if (eIdx == -1)
				return string.Empty;

			return @object.Substring(idx, eIdx - idx);
		}

		/// <summary>
		/// 从后往前查询，获取指定起始字符串和截止字符串中间部分
		/// </summary>
		/// <param name="object"></param>
		/// <param name="startValue"></param>
		/// <param name="endValue"></param>
		/// <returns></returns>
		public static string SubstringLast(this string @object, string startValue, string endValue)
		{
			var eIdx = @object.LastIndexOf(endValue);
			if (eIdx == -1)
				return string.Empty;

			var idx = @object.LastIndexOf(startValue, eIdx - endValue.Length);
			if (idx == -1)
				return string.Empty;

			idx += startValue.Length;
			return @object.Substring(idx, eIdx - idx);
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