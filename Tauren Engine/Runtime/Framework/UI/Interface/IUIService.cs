/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/24 12:08:13
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public interface IUIService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IUIService Instance { get; internal set; }
	}

	public static class IUIServiceExtension
	{
		public static void InitInterface(this IUIService @object)
		{
			if (IUIService.Instance != null)
				Log.Error("IUIService重复创建实例");

			IUIService.Instance = @object;
		}
	}
}