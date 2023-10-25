/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.0
 *│　Time    ：2023/10/24 20:58:11
 *└────────────────────────┘*/

using TaurenEngine.Launch;

namespace TaurenEngine.Hotfix
{
	public static class StartupHelper
	{
		/// <summary>
		/// 初始化服务
		/// </summary>
		public static void InitServier()
		{
			var serviceMgr = ServiceManager.Instance;

			// 基础服务
			serviceMgr.Add<IPoolService>(new PoolService());// 初始化对象池服务

			// 模块服务
			serviceMgr.Add<IResourceService>(new ResourceService());// 初始化资源服务
			serviceMgr.Add<IHotfixService>(new HotfixService());// 初始化热更服务
		}
	}
}