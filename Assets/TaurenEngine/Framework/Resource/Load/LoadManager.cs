/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/6 23:54:27
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 加载管理器
	/// </summary>
	internal class LoadManager
	{
		#region 同步加载
		public T Load<T>(uint id, string path, LoadType loadType, CacheType cacheType) where T : UnityEngine.Object
		{
			if (loadType == LoadType.AssetBundle)
			{
				var loader = ABLoader.Get();
				var data = loader.Load<T>(id, path, cacheType);
				loader.Recycle();
				return data;
			}
			else if (loadType == LoadType.File)
			{
				var loader = FileLoader.Get();
				var data = loader.Load<T>(id, path, cacheType);
				loader.Recycle();
				return data;
			}
			else if (loadType == LoadType.Resources)
			{
				var loader = ResourceLoader.Get();
				var data = loader.Load<T>(id, path, cacheType);
				loader.Recycle();
				return data;
			}
			else
				return null;
		}


		#endregion

		#region 异步加载
		public void LoadAsync<T>(LoadTaskAsync<T> loadTask) where T : UnityEngine.Object
		{
			
		}
		#endregion

		#region 释放资源
		public bool Unload(uint id)
		{
			return false;
		}
		#endregion
	}
}