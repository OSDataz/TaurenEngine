/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/11 21:59:28
 *└────────────────────────┘*/

using System.Diagnostics;

namespace Tauren.Core.Runtime
{
	public static class Log
	{
		/// <summary>
		/// 打印普通日志
		/// </summary>
		/// <param name="message"></param>
		[Conditional("DEBUG_LOG_PRINT")]
		public static void Print(object message)
		{
			ILogService.Instance.Print(message);
		}

		/// <summary>
		/// 重要信息日志
		/// </summary>
		/// <param name="message"></param>
		[Conditional("DEBUG_LOG_INFO")]
		public static void Info(object message)
		{
			ILogService.Instance.Info(message);
		}

		/// <summary>
		/// 警告日志
		/// </summary>
		/// <param name="message"></param>
		[Conditional("DEBUG_LOG_WARN")]
		public static void Warn(object message)
		{
			ILogService.Instance.Warn(message);
		}

		/// <summary>
		/// 报错日志
		/// </summary>
		/// <param name="message"></param>
		[Conditional("DEBUG_LOG_ERROR")]
		public static void Error(object message)
		{
			ILogService.Instance.Error(message);
		}
	}
}