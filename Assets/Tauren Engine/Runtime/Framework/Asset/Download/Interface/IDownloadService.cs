/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 21:28:46
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public interface IDownloadService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IDownloadService Instance { get; internal set; }
	}

	public static class IDownloadServiceExtension
	{
		public static void InitInterface(this IDownloadService @object)
		{
			if (IDownloadService.Instance != null)
				Log.Error("IDownloadService重复创建实例");

			IDownloadService.Instance = @object;
		}
	}
}