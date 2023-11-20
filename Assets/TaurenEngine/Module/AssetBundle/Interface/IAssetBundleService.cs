/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 21:12:32
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.ModAssetBundle
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