/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/28 17:38:07
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Framework
{
	internal class ABLoader : LoaderBase
	{
		#region 对象池
		private static ObjectPool<ABLoader> pool = new ObjectPool<ABLoader>();
		public static ABLoader Get() => pool.Get();

		/// <summary> 回收 </summary>
		public void Recycle() => pool.Add(this);
		#endregion

		private bool TryLoadAB(string path, out AssetBundle data)
		{
			data = AssetBundle.LoadFromFile(Application.persistentDataPath + path);
			if (data != null)
				return true;

			data = AssetBundle.LoadFromFile(Application.streamingAssetsPath + path);
			return data != null;
		}

		public override T Load<T>(string path)
		{
			if (!TryGetAssetConfig(path, out var assetConfig))
			{
				Logger.Warning($"未找到资源配置，Path：{path}");
				return null;
			}

			if (string.IsNullOrEmpty(assetConfig.abName))
			{
				Logger.Warning($"资源配置AB包名字为空，Path：{path}");
				return null;
			}

			if (CacheMgr.TryGetABCache(path, out var ab))
			{
				// AB包有缓存
				return ab.LoadAsset<T>(path);
			}
			else
			{
				Logger.Warning($"资源所属AB包未加载，请使用另一个带缓存接口尝试加载。Path：{path}");
				return null;
			}
		}

		public override T Load<T>(uint id, string path, CacheType cacheType)
		{
			//var assetConfig = TaurenFramework.Resource.resourceConfig?.assetList?.Find(item => item.path == path);
			//if (assetConfig != null)
			//{
			//	if (!string.IsNullOrEmpty(assetConfig.abPackName))
			//	{
			//		var abPackConfig = TaurenFramework.Resource.resourceConfig.abList?.Find(item => item.name == assetConfig.abPackName);
			//		if (abPackConfig != null)
			//		{
			//			// AB包中资源

			//		}
			//		else
			//		{
			//			Debug.LogError($"资源配置错误，资源找不到对应的AB包。Asset：{path} {assetConfig.guid} ABPack：{assetConfig.abPackName}");
			//			return default;
			//		}
			//	}
			//}

			//AssetBundle

			return default;
		}

		public override void LoadAsync<T>(LoadTaskAsync<T> loadTask)
		{
			
		}
	}
}