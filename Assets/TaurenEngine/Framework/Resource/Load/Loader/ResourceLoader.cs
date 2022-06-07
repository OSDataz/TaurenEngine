/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/28 17:37:52
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Framework
{
	internal class ResourceLoader : LoaderBase
	{
		#region 对象池
		private static ObjectPool<ResourceLoader> pool = new ObjectPool<ResourceLoader>();
		public static ResourceLoader Get() => pool.Get();

		/// <summary> 回收 </summary>
		public void Recycle() => pool.Add(this);
		#endregion

		public override T Load<T>(string path)
		{
			return Resources.Load<T>(path);
		}

		public override T Load<T>(uint id, string path, CacheType cacheType)
		{
			var data = Resources.Load<T>(path);
			if (data != null && cacheType != CacheType.None)
			{
				CacheMgr.AddCache(data, id, path, LoadType.Resources, cacheType);
			}

			return data;
		}

		public override void LoadAsync<T>(LoadTaskAsync<T> loadTask)
		{

		}
	}
}