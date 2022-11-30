/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/24 10:56:44
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine
{
	/// <summary>
	/// 日志管理
	/// </summary>
	public class LogService : ILogService
	{
		public LogService()
		{
			this.InitInterface(this);
		}

		private bool _enabled;
		/// <summary>
		/// 是否开启日志管理器
		/// </summary>
		public bool Enabled
		{
			get => _enabled;
			set
			{
				if (_enabled == value)
					return;

				_enabled = value;

				if (_enabled)
					Application.logMessageReceived += OnLogMessageReceived;
				else
					Application.logMessageReceived -= OnLogMessageReceived;
			}
		}

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
	}
}