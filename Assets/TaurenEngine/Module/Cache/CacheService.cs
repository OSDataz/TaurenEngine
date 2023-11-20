/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:37:03
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.ModCache
{
	/// <summary>
	/// 缓存服务
	/// </summary>
	public class CacheService : ICacheService
	{
		#region 缓存参数
		/// <summary> 最大缓存内存（正式模式无效） </summary>
		public int maxMemorySize = 100 * 1024 * 1024;// 100MB

		/// <summary> 最大缓存资源数 </summary>
		public int maxCapacity = 50;

		/// <summary> 最小访问时间 </summary>
		public float minVisitTime = 3.0f;

		/// <summary> 最大持有无引用资源数 </summary>
		public int maxUnusedCount = 10;

		/// <summary> 触发调用GC，最大移除资源数量 </summary>
		public int maxRemoveGCCount = 20;
		#endregion

		#region 缓存资源
		/// <summary> 缓存列表 </summary>
		private readonly RefrenceList<Asset> _cacheList = new RefrenceList<Asset>();

		/// <summary> 当前缓存内存 </summary>
		public int MemorySize { get; private set; }

		/// <summary> 记录自上次gc后删除的资源数量 </summary>
		private int _removeCountSinceLastGC;
		#endregion

		public CacheService()
		{
			this.InitInterface();
		}

		public bool TryGet(string key, out IAsset asset)
		{
			var tAsset = Find(key);
			asset = tAsset;
			if (tAsset == null)
				return false;

			if (tAsset.HasData)
			{
				tAsset.visitTime = Time.realtimeSinceStartup;
				tAsset.IsWeakReference = false;

				return true;
			}
			else
			{
				Remove(tAsset);
				return false;
			}
		}

		private Asset Find(string key)
		{
			var len = _cacheList.Count;
			for (int i = 0; i < len; ++i)
			{
				if (_cacheList[i].key == key)
					return _cacheList[i];
			}

			return null;
		}

		public void Add(IAsset asset)
		{
			if (asset == null)
				return;

			var tAsset = asset as Asset;
			if (!tAsset.cacheable)
				return;

			if (_cacheList.Contains(tAsset))
			{
				Log.Error($"重复添加资源进缓存，Key：{tAsset.key}");
				return;
			}

			CheckCapacity();

			tAsset.visitTime = Time.realtimeSinceStartup;
			_cacheList.Add(tAsset);

			MemorySize += tAsset.MemorySize;
		}

		public void Remove(IAsset asset)
		{
			if (asset == null)
				return;

			var tAsset = asset as Asset;

			if (_cacheList.Remove(tAsset))
			{
				MemorySize -= tAsset.MemorySize;
			}
		}

		public void Remove(string key)
		{
			var asset = Find(key);
			if (asset == null)
				return;

			Remove(asset);
		}

		public void RemoveAll()
		{
			_cacheList.Clear();

			Resources.UnloadUnusedAssets();
		}

		#region 缓存检测
		private void CheckCapacity()
		{
			// 检测资源数和内存大小
			if (_cacheList.Count <= maxCapacity || MemorySize <= maxMemorySize)
				return;

			var time = Time.realtimeSinceStartup;

			for (int i = _cacheList.Count - 1; i >= 0; --i)
			{
				var asset = _cacheList[i];
				if (asset.HasData)
				{
					asset.IsWeakReference = asset.RefCount <= 1 && time - asset.visitTime > minVisitTime;
				}
				else
				{
					Remove(asset);
					_removeCountSinceLastGC += 1;
				}
			}

			if (_removeCountSinceLastGC > maxRemoveGCCount)
			{
				_removeCountSinceLastGC = 0;
				Resources.UnloadUnusedAssets();
			}
		}
		#endregion
	}
}