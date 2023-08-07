/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.6.0
 *│　Time    ：2022/6/26 17:41:35
 *└────────────────────────┘*/

namespace TaurenEngine.Editor
{
	public static class StringExtension
	{
		/// <summary>
		/// 将首字母变大写
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToUpperFirst(this string value)
		{
			return value[0].ToString().ToUpper() + value.Substring(1);
		}
	}
}