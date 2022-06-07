/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/30 22:12:39
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;

namespace TaurenEngine.Framework
{
	internal class LoadTaskAsync<T> : LoadTaskAsync
	{
		#region 对象池
		private static ObjectPool<LoadTaskAsync<T>> pool = new ObjectPool<LoadTaskAsync<T>>();

		public static LoadTaskAsync<T> Get() => pool.Get();

		/// <summary>
		/// 回收
		/// </summary>
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

	internal abstract class LoadTaskAsync : IRecycle
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
			loadStatus = LoadStatus.Wait;
			isUnload = false;

			isABPack = false;
			data = null;
			onLoadProgress = null;
		}

		public virtual void Destroy()
		{
			assetConfig = null;
			abPackConfig = null;
			data = null;
			onLoadProgress = null;
		}
	}
}