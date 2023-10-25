/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/29 20:34:28
 *└────────────────────────┘*/

namespace TaurenEngine.Launch
{
	public interface IVersionService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IVersionService Instance { get; internal set; }
	}

	public static class IVersionServiceExtension
	{
		public static void InitInterface(this IVersionService @object)
		{
			if (IVersionService.Instance != null)
				Log.Error("IVersionService重复创建实例");

			IVersionService.Instance = @object;
		}
	}
}