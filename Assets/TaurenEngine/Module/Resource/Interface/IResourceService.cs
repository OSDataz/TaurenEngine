/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:30:53
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.ModResource
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
				Log.Error("IResourceService重复创建实例");

			IResourceService.Instance = @object;
		}
	}
}