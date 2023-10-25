/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/18 11:34:57
 *└────────────────────────┘*/

using TaurenEngine.Launch;

namespace TaurenEngine.Hotfix
{
	public interface IAssetCacheService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IAssetCacheService Instance { get; internal set; }

		/// <summary>
		/// 将资源添加进缓存
		/// </summary>
		/// <param name="asset"></param>
		void Add(Asset asset);

		/// <summary>
		/// 将资源移除缓存
		/// </summary>
		/// <param name="asset"></param>
		void Remove(Asset asset);

		/// <summary>
		/// 将资源移除缓存
		/// </summary>
		/// <param name="key"></param>
		void Remove(string key);

		/// <summary>
		/// 尝试获取资源
		/// </summary>
		/// <param name="key"></param>
		/// <param name="asset"></param>
		/// <returns></returns>
		bool TryGet(string key, out Asset asset);

		/// <summary>
		/// 移除所有资源
		/// </summary>
		void RemoveAll();
	}

	public static class IAssetCacheServiceExtension
	{
		public static void InitInterface(this IAssetCacheService @object)
		{
			if (IAssetCacheService.Instance != null)
				Log.Error("IAssetCacheService重复创建实例");

			IAssetCacheService.Instance = @object;
		}
	}
}