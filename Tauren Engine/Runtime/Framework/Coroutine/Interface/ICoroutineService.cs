/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/10 14:35:33
 *└────────────────────────┘*/

using System.Collections;
using Tauren.Core.Runtime;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	public interface ICoroutineService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ICoroutineService Instance { get; internal set; }

		/// <summary>
		/// 开始协程
		/// </summary>
		/// <param name="routine"></param>
		/// <returns></returns>
		Coroutine StartCoroutine(IEnumerator routine);

		/// <summary>
		/// 停止协程
		/// </summary>
		/// <param name="routine"></param>
		void StopCoroutine(IEnumerator routine);

		/// <summary>
		/// 停止协程
		/// </summary>
		/// <param name="routine"></param>
		void StopCoroutine(Coroutine routine);
	}

	public static class ICoroutineServiceExtension
	{
		public static void InitInterface(this ICoroutineService @object)
		{
			ICoroutineService.Instance = @object;
		}
	}
}