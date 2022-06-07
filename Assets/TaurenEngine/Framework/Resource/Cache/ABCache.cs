/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/7 15:09:38
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// AB包缓存数据
	/// </summary>
	internal class ABCache : CacheBase
	{
		/// <summary>
		/// AB包
		/// </summary>
		public AssetBundle data;
		/// <summary>
		/// AB包配置
		/// </summary>
		public ABConfig config;

		public override void Release()
		{
			data.Unload(false);

			config = null;
		}
	}
}