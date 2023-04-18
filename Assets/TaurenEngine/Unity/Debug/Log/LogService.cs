/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/15 15:05:16
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Unity
{
	/// <summary>
	/// 日志服务
	/// </summary>
	public sealed class LogService : ILogService
	{
		public LogService()
		{
			this.InitInterface();

#if DEBUG_LOG_INFO || DEBUG_LOG_WARN || DEBUG_LOG_ERROR
			Application.logMessageReceived += OnLogMessageReceived;
#endif
		}

		#region 日志调用接口
		public void Info(object message)
		{
			Debug.Log(message);
		}

		public void Warn(object message)
		{
			Debug.LogWarning(message);
		}

		public void Error(object message)
		{
			Debug.LogError(message);
		}
		#endregion

		#region 日志回调接口
		/// <summary>
		/// 日志更新回调
		/// </summary>
		public Action onLogChange;

		private void OnLogMessageReceived(string logString, string stackTrace, LogType type)
		{
			if (type == LogType.Error && !string.IsNullOrEmpty(stackTrace))
				logString += "\n" + stackTrace;

			//SetRecord(ref logString, type);

			onLogChange?.Invoke();
		}
		#endregion
	}
}