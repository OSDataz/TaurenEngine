/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 11:13:42
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine.Framework
{
	public class ResourcesManager : ResourceAdapt
	{
		public T Load<T>(string path) where T : UnityEngine.Object
		{
			return Resources.Load<T>(path);
		}

		public T Load<T>(string path, CacheType cacheType, out uint id) where T : UnityEngine.Object
		{
			id = ToId();
			if (cacheMgr.TryGetCache<T>(path, id, out var data))
				return data;

			data = Resources.Load<T>(path);
			if (data != null && cacheType != CacheType.None)
			{
				cacheMgr.AddCache(data, id, path, LoadType.Resources, cacheType);
			}

			return data;
		}

		public uint LoadAsync<T>(string path, Action<bool, T> onLoadComplete, int loadPriority = 10, CacheType cacheType = CacheType.ReferenceDelay) where T : UnityEngine.Object
		{
			var id = ToId();
			if (cacheMgr.TryGetCache<T>(path, id, out var data, out var isTypeError))
			{
				onLoadComplete?.Invoke(!isTypeError, data);
				return id;
			}

			var task = AsyncLoadTask<T>.Get();
			task.id = id;
			task.path = path;
			task.loadType = LoadType.Resources;
			task.loadPriority = loadPriority;
			task.cacheType = cacheType;
			task.onLoadComplete = onLoadComplete;

			asyncLoadMgr.Load(task);
			return id;
		}

		internal override void Unload<T>(T asset)
		{
			Resources.UnloadAsset(asset);
		}
	}
}