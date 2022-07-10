/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/22 19:38:11
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	internal partial class LoadTaskManager
	{
		#region 异步加载列队管理（PS：同步加载就是单线程顺序加载执行）
		private readonly DuLinkList<LoadTask> _linkList = new DuLinkList<LoadTask>();
		/// <summary> 当前加载资源数 </summary>
		private uint _loadCount;
		/// <summary> 最大同时加载资源数 </summary>
		public uint loadCountMax = 6;

		private LoadTask FindLoadTask(uint id)
			=> _linkList.Find(node => node.data.id == id)?.data;

		private void AddLoadTask(LoadTask loadTask)
		{
			// 检测是否是第一个加载单元
			if (_linkList.First == null)
			{
				_linkList.SetRootNode(loadTask.LinkNode);

				LoadAsync(loadTask);
				return;
			}

			// 检测是否有与当前同样的正在加载单元
			var node = _linkList.First;
			do
			{
				if (node.data.loadState == LoadState.Loading)
				{
					if (node.data.path.Equals(loadTask))
					{
						loadTask.loadState = LoadState.WaitLoad;

						// 无视优先级排序
						_linkList.Last.AddNextNode(loadTask.LinkNode);
						return;
					}
				}
				else if (node.data.loadState != LoadState.WaitLoad)
					break;

				node = node.Next;
			}
			while (node != null);

			// 检测是否要加载当前单元（无视优先级排序）
			if (_loadCount < loadCountMax)
			{
				_linkList.Last.AddNextNode(loadTask.LinkNode);

				LoadAsync(loadTask);
				return;
			}

			// 优先级添加
			node = _linkList.Last;
			do
			{
				if (node.data.loadState == LoadState.Wait)
				{
					if (loadTask.setting.loadPriority <= node.data.setting.loadPriority)
					{
						node.AddNextNode(loadTask.LinkNode);
						return;
					}
				}

				node = node.Prior;
			}
			while (node != null);

			_linkList.Last.AddNextNode(loadTask.LinkNode);
		}

		private LoadTask GetNextLoadTask()
		{
			return _linkList.Find(node => node.data.loadState == LoadState.Wait)?.data;
		}
		#endregion

		#region 异步加载
		private void LoadAsync(LoadTask loadTask)
		{
			
		}
		#endregion
	}
}