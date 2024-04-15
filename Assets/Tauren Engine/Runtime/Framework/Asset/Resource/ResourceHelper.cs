/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/7 16:20:04
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public static class ResourceHelper
	{
		/// <summary>
		/// 同步加载资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="container"></param>
		/// <param name="path"></param>
		/// <param name="cache"></param>
		/// <returns></returns>
		public static T Load<T>(IRefrenceContainer container, string path, bool cache = true) where T : UnityEngine.Object
		{
			return IResourceService.Instance.Load<T>(container, path, cache);
		}

		/// <summary>
		/// 异步加载资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="container"></param>
		/// <param name="path"></param>
		/// <param name="onComplete"></param>
		/// <param name="cache"></param>
		/// <param name="priority"></param>
		/// <returns>有缓存的情况下为空</returns>
		public static ILoadHandler LoadAsync<T>(IRefrenceContainer container, string path, Action<bool, T> onComplete,
			bool cache = true, int priority = 10) where T : UnityEngine.Object
		{
			return IResourceService.Instance.LoadAsync<T>(container, path, cache, priority, onComplete);
		}
	}
}