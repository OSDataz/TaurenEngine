/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/7 20:03:22
 *└────────────────────────┘*/

using TaurenEngine.Runtime.Framework;

namespace TaurenEngine.Runtime.Launch
{
	public static class LaunchHelper
	{
		/// <summary>
		/// 初始化服务
		/// </summary>
		public static void InitServier()
		{
			var serviceMgr = ServiceManager.Instance;

			// 基础服务
			serviceMgr.Add<ILogService>(new LogService());// 初始化日志服务
			serviceMgr.Add<IPoolService>(new PoolService());// 初始化对象池服务

			// 模块服务
			serviceMgr.Add<IResourceService>(new ResourceService());// 初始化资源服务
			serviceMgr.Add<IAudioService>(new AudioService());// 初始化音频服务
			serviceMgr.Add<INetworkService>(new NetworkService());// 初始化网络服务
			serviceMgr.Add<IHotfixService>(new HotfixService());// 初始化热更服务
		}
	}
}