/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/22 15:30:04
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine
{
	public interface INetworkService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static INetworkService Instance { get; internal set; }
	}

	public static class INetworkServiceExtension
	{
		public static void InitInterface(this INetworkService @object)
		{
			if (INetworkService.Instance != null)
				Debug.LogError("INetworkService重复创建实例");

			INetworkService.Instance = @object;
		}
	}
}