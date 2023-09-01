/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/29 20:33:05
 *└────────────────────────┘*/

namespace TaurenEngine.Runtime.Framework
{
	public interface IAssetBundleService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IAssetBundleService Instance { get; internal set; }
	}

	public static class IAssetBundleServiceExtension
	{
		public static void InitInterface(this IAssetBundleService @object)
		{
			if (IAssetBundleService.Instance != null)
				Log.Error("IAssetBundleService重复创建实例");

			IAssetBundleService.Instance = @object;
		}
	}
}