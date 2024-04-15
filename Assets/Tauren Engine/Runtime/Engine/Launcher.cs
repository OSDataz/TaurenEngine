/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/7 9:50:41
 *└────────────────────────┘*/

using Tauren.Core.Runtime;
using Tauren.Framework.Runtime;
using UnityEngine;

namespace Tauren.Engine.Runtime
{
	/// <summary>
	/// 引擎启动器
	/// </summary>
	public class Launcher : InstanceBase<Launcher>
	{
		#region 服务
		/// <summary>
		/// 初始化服务
		/// </summary>
		public void InitService(GameObject gameObject)
		{
			new LogService();// 日志服务
			new PoolService();// 对象池服务
			new TimerService();// Timer服务
			var timerComp = gameObject.GetOrAddComponent<TimerComponent>();
			new EventService();// 事件服务
			new JsonService();// Json服务
			new CoroutineService(timerComp);// 协程服务

			new CacheService();// 缓存服务
			new AssetBundleService();// AB包服务
			new LoadService();// 加载服务
			new DownloadService();// 下载服务
			new ResourceService();// 资源服务

			new VersionService();// 版本服务
			new HotfixService();// 热更新服务

			new UIService();// UI管理服务
			new ConfigService();// 配置表服务
			new AudioService();// 音频服务
			new NetworkService();// 网络服务
		}
		#endregion
	}
}