/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 12:04:11
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Core;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 异步加载管理器
	/// </summary>
	internal class AsyncLoadManager
	{
		public int multLoadMaximum;

		public AsyncLoadManager()
		{
			_loaderList = new List<AsyncLoaderBase>();
			_taskList = new DuLinkList<AsyncLoadTask>();
		}

		public void Load(AsyncLoadTask loadTask)
		{
			if (IsLoadingMax)
			{
				AddTask(loadTask);
			}
			else
			{
				GetLoader(loadTask.loadType).Load(loadTask);
			}
		}

		public bool Unload(uint id)
		{
			

			return true;
		}

		#region 加载器
		private readonly List<AsyncLoaderBase> _loaderList;

		private AsyncLoaderBase GetLoader(LoadType loadType)
		{
			AsyncLoaderBase loader;
			if (loadType == LoadType.AssetBundle) loader = AssetBundleAsync.Get();
			else if (loadType == LoadType.Resources) loader = ResourcesAsync.Get();
			else if (loadType == LoadType.File) loader = FileAsync.Get();
			else return null;

			_loaderList.Add(loader);
			return loader;
		}

		/// <summary> 是否加载数量上限 </summary>
		private bool IsLoadingMax => _loaderList.Count >= multLoadMaximum;
		#endregion

		#region 任务列表
		private readonly DuLinkList<AsyncLoadTask> _taskList;

		private void AddTask(AsyncLoadTask loadTask)
		{
			var task = _taskList.Last;
			while (task != null)
			{
				if (task.loadPriority >= loadTask.loadPriority)
				{
					_taskList.AddToNext(loadTask, task);
					return;
				}
			}

			_taskList.AddToFirst(loadTask);
		}

		private bool TryGetNextTask(out AsyncLoadTask task)
		{
			task = _taskList.First;
			while (task != null)
			{
				if (task.loadStatus == LoadStatus.Wait)
					return true;
			}

			return false;
		}
		#endregion
	}
}