/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/21 22:17:02
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
	public static class TDebug
	{
		public static void Log(object message)
		{
			LogManager.Instance.Log(message);
		}

		public static void Error(object message)
		{
			LogManager.Instance.Error(message);
		}

		public static void Warning(object message)
		{
			LogManager.Instance.Warning(message);
		}

		public static void LogLabel(string label, string message)
		{
			LogManager.Instance.SetLabel(label, message);
		}

		public static void LogMix(string label, string message)
		{
			LogManager.Instance.SetLabel(label, message);
			LogManager.Instance.Log($"{label}：{message}");
		}

		public static void ClearLabel(string label)
		{
			LogManager.Instance.RemoveLabel(label);
		}

		public static void LogApplication()
		{
			Log($"Version: {Application.version}");
			Log($"UnityVersion: {Application.unityVersion}");
			Log($"Platform: {Application.platform}");
		}
	}
}