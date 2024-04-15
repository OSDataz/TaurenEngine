/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/9 8:09:49
 *└────────────────────────┘*/

using System;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 异步加载
	/// </summary>
	public partial class LoadService
	{
		public ILoadHandler LoadAsync(string path, LoadType loadType, int priority, Action<ILoadData> onComplete)
		{
			var loadItem = new LoadItemAsync();
			loadItem.path = path;
			loadItem.loadType = loadType;
			loadItem.Priority = priority;
			loadItem.onComplete = onComplete;

			_execList.Execute(loadItem);

			return loadItem;
		}

		public void UnloadAsync(ILoadHandler loadHandler)
		{
			if (loadHandler is IExecuteItem execItem)
				_execList.Remove(execItem);
		}

		#region 执行队列管理
		private readonly ExecuteList _execList;

		private void OnExecute(IExecuteItem item)
		{
			var loadItem = item as LoadItemAsync;

			if (loadItem.loadType == LoadType.Asset)
			{
				LoadAsyncByAsset(loadItem);
			}
			else if (loadItem.loadType == LoadType.Resources)
			{
				LoadAsyncByResources(loadItem);
			}
			else if (loadItem.loadType == LoadType.RawFile)
			{
				LoadAsyncByFile(loadItem);
			}
		}

		private void OnLoadAsyncComplete(LoadItemAsync loadItem)
		{
			if (loadItem.Code != LoadCode.Success)
				ToErrorLog(loadItem);

			loadItem.onComplete?.Invoke(loadItem);

			_execList.Complete(loadItem);
		}
		#endregion
	}
}