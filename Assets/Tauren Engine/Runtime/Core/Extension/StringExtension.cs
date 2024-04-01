/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 11:01:31
 *└────────────────────────┘*/

using System.Text.RegularExpressions;

namespace Tauren.Core.Runtime
{
	public static class StringExtension
	{
		/// <summary>
		/// 截取指定字符之前部分，从前往后查询
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
		/// 截取指定字符之前的部分，从后往前查询
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
		/// 截取指定字符之后的部分，从前往后查
		/// </summary>
		/// <param name="object"></param>
		/// <param name="match"></param>
		/// <returns></returns>
		public static string SubstringAfter(this string @object, string match)
		{
			var idx = @object.IndexOf(match);
			if (idx == -1)
				return string.Empty;

			return @object.Substring(idx + match.Length);
		}

		/// <summary>
		/// 截取指定字符之后的部分，从后往前查询
		/// </summary>
		/// <param name="object"></param>
		/// <param name="match"></param>
		/// <returns></returns>
		public static string SubstringAfterLast(this string @object, string match)
		{
			var idx = @object.LastIndexOf(match);
			if (idx == -1)
				return string.Empty;

			return @object.Substring(idx + match.Length);
		}

		/// <summary>
		/// 截取两个指定字符之前的部分，从前往后查询
		/// </summary>
		/// <param name="object"></param>
		/// <param name="startMatch"></param>
		/// <param name="endMatch"></param>
		/// <returns></returns>
		public static string SubstringBetween(this string @object, string startMatch, string endMatch)
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

		/// <summary>
		/// 截取两个指定字符之前的部分，从后往前查询
		/// </summary>
		/// <param name="object"></param>
		/// <param name="startMatch"></param>
		/// <param name="endMatch"></param>
		/// <returns></returns>
		public static string SubstringBetweenLast(this string @object, string startMatch, string endMatch)
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