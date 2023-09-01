/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/29 20:33:52
 *└────────────────────────┘*/

namespace TaurenEngine.Runtime.Framework
{
	public interface ILoaderService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ILoaderService Instance { get; internal set; }
	}

	public static class ILoaderServiceExtension
	{
		public static void InitInterface(this ILoaderService @object)
		{
			if (ILoaderService.Instance != null)
				Log.Error("ILoaderService重复创建实例");

			ILoaderService.Instance = @object;
		}
	}
}