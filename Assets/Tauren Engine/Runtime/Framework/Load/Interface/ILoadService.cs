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
		/// 同步加载数据
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		ILoadData Load(string path);

		/// <summary>
		/// 异步加载数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="path"></param>
		/// <param name="onComplete"></param>
		void LoadAsync<T>(string path, Action<ILoadData> onComplete) where T : UnityEngine.Object;
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
}