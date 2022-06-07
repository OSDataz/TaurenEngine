﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/7 0:06:19
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 缓存数据
	/// </summary>
	internal class Cache : CacheBase
	{
		/// <summary>
		/// 加载类型
		/// </summary>
		public LoadType loadType;
		/// <summary>
		/// 缓存资源
		/// </summary>
		public UnityEngine.Object data;

		/// <summary>
		/// 释放资源
		/// </summary>
		public override void Release()
		{
			if (loadType == LoadType.Resources)
				Resources.UnloadAsset(data);

			data = null;
		}
	}
}