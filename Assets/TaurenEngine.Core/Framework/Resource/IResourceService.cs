/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/21 20:12:02
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine
{
	public interface IResourceService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IResourceService Instance { get; internal set; }
	}

	public static class IResourceServiceExtension
	{
		public static void InitInterface(this IResourceService @object)
		{
			if (IResourceService.Instance != null)
				Debug.LogError("IResourceService重复创建实例");

			IResourceService.Instance = @object;
		}
	}
}