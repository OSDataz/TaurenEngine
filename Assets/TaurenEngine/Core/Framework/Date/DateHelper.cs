/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/20 20:38:17
 *└────────────────────────┘*/

namespace TaurenEngine
{
	public static class DateHelper
	{
		internal static IDateService dateService;

		/// <summary>
		/// 自启动时间（支持子线程访问）
		/// </summary>
		public static float RealtimeSinceStartup => dateService.RealtimeSinceStartup;
	}
}