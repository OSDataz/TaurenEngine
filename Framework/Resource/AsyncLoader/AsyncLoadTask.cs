/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 12:04:58
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;

namespace TaurenEngine.Framework
{
	internal class AsyncLoadTask<T> : AsyncLoadTask
	{
		#region 对象池
		private static ObjectPool<AsyncLoadTask<T>> pool = new ObjectPool<AsyncLoadTask<T>>();

		public static AsyncLoadTask<T> Get() => pool.Get();

		/// <summary> 回收 </summary>
		public void Recycle() => pool.Add(this);
		#endregion

		/// <summary>
		/// 加载完成回调
		/// </summary>
		public Action<bool, T> onLoadComplete;

		public override void Clear()
		{
			onLoadComplete = null;

			base.Clear();
		}

		public override void Destroy()
		{
			onLoadComplete = null;

			base.Destroy();
		}
	}

	/// <summary>
	/// 异步加载任务
	/// </summary>
	internal class AsyncLoadTask : DuLinkNode<AsyncLoadTask>, IRecycle
	{
		/// <summary>
		/// 资源引用ID
		/// </summary>
		public uint id;
		/// <summary>
		/// 资源名/路径
		/// </summary>
		public string path;
		/// <summary>
		/// 加载类型
		/// </summary>
		public LoadType loadType;
		/// <summary>
		/// 加载优先级，越小优先级越高
		/// </summary>
		public int loadPriority;
		/// <summary>
		/// 缓存类型
		/// </summary>
		public CacheType cacheType;
		/// <summary>
		/// 是否远程下载
		/// </summary>
		public DownloadType downloadType;
		/// <summary>
		/// 是否取消加载，卸载资源
		/// </summary>
		public bool isUnload;

		/// <summary>
		/// 加载进度回调
		/// </summary>
		public Action<float> onLoadProgress;

		/// <summary>
		/// 是否是AB包资源
		/// </summary>
		public bool isABPack;
		/// <summary>
		/// 资源配置
		/// </summary>
		public AssetConfig assetConfig;
		/// <summary>
		/// AB包配置
		/// </summary>
		public ABConfig abPackConfig;

		/// <summary>
		/// 加载资源
		/// </summary>
		public UnityEngine.Object data;
		/// <summary>
		/// 加载状态
		/// </summary>
		public LoadStatus loadStatus = LoadStatus.Wait;

		public virtual void Clear()
		{
			downloadType = DownloadType.None;
			isUnload = false;

			onLoadProgress = null;

			isABPack = false;
			assetConfig = null;
			abPackConfig = null;

			data = null;
			loadStatus = LoadStatus.Wait;
		}

		public virtual void Destroy()
		{
			onLoadProgress = null;

			assetConfig = null;
			abPackConfig = null;

			data = null;
		}
	}
}