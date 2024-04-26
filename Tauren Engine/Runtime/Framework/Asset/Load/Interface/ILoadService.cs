/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:07:51
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public interface ILoadService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ILoadService Instance { get; internal set; }

		/// <summary>
		/// 同步加载资源
		/// </summary>
		/// <param name="path"></param>
		/// <param name="loadType"></param>
		/// <returns></returns>
		ILoadData Load(string path, LoadType loadType);

		/// <summary>
		/// 异步加载资源
		/// </summary>
		/// <param name="path"></param>
		/// <param name="loadType"></param>
		/// <param name="priority"></param>
		/// <param name="onComplete"></param>
		/// <returns></returns>
		ILoadHandler LoadAsync(string path, LoadType loadType, int priority, Action<ILoadData> onComplete);

		/// <summary>
		/// 停止异步加载资源
		/// </summary>
		/// <param name="loadHandler"></param>
		void UnloadAsync(ILoadHandler loadHandler);
	}

	public static class ILoadServiceExtension
	{
		public static void InitInterface(this ILoadService @object)
		{
			if (ILoadService.Instance != null)
				Log.Error("ILoadService重复创建实例");

			ILoadService.Instance = @object;
		}
	}

	/// <summary>
	/// 加载类型
	/// </summary>
	public enum LoadType
	{
		/// <summary>
		/// 直接加载单个资源（从AB包中加载或编辑器模式加载）
		/// </summary>
		Asset,
		/// <summary>
		/// 加载AB包（注意：不是从AB包中加载）
		/// </summary>
		AssetBundle,
		/// <summary>
		/// 加载Resources下资源
		/// </summary>
		Resources,
		/// <summary>
		/// 使用File加载原生资源
		/// </summary>
		RawFile
	}
}