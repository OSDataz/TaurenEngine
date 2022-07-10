/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/24 17:17:16
 *└────────────────────────┘*/

using LitJson;

namespace TaurenEngine.Json
{
	public static class JsonHelper
	{
		public static T ToObject<T>(string value)
		{
			return JsonMapper.ToObject<T>(value);
		}

		public static JsonData ToObject(string value)
		{
			return JsonMapper.ToObject(value);
		}

		public static string ToJson(object value)
		{
			return JsonMapper.ToJson(value);
		}
	}
}