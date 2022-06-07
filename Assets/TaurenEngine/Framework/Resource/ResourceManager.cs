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
		internal ResourceConfig resourceConfig;

		private uint _toId = 0;

		public ResourceManager()
		{
			cacheMgr = new CacheManager();
			loadMgr = new LoadManager();
		}

		#region 加载AB配置
		/// <summary>
		/// 加载AB资源配置
		/// </summary>
		/// <param name="path"></param>
		/// <param name="loadType"></param>
		/// <param name="downloadType"></param>
		public uint LoadABConfig(string path, LoadType loadType, DownloadType downloadType, Action<bool> onLoadComplete)
		{
			return LoadAsync<TextAsset>(path, loadType, (result, value) =>
			{
				if (!result)
				{
					Debug.LogError($"资源配置加载失败，Path：{path}");
					return;
				}

				try
				{
					resourceConfig = JsonHelper.ToObject<ResourceConfig>(value.text);
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
			10, CacheType.None, downloadType);
		}
		#endregion

		#region 同步加载
		/// <summary>
		/// 【不推荐，脱管】同步加载资源。
		/// <para>默认不缓存，无单独依赖资源，不加载AB包，不下载。</para>
		/// <para>资源自行管理，不需要调用管理器释放。</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path"></param>
		/// <param name="loadType"></param>
		/// <returns></returns>
		public T Load<T>(string path, LoadType loadType) where T : UnityEngine.Object
		{
			return loadMgr.Load<T>(0, path, loadType, CacheType.None);
		}

		/// <summary>
		/// 同步加载资源
		/// <para>默认不下载</para>
		/// <para>注意：不管是否缓存，释放资源一定要调用管理器释放<c>Unload</c>。</para>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path"></param>
		/// <param name="loadType"></param>
		/// <param name="id"></param>
		/// <param name="cacheType"></param>
		/// <returns></returns>
		public T Load<T>(string path, LoadType loadType, out uint id, CacheType cacheType = CacheType.ReferenceDelay) where T : UnityEngine.Object
		{
			id = ++_toId;
			if (cacheMgr.TryGetCache<T>(path, id, out var data))
				return data;

			return loadMgr.Load<T>(id, path, loadType, cacheType);
		}
		#endregion

		#region 异步加载
		public uint LoadAsync<T>(string path, LoadType loadType, Action<bool, T> onLoadComplete,
			int loadPriority = 10, CacheType cacheType = CacheType.ReferenceDelay,
			DownloadType downloadType = DownloadType.None, Action<float> onLoadProgress = null) where T : UnityEngine.Object
		{
			var id = ++_toId;
			if (cacheMgr.TryGetCache<T>(path, id, out var data, out var isTypeError))
			{
				onLoadComplete?.Invoke(!isTypeError, data);
				return id;
			}

			var task = LoadTaskAsync<T>.Get();
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
		#endregion

		#region 卸载资源
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
		#endregion
	}
}