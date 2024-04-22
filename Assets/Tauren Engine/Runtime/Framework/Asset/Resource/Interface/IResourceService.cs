/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:30:53
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public interface IResourceService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IResourceService Instance { get; internal set; }

		/// <summary>
		/// 同步加载资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="container"></param>
		/// <param name="path"></param>
		/// <param name="loadType"></param>
		/// <param name="cache"></param>
		/// <returns></returns>
		T Load<T>(IRefrenceContainer container, string path, LoadType loadType, bool cache);

		/// <summary>
		/// 异步加载资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="container"></param>
		/// <param name="path"></param>
		/// <param name="loadType"></param>
		/// <param name="cache"></param>
		/// <param name="priority"></param>
		/// <param name="onComplete"></param>
		/// <returns></returns>
		ILoadHandler LoadAsync<T>(IRefrenceContainer container, string path, LoadType loadType, bool cache, int priority, Action<bool, T> onComplete);
	}

	public static class IResourceServiceExtension
	{
		public static void InitInterface(this IResourceService @object)
		{
			if (IResourceService.Instance != null)
				Log.Error("IResourceService重复创建实例");

			IResourceService.Instance = @object;
		}
	}
}