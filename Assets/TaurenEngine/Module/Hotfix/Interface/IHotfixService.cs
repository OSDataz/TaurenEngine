/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 21:14:22
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.ModHotfix
{
	public interface IHotfixService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IHotfixService Instance { get; internal set; }
	}

	public static class IHotfixServiceExtension
	{
		public static void InitInterface(this IHotfixService @object)
		{
			if (IHotfixService.Instance != null)
				Log.Error("IHotfixService重复创建实例");

			IHotfixService.Instance = @object;
		}
	}
}