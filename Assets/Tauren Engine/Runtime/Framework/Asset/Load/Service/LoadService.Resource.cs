/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/10 11:36:28
 *└────────────────────────┘*/

using System.Collections;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// Resources文件加载
	/// </summary>
	public partial class LoadService
	{
		#region 同步加载
		private void LoadByResources(LoadItem loadItem)
		{
			var asset = Resources.Load(loadItem.path);
			if (asset != null)
			{
				loadItem.Asset = CreateAsset(loadItem.path, asset);
				loadItem.Code = LoadCode.Success;
			}
			else
			{
				loadItem.Code = LoadCode.Fail;
			}
		}
		#endregion

		#region 异步加载
		private void LoadAsyncByResources(LoadItemAsync loadItem)
		{
			CoroutineHelper.StartCoroutine(LoadAsyncByResourcesCoroutine(loadItem));
		}

		private IEnumerator LoadAsyncByResourcesCoroutine(LoadItemAsync loadItem)
		{
			var request = Resources.LoadAsync(loadItem.path);

			yield return request;

			if (request.isDone && request.asset != null)
			{
				loadItem.Asset = CreateAsset(loadItem.path, request.asset);
				loadItem.Code = LoadCode.Success;
			}
			else
			{
				loadItem.Code = LoadCode.Fail;
			}

			OnLoadAsyncComplete(loadItem);
		}
		#endregion
	}
}