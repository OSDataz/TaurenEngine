/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/8 14:15:58
 *└────────────────────────┘*/

using System.Diagnostics;

namespace TaurenEngine.Core
{
	public static class Debugx
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