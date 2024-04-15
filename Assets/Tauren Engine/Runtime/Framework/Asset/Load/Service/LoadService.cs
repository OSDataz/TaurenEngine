/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:07:21
 *└────────────────────────┘*/

using Tauren.Core.Runtime;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 加载服务
	/// </summary>
	public partial class LoadService : ILoadService
	{
		public LoadService()
		{
			this.InitInterface();

			_execList = new ExecuteList(OnExecute, 5);
		}

		private Asset CreateAsset(string key, UnityEngine.Object data)
		{
			var asset = new Asset();
			asset.key = key;
			asset.Data = data;

			return asset;
		}

		private void ToErrorLog(LoadItem loadItem)
		{
			if (loadItem is LoadItemAsync loadItemAsync)
			{
				Log.Error($"资源加载失败，Code:{loadItemAsync.Code} Path:{loadItemAsync.path} LoadType:{loadItemAsync.loadType} Priority:{loadItemAsync.Priority}");
			}
			else
			{
				Log.Error($"资源加载失败，Code:{loadItem.Code} Path:{loadItem.path} LoadType:{loadItem.loadType}");
			}
		}
	}
}