/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:31:43
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 资源服务，将各个资源模块整合服务
	/// 
	/// 1.缓存资源；
	/// 2.Http远程下载资源；
	/// 3.本地加载资源；
	/// 4.AB包管理和资源管理；
	/// </summary>
	public partial class ResourceService : IResourceService
	{
		public ResourceService()
		{
			this.InitInterface();
		}

		#region 同步加载
		public T Load<T>(IRefrenceContainer container, string path, LoadType loadType, bool cache)
		{
			// 格式化地址
			path = FormatPath(path);

			// 检测资源缓存池
			if (FindFromCache<T>(container, path, out var cacheAsset))
				return cacheAsset;

			// 加载资源
			return LoadPure<T>(container, path, loadType, cache);
		}
		#endregion

		#region 异步加载
		public ILoadHandler LoadAsync<T>(IRefrenceContainer container, string path, LoadType loadType, bool cache, int priority, Action<bool, T> onComplete) 
		{
			// 格式化地址
			path = FormatPath(path);

			// 检测资源缓存池
			if (FindFromCache<T>(container, path, onComplete))
				return null;

			// 加载资源
			return LoadPure<T>(container, path, loadType, cache, priority, onComplete);
		}
		#endregion

		#region 辅助处理
		/// <summary>
		/// 格式化资源路径
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		private string FormatPath(string path)
		{
			return path;
		}
		#endregion
	}
}