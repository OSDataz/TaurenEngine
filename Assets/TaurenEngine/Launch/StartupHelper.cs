/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.0
 *│　Time    ：2023/10/24 20:57:34
 *└────────────────────────┘*/

namespace TaurenEngine.Launch
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
			serviceMgr.Add<ILogService>(new LogService());// 初始化日志服务
		}
	}
}