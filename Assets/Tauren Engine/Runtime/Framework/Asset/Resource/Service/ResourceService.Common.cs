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
		public bool FindFromCache<T>(IRefrenceContainer container, string path, out T asset) where T : UnityEngine.Object
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
					asset = null;
				}

				return true;
			}

			asset = null;
			return false;
		}

		public bool FindFromCache<T>(IRefrenceContainer container, string path, Action<bool, T> onComplete) where T : UnityEngine.Object
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
					onComplete?.Invoke(false, null);
				}

				return true;
			}

			return false;
		}
		#endregion

		#region 加载资源
		public T LoadPure<T>(IRefrenceContainer container, string path, LoadType loadType, bool cache) where T : UnityEngine.Object
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

			return null;
		}

		public ILoadHandler LoadPure<T>(IRefrenceContainer container, string path, LoadType loadType, bool cache,
			int priority, Action<bool, T> onComplete) where T : UnityEngine.Object
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
					onComplete?.Invoke(false, null);
				}
			});
		}
		#endregion
	}
}