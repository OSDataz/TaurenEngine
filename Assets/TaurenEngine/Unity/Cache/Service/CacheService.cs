/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/18 11:38:08
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Core;

namespace TaurenEngine.Unity
{
	/// <summary>
	/// 缓存服务
	/// </summary>
	public class CacheService : ICacheService
	{
		/// <summary> 缓存列表 </summary>
		private readonly RefrenceList<Asset> _cacheList = new RefrenceList<Asset>();

		/// <summary> 回收资源列表 </summary>
		private readonly List<Asset> _recycleAssets = new List<Asset>();

		/// <summary> 缓冲数量 </summary>
		public int capacity = 50;
		/// <summary> 最大缓冲内存 </summary>
		public int memoryCapacity = 100 * 1024 * 1024;

		public bool TryGetAsset(string path)
		{


			return false;
		}

		public CacheService()
		{
			this.InitInterface();
		}
	}
}