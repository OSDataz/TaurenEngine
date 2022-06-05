/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/6 23:39:39
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 资源管理器
	/// </summary>
	public class ResourceManager
	{
		internal readonly CacheManager cacheMgr;
		internal readonly LoadManager loadMgr;
		private ResourceConfig _resourceConfig;

		private uint _toId = 0;

		public ResourceManager()
		{
			cacheMgr = new CacheManager();
			loadMgr = new LoadManager();
		}

		#region 加载资源配置
		/// <summary>
		/// 加载资源配置
		/// </summary>
		/// <param name="path"></param>
		/// <param name="loadType"></param>
		/// <param name="downloadType"></param>
		public uint LoadConfig(string path, LoadType loadType, DownloadType downloadType, Action<bool> onLoadComplete)
		{
			return LoadAsync<string>(path, loadType, (result, value) =>
			{
				if (!result)
				{
					Debug.LogError($"资源配置加载失败，Path：{path}");
					return;
				}

				try
				{
					_resourceConfig = JsonHelper.ToObject<ResourceConfig>(value);
				}
				catch (Exception ex)
				{
					Debug.LogError($"资源配置Json解析失败，Path：{path}\n{ex.ToString()}");
				}
				finally
				{
					onLoadComplete?.Invoke(result);
				}
			},
			0, CacheType.None, downloadType);
		}

		private AssetItemConfig GetABItemConfig(string path)
		{
			if (_resourceConfig == null)
				return null;

			return null;
		}
		#endregion

		public T Load<T>(out uint id, string path, LoadType loadType,
			int loadPriority = 10, CacheType cacheType = CacheType.ReferenceDelay,
			DownloadType downloadType = DownloadType.None, Action<float> onLoadProgress = null)
		{
			id = ++_toId;
			if (cacheMgr.TryGetCache<T>(path, id, out var data))
				return data;

			var task = LoadTask<T>.Get();
			task.id = id;
			task.path = path;
			task.loadType = loadType;
			task.loadPriority = loadPriority;
			task.cacheType = cacheType;
			task.downloadType = downloadType;
			task.onLoadProgress = onLoadProgress;

			return loadMgr.Load(task);
		}

		public uint LoadAsync<T>(string path, LoadType loadType, Action<bool, T> onLoadComplete,
			int loadPriority = 10, CacheType cacheType = CacheType.ReferenceDelay,
			DownloadType downloadType = DownloadType.None, Action<float> onLoadProgress = null)
		{
			var id = ++_toId;
			if (cacheMgr.TryGetCache<T>(path, id, out var data))
			{
				onLoadComplete?.Invoke(true, data);
				return id;
			}

			var task = LoadTask<T>.Get();
			task.id = id;
			task.path = path;
			task.loadType = loadType;
			task.loadPriority = loadPriority;
			task.cacheType = cacheType;
			task.downloadType = downloadType;
			task.onLoadComplete = onLoadComplete;
			task.onLoadProgress = onLoadProgress;

			loadMgr.LoadAsync(task);
			return id;
		}

		/// <summary>
		/// 卸载资源
		/// </summary>
		/// <param name="id"></param>
		public void Unload(uint id)
		{
			if (loadMgr.Unload(id))
				return;

			cacheMgr.Release(id);
		}
	}
}