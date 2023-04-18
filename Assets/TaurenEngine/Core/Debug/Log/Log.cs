/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/11 21:59:28
 *└────────────────────────┘*/

using System.Diagnostics;

namespace TaurenEngine.Core
{
	public static class Log
	{
		[Conditional("DEBUG_LOG_INFO")]
		public static void Info(object message)
		{
			ILogService.Instance.Info(message);
		}

		[Conditional("DEBUG_LOG_WARN")]
		public static void Warn(object message)
		{
			ILogService.Instance.Warn(message);
		}

		[Conditional("DEBUG_LOG_ERROR")]
		public static void Error(object message)
		{
			ILogService.Instance.Error(message);
		}
	}
}