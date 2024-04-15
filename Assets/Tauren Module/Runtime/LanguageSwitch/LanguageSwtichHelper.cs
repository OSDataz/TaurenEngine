/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/29 8:31:05
 *└────────────────────────┘*/

using System;
using System.Runtime.InteropServices;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 繁体简体转换方案二：
	/// 引用：Microsoft.VisualBasic
	/// 繁体转化为简体中文 Strings.StrConv(str, VbStrConv.SimplifiedChinese);
	/// 简体转化为繁体中文 Strings.StrConv(str, VbStrConv.TraditionalChinese);
	/// </summary>
	public static class LanguageSwtichHelper
    {
		private const int LOCALE_SYSTEM_DEFAULT = 0x0800;
		private const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
		private const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int LCMapString(int Locale, int dwMapFlags, string lpSrcStr, int cchSrc, [Out] string lpDestStr, int cchDest);

		/// <summary>
		/// 繁体转化为简体中文
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string ToSimplifiedChinese(string source)
		{
			String target = new String(' ', source.Length);
			LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_SIMPLIFIED_CHINESE, source, source.Length, target, source.Length);
			return target;
		}

		/// <summary>
		/// 简体转化为繁体中文
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string ToTraditionalChinese(string source)
		{
			String target = new String(' ', source.Length);
			LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_TRADITIONAL_CHINESE, source, source.Length, target, source.Length);
			return target;
		}
	}
}