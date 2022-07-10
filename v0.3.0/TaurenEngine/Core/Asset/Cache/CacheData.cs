/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/29 14:23:02
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Core
{
	internal class CacheData
	{
		/// <summary>
		/// 资源路径数据
		/// </summary>
		public LoadPath LoadPath { get; private set; }
		/// <summary>
		/// 缓存资源
		/// </summary>
		public LoadRes LoadRes { get; private set; }

		/// <summary>
		/// 永久缓存（只要有一个请求永久缓存，该资源就会永久缓存）
		/// </summary>
		public bool IsPermanent { get; private set; }
		/// <summary>
		/// 资源持有对象
		/// </summary>
		public readonly List<int> holders = new List<int>();
		/// <summary>
		/// 上次使用时间
		/// </summary>
		public float LastUseTime { get; private set; }

		public void SetData(LoadTask loadTask)
		{
			LoadPath = loadTask.path;
			LoadRes = loadTask.ResData;

			if (loadTask.IsPermanent)
				IsPermanent = true;

			holders.AddUnique(loadTask.id);
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

		public void Release()
		{
			LoadRes = null;
			LoadPath = null;

			holders.Clear();
		}
	}
}