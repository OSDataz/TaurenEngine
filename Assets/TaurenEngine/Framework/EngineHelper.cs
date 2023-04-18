/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/18 20:33:09
 *└────────────────────────┘*/

using TaurenEngine.Core;
using TaurenEngine.Unity;

namespace TaurenEngine.Framework
{
	public static class EngineHelper
	{
		/// <summary>
		/// 【业务调用接口】初始化服务
		/// </summary>
		public static void InitService()
		{
			var serviceMgr = ServiceManager.Instance;

			serviceMgr.Add<ILogService>(new LogService());// 初始化日志服务

			Core.EngineHelper.InitService();// 初始化Core层服务
			Unity.EngineHelper.InitService();// 初始化Unity层服务

			serviceMgr.Add<IResourceService>(new ResourceService());// 初始化资源服务
			serviceMgr.Add<IAudioService>(new AudioService());// 初始化音频服务
			serviceMgr.Add<INetworkService>(new NetworkService());// 初始化网络服务
			serviceMgr.Add<IHotfixService>(new HotfixService());// 初始化热更服务
		}
	}
}