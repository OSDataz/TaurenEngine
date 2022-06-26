/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 11:52:07
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Framework
{
	internal abstract class Cache
	{
		/// <summary>
		/// 资源路径
		/// </summary>
		public string path;
		/// <summary>
		/// 缓存类型
		/// </summary>
		public CacheType cacheType;

		/// <summary>
		/// 资源持有对象
		/// </summary>
		public readonly List<uint> holders = new List<uint>();

		/// <summary>
		/// 最后使用时间
		/// </summary>
		public float lastUseTime;
		/// <summary>
		/// 是否立即释放
		/// </summary>
		public bool IsReleaseImmediate { get; private set; }

		public bool CheckRelease(uint id)
		{
			if (holders.Remove(id))
			{
				if (cacheType == CacheType.Persistent)
				{
					IsReleaseImmediate = false;
				}
				else
				{
					if (holders.Count == 0)
					{
						if (cacheType == CacheType.Reference)
						{
							Release();
							IsReleaseImmediate = true;
						}
						else
						{
							lastUseTime = Time.time;
							IsReleaseImmediate = false;
						}
					}
				}

				return true;
			}

			return false;
		}

		/// <summary>
		/// 释放资源
		/// </summary>
		public abstract void Release();
	}
}