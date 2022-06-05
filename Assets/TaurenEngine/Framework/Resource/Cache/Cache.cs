/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/7 0:06:19
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 缓存数据
	/// </summary>
	internal class Cache
	{
		/// <summary>
		/// 资源路径
		/// </summary>
		public string Path { get; private set; }
		/// <summary>
		/// 缓存资源
		/// </summary>
		public Asset Asset { get; private set; }

		/// <summary>
		/// 缓存类型
		/// </summary>
		public CacheType CacheType { get; private set; }
		/// <summary>
		/// 资源持有对象
		/// </summary>
		public readonly List<uint> holders = new List<uint>();
		/// <summary>
		/// 最后使用时间
		/// </summary>
		public float LastUseTime { get; private set; }

		/// <summary>
		/// 是否立即释放
		/// </summary>
		public bool IsReleaseImmediate { get; private set; }

		public void SetData(LoadTask loadTask)
		{
			Path = loadTask.path;
			Asset = loadTask.asset;
			CacheType = loadTask.cacheType;
			holders.Add(loadTask.id);
		}

		public bool CheckRelease(uint id)
		{
			if (holders.Remove(id))
			{
				if (CacheType == CacheType.Persistent)
				{
					IsReleaseImmediate = false;
				}
				else
				{
					if (holders.Count == 0)
					{
						if (CacheType == CacheType.Reference)
						{
							Release();
							IsReleaseImmediate = true;
						}
						else
						{
							LastUseTime = Time.time;
							IsReleaseImmediate = false;
						}
					}
				}

				return true;
			}

			return false;
		}

		public void Release()
		{
			Asset.Release();
			Asset = null;
		}
	}
}