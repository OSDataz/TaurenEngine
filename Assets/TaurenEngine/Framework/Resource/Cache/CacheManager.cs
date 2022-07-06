/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 11:59:34
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 缓存管理器
	/// 
	/// todo：资源大小，如果资源缓存比较少，就不进行自动清理
	/// </summary>
	internal class CacheManager
	{
		private readonly List<AssetCache> _caches;
		private readonly List<ABCache> _abCaches;

		public CacheManager()
		{
			_caches = new List<AssetCache>();
			_abCaches = new List<ABCache>();
		}

		#region 添加缓存资源
		/// <summary>
		/// 添加缓存
		/// </summary>
		/// <param name="loadTask"></param>
		public void AddCache(AsyncLoadTask loadTask)
		{
			if (loadTask.isABPack)
				AddABCache(loadTask.data as AssetBundle, loadTask.abPackConfig, loadTask.id, loadTask.path, loadTask.cacheType);
			else
				AddCache(loadTask.data, loadTask.id, loadTask.path, loadTask.loadType, loadTask.cacheType);
		}

		public void AddCache(UnityEngine.Object data, uint id, string path, LoadType loadType, CacheType cacheType)
		{
			var cache = new AssetCache()
			{
				path = path,
				cacheType = cacheType,

				data = data,
				loadType = loadType
			};
			cache.holders.Add(id);

			_caches.Add(cache);
		}

		public void AddABCache(AssetBundle data, ABConfig config, uint id, string path, CacheType cacheType)
		{
			var cache = new ABCache()
			{
				path = path,
				cacheType = cacheType,

				data = data,
				config = config
			};
			cache.holders.Add(id);

			_abCaches.Add(cache);
		}
		#endregion

		#region 获取缓存资源
		/// <summary>
		/// 获取缓存
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path"></param>
		/// <param name="id"></param>
		/// <param name="data"></param>
		/// <param name="isTypeError"></param>
		/// <returns></returns>
		public bool TryGetCache<T>(string path, uint id, out T data, out bool isTypeError)
		{
			var cache = _caches.Find(item => item.path == path);
			if (cache != null)
			{
				cache.holders.Add(id);
				if (cache.data is T assetData)
				{
					isTypeError = false;
					data = assetData;
				}
				else
				{
					isTypeError = true;
					data = default;
					Debugger.Error($"资源存在但获取类型错误，Path：{path}, AssetType：{cache.data.GetType()} GetType：{typeof(T)}");
				}

				return true;
			}

			data = default;
			isTypeError = false;
			return false;
		}

		public bool TryGetCache<T>(string path, uint id, out T data)
		{
			var cache = _caches.Find(item => item.path == path);
			if (cache != null)
			{
				cache.holders.Add(id);
				if (cache.data is T assetData)
				{
					data = assetData;
				}
				else
				{
					data = default;
					Debugger.Error($"资源存在但获取类型错误，Path：{path}, AssetType：{cache.data.GetType()} GetType：{typeof(T)}");
				}

				return true;
			}

			data = default;
			return false;
		}

		/// <summary>
		/// 获取AB包缓存
		/// </summary>
		/// <param name="path"></param>
		/// <param name="id"></param>
		/// <param name="ab"></param>
		/// <returns></returns>
		public bool TryGetABCache(string path, uint id, out AssetBundle ab)
		{
			var cache = _abCaches.Find(item => item.path == path);
			if (cache != null)
			{
				cache.holders.Add(id);
				ab = cache.data;
				return true;
			}

			ab = null;
			return false;
		}

		public bool TryGetABCache(string path, out AssetBundle ab)
		{
			var cache = _abCaches.Find(item => item.path == path);
			if (cache != null)
			{
				ab = cache.data;
				return true;
			}

			ab = null;
			return false;
		}
		#endregion

		#region 释放缓存资源
		/// <summary>
		/// 释放缓存
		/// </summary>
		/// <param name="id"></param>
		public void Release(uint id)
		{
			var needAutoRealease = false;

			Release(id, _caches, ref needAutoRealease);
			Release(id, _abCaches, ref needAutoRealease);

			if (!alwaysAutoRelease && needAutoRealease)
				StartAutoRelease();
		}

		private void Release<T>(uint id, List<T> caches, ref bool needAutoRealease) where T : Cache
		{
			for (int i = caches.Count - 1; i >= 0; --i)
			{
				if (caches[i].CheckRelease(id))
				{
					if (caches[i].IsReleaseImmediate)
						caches.RemoveAt(i);
					else
						needAutoRealease = true;
				}
			}
		}

		/// <summary>
		/// 【谨慎使用】强制释放所有缓存资源
		/// </summary>
		public void ReleaseAll()
		{
			foreach (var data in _caches)
			{
				data.Release();
			}
			_caches.Clear();

			foreach (var data in _abCaches)
			{
				data.Release();
			}
			_abCaches.Clear();

			Resources.UnloadUnusedAssets();
			AssetBundle.UnloadAllAssetBundles(true);

			if (!alwaysAutoRelease)
				StopAutoRelease();
		}
		#endregion

		#region 自动释放内存资源
		/// <summary> 一直开启自动检测释放资源 </summary>
		public bool alwaysAutoRelease;
		/// <summary> 检测间隔时间 </summary>
		public float autoReleaseInterval;

		private IFrame _timer;

		/// <summary>
		/// 开始自动释放缓存
		/// </summary>
		public void StartAutoRelease()
		{
			_timer ??= TaurenFramework.Frame.GetTimer(autoReleaseInterval, OnCheckRelease);
			_timer.Start();
		}

		/// <summary>
		/// 停止自动释放缓存
		/// </summary>
		public void StopAutoRelease()
		{
			_timer?.Stop();
		}

		private void OnCheckRelease()
		{
			var needAutoRealease = false;
			var time = Time.time;

			CheckRelease(_caches, time, ref needAutoRealease);
			CheckRelease(_abCaches, time, ref needAutoRealease);

			if (!alwaysAutoRelease && !needAutoRealease)
				StopAutoRelease();
		}

		private void CheckRelease<T>(List<T> caches, float time, ref bool needAutoRealease) where T : Cache
		{
			for (int i = caches.Count - 1; i >= 0; --i)
			{
				var data = caches[i];
				if (data.cacheType != CacheType.ReferenceDelay)
					continue;

				if (data.holders.Count > 0)
				{
					needAutoRealease = true;
					continue;
				}

				if (time - data.lastUseTime > autoReleaseInterval)
				{
					caches[i].Release();
					caches.RemoveAt(i);
				}
			}
		}
		#endregion
	}
}