/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.6.0
 *│　Time    ：2022/6/26 17:41:35
 *└────────────────────────┘*/

namespace TaurenEditor
{
	public static class StringExtension
	{
		public static string ToUpperFirst(this string value)
		{
			return value[0].ToString().ToUpper() + value.Substring(1);
		}
	}
}