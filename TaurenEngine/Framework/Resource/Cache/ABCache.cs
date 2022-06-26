/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 11:55:09
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// AB包缓存数据
	/// </summary>
	internal class ABCache : Cache
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
			data = null;

			config = null;
		}
	}
}