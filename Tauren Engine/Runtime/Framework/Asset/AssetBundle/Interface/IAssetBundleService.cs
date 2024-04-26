/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 21:12:32
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public interface IAssetBundleService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IAssetBundleService Instance { get; internal set; }

		/// <summary>
		/// 是否启用AB包模式
		/// </summary>
		bool Enabled { get; set; }

		/// <summary>
		/// 指定资源是否在AB包中
		/// </summary>
		/// <param name="assetPath"></param>
		/// <returns></returns>
		bool InAssetBundle(string assetPath);
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