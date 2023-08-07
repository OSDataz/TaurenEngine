/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/21 20:11:11
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Runtime.Framework
{
	/// <summary>
	/// 资源服务
	/// 
	/// 1.缓存资源；
	/// 2.Http远程下载资源；
	/// 3.本地下载资源；
	/// 4.AB包管理和资源管理；
	/// </summary>
	internal class ResourceService : IResourceService
	{
		public ResourceService()
		{
			this.InitInterface();
		}

		public T Load<T>(IRefrenceObject target, string path, bool cache = true)
		{


			return default(T);
		}

		public void Load<T>(IRefrenceObject target, string path, Action<bool, T> onLoadComplete, bool cache = true, int loadPriority = 10, 
			Action onLoadStart = null, Action<int, int> onLoadProgress = null)
		{
			
		}
	}
}