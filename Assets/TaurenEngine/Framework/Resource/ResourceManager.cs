/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/6 23:39:39
 *└────────────────────────┘*/

/* 资源加载流程：
 * 1.查找缓存；
 * 2.本地查询资源；
 * 3.查找对应AB包；
 * 4.HTTP下载对应AB包（保存到本地？）；
 * 5.解析对应AB包？；
 * 6.获取资源；
 */

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 资源管理器
	/// </summary>
	public class ResourceManager
	{
		internal readonly CacheManager CacheMgr;
		internal readonly LoadManager LoadMgr;

		public ResourceManager()
		{
			CacheMgr = new CacheManager();
			LoadMgr = new LoadManager();
		}

		public T Load<T>()
		{
			return default(T);
		}

		public int LoadAsync<T>()
		{
			return 0;
		}
	}
}