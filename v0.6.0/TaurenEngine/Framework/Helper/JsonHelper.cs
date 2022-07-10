/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/4 10:21:27
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	public static class JsonHelper
	{
		public static T ToObject<T>(string value)
		{
			return LitJson.JsonMapper.ToObject<T>(value);
		}

		public static string ToJson(object value)
		{
			return LitJson.JsonMapper.ToJson(value);
		}
	}
}