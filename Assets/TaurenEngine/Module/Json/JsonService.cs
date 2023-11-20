/* ----------------------------------------------
 * | 作者：彭彬珂
 * | 时间：2023/11/2 21:46:52
 * ----------------------------------------------*/

using TaurenEngine.Core;

namespace TaurenEngine.ModJson
{
	public class JsonService : IJsonService
	{
		public JsonService() 
		{
			this.InitInterface();
		}

		public T ToObject<T>(string value)
		{
			return LitJson.JsonMapper.ToObject<T>(value);
		}

		public string ToJson(object value)
		{
			return LitJson.JsonMapper.ToJson(value);
		}
	}
}