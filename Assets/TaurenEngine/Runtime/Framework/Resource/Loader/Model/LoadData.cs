/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/9/4 20:19:43
 *└────────────────────────┘*/

namespace TaurenEngine.Runtime.Framework
{
	/// <summary>
	/// 资源加载数据
	/// </summary>
	public class LoadData : PoolObject<LoadData>
	{
		/// <summary> 资源路径 </summary>
		public string path;

		/// <summary> 加载完成后是否缓存资源 </summary>
		public bool cache;

		/// <summary> 加载资源 </summary>
		public Asset asset;

		/// <summary> 异步加载数据 </summary>
		public readonly LoadAsyncData asyncData = new LoadAsyncData();

		/// <summary> 远程下载数据 </summary>
		public readonly DownloadData downloadData = new DownloadData();

		/// <summary> 加载AB包数据 </summary>
		public readonly LoadAssetBundleData abData = new LoadAssetBundleData();

		public override void Clear()
		{
			path = string.Empty;
			cache = false;
			asset = null;

			asyncData.Clear();
			downloadData.Clear();
			abData.Clear();
		}
	}
}