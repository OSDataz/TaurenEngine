/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/21 22:15:22
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TaurenEngine.Core
{
	public sealed class LogManager : Singleton<LogManager>
	{
		public LogManager() 
		{
			Active = true;
		}

		internal void Log(object message)
		{
			if (!_active) return;
			Debug.Log(message);
		}

		internal void Error(object message)
		{
			if (!_active) return;
			Debug.LogError(message);
		}

		internal void Warning(object message)
		{
			if (!_active) return;
			Debug.LogWarning(message);
		}

		#region 激活日志
		private bool _active;

		/// <summary>
		/// 激活日志
		/// </summary>
		public bool Active
		{
			get => _active;
			set
			{
				if (_active == value)
					return;

				_active = value;

				if (_active)
					Application.logMessageReceived += LogMessageHandler;
				else
					Application.logMessageReceived -= LogMessageHandler;
			}
		}

		private void LogMessageHandler(string logString, string stackTrace, LogType type)
		{
			if (type == LogType.Error && !string.IsNullOrEmpty(stackTrace))
				logString += "\n" + stackTrace;

			SetRecord(ref logString, type);

			OnLogChange?.Invoke();
		}
		#endregion

		#region 日志存储
		public readonly List<LogItem> logs = new List<LogItem>();
		public LogItem LastLog { get; private set; }

		private void SetRecord(ref string message, LogType type)
		{
			if (LastLog?.message == message)
			{
				LastLog.count += 1;
			}
			else
			{
				LastLog = new LogItem()
				{
					index = logs.Count + 1,
					time = DateTime.Now.ToString("MM-dd HH:mm:ss"),
					type = type,
					message = message,
					count = 1
				};

				logs.Add(LastLog);
			}
		}

		public void Clear()
		{
			logs.Clear();
		}
		#endregion

		#region 日志扩展-改变更新
		public event Action OnLogChange;
		#endregion

		#region 标签日志
		private readonly Dictionary<string, string> _labelLogs = new Dictionary<string, string>();

		public void SetLabel(string label, string message)
		{
			_labelLogs.Set(label, message);
		}

		public void RemoveLabel(string label)
		{
			_labelLogs.Remove(label);
		}

		public string LabelToString()
		{
			StringBuilder text = new StringBuilder();

			foreach (var kv in _labelLogs)
			{
				text.AppendLine($"{kv.Key}：{kv.Value}");
			}

			return text.ToString();
		}

		public int LabelCount => _labelLogs.Count;
		#endregion
	}

	public class LogItem
	{
		public int index;
		public string time;
		public LogType type;
		public string message;
		public int count;
	}
}