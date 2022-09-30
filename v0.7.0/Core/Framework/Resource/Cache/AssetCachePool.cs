/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/8/2 20:09:16
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine
{
	/// <summary>
	/// 资源缓冲池
	/// </summary>
	public class AssetCachePool : ObjectCachePool<Asset>
	{
		/// <summary> 回收资源列表 </summary>
		protected readonly List<Asset> recycleAssets = new List<Asset>();

		/// <summary> 最大缓冲内存 </summary>
		public int memoryCapacity = 100 * 1024 * 1024;

		/// <summary> 最小访问时间 </summary>
		public float minVisitTime = 3.0f;

		/// <summary> 持有无引用资源的数量 </summary>
		public int persistUnusedCount = 10;

		/// <summary> 记录自上次gc后删除的资源最大数量 </summary>
		public int removeMaxCountSinceLastGC;

		/// <summary> 记录自上次gc后删除的资源数量 </summary>
		private int _removeCountSinceLastGC;

		public override void AddToCache(Asset item)
		{
			if (item == null || !item.cacheable)
				return;

			item.visitTime = DateHelper.RealtimeSinceStartup;
			base.AddToCache(item);
		}

		protected void RemoveRefObject(Asset asset, int index)
		{
			RefObjectList.RemoveAt(index);
			asset.DelRefCount();
		}

		protected override void CheckCapacity()
		{
			var clearUnused = false;

			while (CheckCacheFull())
			{
				var index = FindOldestIndex();
				if (index >= 0)
				{
					var asset = RefObjectList[index];
					recycleAssets.Add(asset);

					RemoveRefObject(asset, index);

					clearUnused = true;
				}
				else
					break;
			}

			if (clearUnused)
			{
				// 清理没有引用资源
				int unusedCount = 0;

				for (int i = recycleAssets.Count - 1; i >= 0; --i)
				{
					var recycleAsset = recycleAssets[i];
					if (recycleAsset.HasData)
					{
						if (recycleAsset.RefCount == 0)
						{
							if (++unusedCount > persistUnusedCount)
							{
								recycleAssets.RemoveAt(i);
								recycleAsset.Destroy();
								_removeCountSinceLastGC += 1;
							}
						}
					}
					else
					{
						// 已经没有资源
						recycleAssets.RemoveAt(i);
					}
				}

				if (_removeCountSinceLastGC > removeMaxCountSinceLastGC)
				{
					_removeCountSinceLastGC = 0;
					Resources.UnloadUnusedAssets();// 卸载所有没有引用的资源
				}
			}
		}

		protected override bool CheckCacheFull()
		{
			return GetTotalMemroy() > memoryCapacity || base.CheckCacheFull();
		}

		/// <summary>
		/// 获取总占用内存
		/// </summary>
		/// <returns></returns>
		protected int GetTotalMemroy()
		{
			var total = 0;
			for (var i = RefObjectList.Count - 1; i >= 0; --i)
			{
				total += RefObjectList[i].MemorySize;
			}

			return total;
		}

		protected override int FindOldestIndex()
		{
			var time = DateHelper.RealtimeSinceStartup;

			// 优先查找没有被引用的元素
			var len = RefObjectList.Count;
			for (int i = 0; i < len; ++i)
			{
				var asset = RefObjectList[i];
				if (asset.RefCount <= 1 && time - asset.visitTime > minVisitTime)
					return i;
			}

			// 查找最早加载的资源，强制移除缓存，但不会强制卸载，主要考虑脱管资源
			int oldestIndex = -1;
			for (int i = 0; i < len; ++i)
			{
				var asset = RefObjectList[i];
				if (time > asset.visitTime)
				{
					time = asset.visitTime;
					oldestIndex = i;
				}
			}

			return oldestIndex;
		}

		public override void Clear()
		{
			// 清理托管队列
			for (int i = RefObjectList.Count - 1; i >= 0; --i)
			{
				var asset = RefObjectList[i];
				recycleAssets.Add(asset);
				RemoveRefObject(asset, i);
				asset.DelRefCount();
			}

			// 清理回收队列
			for (int i = recycleAssets.Count - 1; i >= 0; --i)
			{
				var recycleAsset = recycleAssets[i];
				if (recycleAsset.HasData)
				{
					if (recycleAsset.RefCount == 0)
					{
						recycleAssets.RemoveAt(i);
						recycleAsset.Destroy();
					}
				}
				else
				{
					recycleAssets.RemoveAt(i);
				}
			}

			Resources.UnloadUnusedAssets();
			GC.Collect();
		}
	}
}