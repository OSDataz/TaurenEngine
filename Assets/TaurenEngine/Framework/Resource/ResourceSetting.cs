/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/3 17:44:50
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 加载类型
	/// </summary>
	public enum LoadType
	{
		/// <summary>
		/// AssetBundle路径格式：Assets起的相对路径，ab包中的索引名。AB包路径：Assets起的相对路径，无后缀
		/// </summary>
		AssetBundle,
		/// <summary>
		/// File路径格式：Assets起的相对路径，有后缀，不支持配置
		/// </summary>
		File,
		/// <summary>
		/// 项目自带资源加载，不支持远程下载，不支持配置
		/// <para>Resources路径格式：Resources文件夹后的相对路径，无后缀</para>
		/// </summary>
		Resources,
	}

	/// <summary>
	/// 缓存类型
	/// </summary>
	public enum CacheType
	{
		/// <summary>
		/// 不缓存
		/// </summary>
		None,
		/// <summary>
		/// 引用计数缓存，立即清理
		/// </summary>
		Reference,
		/// <summary>
		/// 引用计数缓存，延迟定时清理
		/// </summary>
		ReferenceDelay,
		/// <summary>
		/// 持久化（永久）缓存（只要有一个请求永久缓存，该资源就会永久缓存），可以手动清理
		/// </summary>
		Persistent,
	}

	/// <summary>
	/// 下载类型
	/// </summary>
	public enum DownloadType
	{
		/// <summary>
		/// 不远程下载
		/// </summary>
		None,
		/// <summary>
		/// 远程下载，但不本地保存
		/// </summary>
		Download,
		/// <summary>
		/// 远程下载，并保存在本地
		/// </summary>
		DownloadSaveToLocal,
	}

	/// <summary>
	/// 加载状态
	/// </summary>
	internal enum LoadStatus
	{
		/// <summary>
		/// 等待中
		/// </summary>
		Wait,
		/// <summary>
		/// 等待其他同样资源加载
		/// </summary>
		WaitLoad,
		/// <summary>
		/// 加载中
		/// </summary>
		Loading,
		/// <summary>
		/// 加载成功
		/// </summary>
		Success,
		/// <summary>
		/// 加载失败
		/// </summary>
		Fail
	}
}