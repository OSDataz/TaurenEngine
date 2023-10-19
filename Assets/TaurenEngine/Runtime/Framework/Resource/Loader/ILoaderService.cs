/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/29 20:33:52
 *└────────────────────────┘*/

namespace TaurenEngine.Runtime.Framework
{
	public interface ILoaderService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ILoaderService Instance { get; internal set; }

		/// <summary>
		/// 同步加载
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="loadData"></param>
		/// <returns></returns>
		T Load<T>(LoadData loadData) where T : UnityEngine.Object;

		/// <summary>
		/// 异步加载
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="loadData"></param>
		void LoadAsync<T>(LoadData loadData) where T : UnityEngine.Object;
	}

	public static class ILoaderServiceExtension
	{
		public static void InitInterface(this ILoaderService @object)
		{
			if (ILoaderService.Instance != null)
				Log.Error("ILoaderService重复创建实例");

			ILoaderService.Instance = @object;
		}
	}
}