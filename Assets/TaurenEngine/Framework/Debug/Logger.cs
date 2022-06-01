/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/25 0:41:29
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 业务代码可选择使用该接口输出日志，用于控制性能
	/// </summary>
	public class Logger
	{
		public static void Log(object message)
		{
			Debug.Log(message);
		}

		public static void Warning(object message)
		{
			Debug.LogWarning(message);
		}

		public static void Error(object message)
		{
			Debug.LogError(message);
		}
	}
}