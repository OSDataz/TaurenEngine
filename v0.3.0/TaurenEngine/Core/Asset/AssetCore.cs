/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/21 14:33:10
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 资源加载管理核心类
	/// </summary>
	public sealed class AssetCore
	{
		/// <summary>
		/// 加载器管理器
		/// </summary>
		internal LoaderManager Loader { get; private set; }

		/// <summary>
		/// 加载资源管理器
		/// </summary>
		internal LoadResManager LoadRes { get; private set; }

		/// <summary>
		/// 加载任务管理器
		/// </summary>
		internal LoadTaskManager LoadTask { get; private set; }

		/// <summary>
		/// 缓存池
		/// </summary>
		internal CachePool Cache { get; private set; }

		public AssetCore()
		{
			Loader = new LoaderManager();

			LoadRes = new LoadResManager(Loader);
			LoadRes.InitRegisterResource();

			LoadTask = new LoadTaskManager();

			Cache = new CachePool();
		}
	}
}