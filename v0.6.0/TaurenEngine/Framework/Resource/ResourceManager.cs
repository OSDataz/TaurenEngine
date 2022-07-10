/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/5/6 23:39:39
 *└────────────────────────┘*/

/* 开发笔记：
 * 
 * 2022-06-14
 * 1.资源模块需要尽量模块化设计；
 * 2.资源管理器默认是AB包加载，其他加载方式单独模块处理；
 */

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 资源管理器
	/// </summary>
	public partial class ResourceManager
	{
		internal readonly CacheManager cacheMgr;
		internal readonly AsyncLoadManager asyncLoadMgr;

		public ResourceManager()
		{
			cacheMgr = new CacheManager();
			asyncLoadMgr = new AsyncLoadManager();
		}

		#region 资源ID
		internal uint toId = 0;
		internal uint ToId() => ++toId;
		#endregion

		#region Resources加载
		private ResourcesManager _resources;
		public ResourcesManager Resources => _resources ??= new ResourcesManager();
		#endregion

		#region File加载
		private FileManager _file;
		public FileManager File => _file ??= new FileManager();
		#endregion

		#region 卸载资源
		/// <summary>
		/// 卸载资源
		/// </summary>
		/// <param name="id"></param>
		public void Unload(uint id)
		{
			if (asyncLoadMgr.Unload(id))
				return;

			cacheMgr.Release(id);
		}
		#endregion

		/// <summary>
		/// 卸载资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="loadType"></param>
		/// <param name="asset"></param>
		internal void Unload<T>(LoadType loadType, T asset) where T : UnityEngine.Object
		{
			if (loadType == LoadType.AssetBundle) Unload(asset);
			else if (loadType == LoadType.Resources) Resources.Unload(asset);
			else if (loadType == LoadType.File) File.Unload(asset);
		}
	}
}