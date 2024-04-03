/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:31:43
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 资源服务
	/// 
	/// 1.缓存资源；
	/// 2.Http远程下载资源；
	/// 3.本地加载资源；
	/// 4.AB包管理和资源管理；
	/// </summary>
	public class ResourceService : IResourceService
	{
		public ResourceService()
		{
			this.InitInterface();
		}

		#region 同步加载
		public T Load<T>(IRefrenceObject target, string path, bool cache = true) where T : UnityEngine.Object
		{
			path = FormatPath(path);

			// 检测资源缓存池缓存资源
			if (ICacheService.Instance.TryGet(path, out var asset))
				return asset.TryGetAsset<T>(out var data) ? data : null;

			//var loadData = LoadData.GetFromPool();
			//loadData.path = path;
			//loadData.cache = cache;

			return null;
		}
		#endregion

		#region 异步加载
		//public void Load<T>(IRefrenceObject target, string path, Action<bool, T> onLoadComplete, 
		//	bool cache = true, int priority = 10, 
		//	Action onDownloadStart = null, Action<int, int> onDownloadProgress = null) where T : UnityEngine.Object
		//{
		//	path = FormatPath(path);

		//	// 检测资源缓存池缓存资源
		//	if (IAssetCacheService.Instance.TryGet(path, out var asset))
		//	{
		//		onLoadComplete?.Invoke(asset.TryGet<T>(out var data), data);
		//		return;
		//	}

		//	var loadData = LoadData.GetFromPool();
		//	loadData.path = path;
		//	loadData.cache = cache;
		//	loadData.asyncData.priority = priority;
		//	loadData.asyncData.onComplete = () => LoadAsyncComplete(loadData, onLoadComplete);
		//	loadData.downloadData.onStart = onDownloadStart;
		//	loadData.downloadData.onProgress = onDownloadProgress;
		//	loadData.abData.isAssetBundle = PathUtils.IsExtension(path, ".unity3d");// 是否是AB包资源


		//}

		//private void LoadAsyncComplete<T>(LoadData loadData, Action<bool, T> onLoadComplete) where T : UnityEngine.Object
		//{

		//}
		#endregion

		#region 公共模块
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