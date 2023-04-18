/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/18 20:32:20
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.Unity
{
	public static class EngineHelper
	{
		/// <summary>
		/// 初始化服务
		/// </summary>
		public static void InitService()
		{
			var serviceMgr = ServiceManager.Instance;

			serviceMgr.Add<ICacheService>(new CacheService());// 初始化缓存服务
		}
	}
}