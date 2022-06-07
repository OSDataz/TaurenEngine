/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/28 17:37:46
 *└────────────────────────┘*/

using TaurenEngine.Core;
using TaurenEngine.Unity;
using UnityEngine;

namespace TaurenEngine.Framework
{
	internal class FileLoader : LoaderBase
	{
		#region 对象池
		private static ObjectPool<FileLoader> pool = new ObjectPool<FileLoader>();
		public static FileLoader Get() => pool.Get();

		/// <summary> 回收 </summary>
		public void Recycle() => pool.Add(this);
		#endregion

		public override T Load<T>(string path)
		{
			if (FileEx.LoadObject(Application.persistentDataPath + path, out var objectData))
			{
				if (objectData is T typeData)
					return typeData;

				Logger.Error($"资源获取类型错误，Path：{path}, AssetType：{objectData?.GetType()} GetType：{typeof(T)}");
			}
			else if (FileEx.LoadObject(Application.streamingAssetsPath + path, out objectData))
			{
				if (objectData is T typeData)
					return typeData;

				Logger.Error($"资源获取类型错误，Path：{path}, AssetType：{objectData?.GetType()} GetType：{typeof(T)}");
			}

			Logger.Warning($"FileLoader资源加载失败，Path：{path}");
			return null;
		}

		public override T Load<T>(uint id, string path, CacheType cacheType)
		{
			var data = Load<T>(path);
			if (data != null && cacheType != CacheType.None)
			{
				CacheMgr.AddCache(data, id, path, LoadType.File, cacheType);
			}

			return data;
		}

		public override void LoadAsync<T>(LoadTaskAsync<T> loadTask)
		{

		}
	}
}