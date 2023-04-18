/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/18 11:34:57
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.Unity
{
	public interface ICacheService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ICacheService Instance { get; internal set; }
	}

	public static class ICacheServiceExtension
	{
		public static void InitInterface(this ICacheService @object)
		{
			if (ICacheService.Instance != null)
				Log.Error("ICacheService重复创建实例");

			ICacheService.Instance = @object;
		}
	}
}