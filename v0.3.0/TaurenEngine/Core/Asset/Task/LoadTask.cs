/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/20 11:10:55
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 加载任务
	/// </summary>
	/// <typeparam name="T">资源类型</typeparam>
	internal class LoadTask<T> : LoadTask
	{
		/// <summary>
		/// 资源数据
		/// </summary>
		public LoadRes<T> resData;
		public override LoadRes ResData => resData;

		/// <summary>
		/// 加载完成回调<是否加载成功，加载资源>
		/// </summary>
		public Action<bool, T> onLoadComplete;

		public override void Clear()
		{

		}

		public override void Destroy()
		{

		}
	}

	/// <summary>
	/// 加载任务
	/// </summary>
	internal abstract class LoadTask
	{
		/// <summary>
		/// 资源加载ID
		/// </summary>
		public int id;
		/// <summary>
		/// 地址数据
		/// </summary>
		public LoadPath path;
		/// <summary>
		/// 加载资源
		/// </summary>
		public abstract LoadRes ResData { get; }
		/// <summary>
		/// 加载设置
		/// </summary>
		public LoadSetting setting;
		/// <summary>
		/// 加载状态
		/// </summary>
		public LoadState loadState;
		/// <summary>
		/// 是否已取消加载
		/// </summary>
		public bool isCancel;

		/// <summary>
		/// 是否永久缓存
		/// </summary>
		public bool IsPermanent => setting.cacheType == CacheType.PermanentCache;

		/// <summary>
		/// 链表节点
		/// </summary>
		public DuLinkNode<LoadTask> LinkNode { get; private set; }

		public LoadTask()
		{
			LinkNode = new DuLinkNode<LoadTask>(this);
		}

		public virtual void Clear()
		{

		}

		public virtual void Destroy()
		{

		}
	}
}