/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/18 20:28:30
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public static class EngineHelper
	{
		/// <summary>
		/// 初始化服务
		/// </summary>
		public static void InitService()
		{
			var serviceMgr = ServiceManager.Instance;

			serviceMgr.Add<IPoolService>(new PoolService());// 初始化对象池服务
		}
	}
}