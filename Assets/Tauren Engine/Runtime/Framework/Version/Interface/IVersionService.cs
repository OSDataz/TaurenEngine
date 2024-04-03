/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:21:48
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
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