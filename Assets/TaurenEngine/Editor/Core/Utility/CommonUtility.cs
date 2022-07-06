/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/23 20:59:23
 *└────────────────────────┘*/

using System.Text;

namespace TaurenEngine.Editor
{
	public static class CommonUtility
	{
		public static Encoding ToEncoding(EncodingType encoding)
		{
			if (encoding == EncodingType.UTF8) return Encoding.UTF8;
			if (encoding == EncodingType.GB2312) return Encoding.GetEncoding("gb2312");
			return Encoding.Default;
		}
	}
}