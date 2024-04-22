/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/9 18:14:27
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public partial class ResourceService
	{
		#region 检查缓存池
		public bool FindFromCache<T>(IRefrenceContainer container, string path, out T asset)
		{
			if (ICacheService.Instance.TryGet(path, out var cacheAsset))
			{
				if (cacheAsset.TryGetAsset<T>(out var tCacheAsset))
				{
					if (container != null)
						container.Add(cacheAsset);// 将资源添加到引用容器

					asset = tCacheAsset;// 返回缓存资源
				}
				else
				{
					asset = default;
				}

				return true;
			}

			asset = default;
			return false;
		}

		public bool FindFromCache<T>(IRefrenceContainer container, string path, Action<bool, T> onComplete)
		{
			if (ICacheService.Instance.TryGet(path, out var cacheAsset))
			{
				if (cacheAsset.TryGetAsset<T>(out var tCacheAsset))
				{
					if (container != null)
						container.Add(cacheAsset);// 将资源添加到引用容器

					onComplete?.Invoke(true, tCacheAsset);// 返回缓存资源
				}
				else
				{
					onComplete?.Invoke(false, default);
				}

				return true;
			}

			return false;
		}
		#endregion

		#region 加载资源
		public T LoadPure<T>(IRefrenceContainer container, string path, LoadType loadType, bool cache)
		{
			var loadData = ILoadService.Instance.Load(path, loadType);
			if (loadData.Code == 0 && loadData.Asset.TryGetAsset<T>(out var tLoadAsset))
			{
				if (cache)
					ICacheService.Instance.Add(loadData.Asset);// 添加入资源缓存池

				if (container != null)
					container.Add(loadData.Asset);// 将资源添加到引用容器

				return tLoadAsset;// 返回加载资源
			}

			return default;
		}

		public ILoadHandler LoadPure<T>(IRefrenceContainer container, string path, LoadType loadType, bool cache,
			int priority, Action<bool, T> onComplete)
		{
			return ILoadService.Instance.LoadAsync(path, loadType, priority, loadData =>
			{
				if (loadData.Code == 0 && loadData.Asset.TryGetAsset<T>(out var tLoadAsset))
				{
					if (cache)
						ICacheService.Instance.Add(loadData.Asset);// 添加入资源缓存池

					if (container != null)
						container.Add(loadData.Asset);// 将资源添加到引用容器

					onComplete?.Invoke(true, tLoadAsset);// 返回加载资源
				}
				else
				{
					onComplete?.Invoke(false, default);
				}
			});
		}
		#endregion
	}
}