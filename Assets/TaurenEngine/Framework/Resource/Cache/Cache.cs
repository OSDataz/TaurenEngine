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
		/// 缓存资源
		/// </summary>
		public Asset Asset { get; private set; }

		/// <summary>
		/// 持久化（永久）缓存（只要有一个请求永久缓存，该资源就会永久缓存），可以手动清理
		/// </summary>
		public bool Persistent { get; private set; }
		/// <summary>
		/// 资源持有对象
		/// </summary>
		public readonly List<int> holders = new List<int>();
		/// <summary>
		/// 最后使用时间
		/// </summary>
		public float LastUseTime { get; private set; }

		public void SetData()
		{

		}

		public bool Release(int id)
		{
			if (holders.Remove(id))
			{
				if (holders.Count == 0)
					LastUseTime = Time.time;

				return true;
			}
			else
				return false;
		}
	}
}