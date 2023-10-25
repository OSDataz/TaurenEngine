/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.0
 *│　Time    ：2023/10/24 20:56:46
 *└────────────────────────┘*/

using TaurenEngine.Launch;

namespace TaurenEngine.Runtime
{
	public static class StartupHelper
	{
		/// <summary>
		/// 初始化服务
		/// </summary>
		public static void InitServier()
		{
			var serviceMgr = ServiceManager.Instance;

			// 模块服务
			serviceMgr.Add<IAudioService>(new AudioService());// 初始化音频服务
			serviceMgr.Add<IUIService>(new UIService());// 初始化UI服务
			serviceMgr.Add<INetworkService>(new NetworkService());// 初始化网络服务
		}
	}
}