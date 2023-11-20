/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/2 21:44:53
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public interface IJsonService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IJsonService Instance { get; internal set; }

		/// <summary>
		/// 字符串转Json对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		T ToObject<T>(string value);

		/// <summary>
		/// Json对象转字符串
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		string ToJson(object value);
	}

	public static class IJsonServiceExtension
	{
		public static void InitInterface(this IJsonService @object)
		{
			IJsonService.Instance = @object;
		}
	}
}