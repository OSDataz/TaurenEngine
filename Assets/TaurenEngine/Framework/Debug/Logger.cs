/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/25 0:41:29
 *└────────────────────────┘*/

using System.Diagnostics;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 业务代码可选择使用该接口输出日志，用于控制性能
	/// </summary>
	public class Logger
	{
		[Conditional("DEBUG")]
		public static void Log(object message)
		{
			UnityEngine.Debug.Log(message);
		}

		[Conditional("DEBUG")]
		public static void Warning(object message)
		{
			UnityEngine.Debug.LogWarning(message);
		}

		public static void Error(object message)
		{
			UnityEngine.Debug.LogError(message);
		}
	}
}