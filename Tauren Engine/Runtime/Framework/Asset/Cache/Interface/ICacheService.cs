/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:36:27
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public interface ICacheService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ICacheService Instance { get; internal set; }

		/// <summary>
		/// 尝试获取资源
		/// </summary>
		/// <param name="key"></param>
		/// <param name="asset"></param>
		/// <returns></returns>
		bool TryGet(string key, out IAsset asset);

		/// <summary>
		/// 将资源添加进缓存
		/// </summary>
		/// <param name="asset"></param>
		void Add(IAsset asset);

		/// <summary>
		/// 将资源移除缓存
		/// </summary>
		/// <param name="asset"></param>
		void Remove(IAsset asset);

		/// <summary>
		/// 移除所有资源
		/// </summary>
		void RemoveAll();
	}

	public static class ICacheServiceExtension
	{
		public static void InitInterface(this ICacheService @object)
		{
			if (ICacheService.Instance != null)
				Log.Error("ICacheService重复创建实例");

			ICacheService.Instance = @object;
		}
	}
}