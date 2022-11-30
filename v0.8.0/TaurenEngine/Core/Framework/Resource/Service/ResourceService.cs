/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/21 20:11:11
 *└────────────────────────┘*/

using System;

namespace TaurenEngine
{
	/// <summary>
	/// 资源服务
	/// </summary>
	internal class ResourceService : IResourceService
	{
		/// <summary>
		/// 资源缓存
		/// </summary>
		private ResourceCache _resourceCache = new ResourceCache();

		public T Load<T>(IRefObject target, string path, bool cache = true)
		{


			return default(T);
		}

		public void Load<T>(IRefObject target, string path, Action<bool, T> onLoadComplete, bool cache = true, int loadPriority = 10, 
			Action onLoadStart = null, Action<int, int> onLoadProgress = null)
		{
			
		}
	}
}