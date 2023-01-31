/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/21 20:17:24
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine
{
	/// <summary>
	/// 资源缓存管理
	/// </summary>
	internal class ResourceCache
	{
		/// <summary> 缓存列表 </summary>
		protected readonly RefrenceList<Asset> cacheList = new RefrenceList<Asset>();

		/// <summary> 回收资源列表 </summary>
		protected readonly List<Asset> recycleAssets = new List<Asset>();

		/// <summary> 缓冲数量 </summary>
		public int capacity = 50;
		/// <summary> 最大缓冲内存 </summary>
		public int memoryCapacity = 100 * 1024 * 1024;

		public bool TryGetAsset(string path)
		{


			return false;
		}
	}
}