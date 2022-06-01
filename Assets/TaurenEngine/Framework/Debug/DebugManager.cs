/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/17 10:03:02
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.Text;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 日志管理器
	/// </summary>
	public class DebugManager
	{
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
					Application.logMessageReceived += LogMessageHandler;
				else
					Application.logMessageReceived -= LogMessageHandler;
			}
		}

		/// <summary>
		/// 日志更新回调
		/// </summary>
		public Action onLogChange;

		private void LogMessageHandler(string logString, string stackTrace, LogType type)
		{
			if (type == LogType.Error && !string.IsNullOrEmpty(stackTrace))
				logString += "\n" + stackTrace;

			SetRecord(ref logString, type);

			onLogChange?.Invoke();
		}

		#region 日志存储
		private readonly ObjectPool<LogItem> pool = new ObjectPool<LogItem>();
		public readonly List<LogItem> logs = new List<LogItem>();
		/// <summary> 最新一条日志 </summary>
		public LogItem LastLog { get; private set; }

		private int _maxLogCount = 1000;
		private int _removeLogCount = 100;
		/// <summary> 最大日志数 </summary>
		public int MaxLogCount
		{
			get => _maxLogCount;
			set
			{
				value = Math.Min(value, 100);
				if (_maxLogCount == value)
					return;

				_maxLogCount = value;
				_removeLogCount = (int)(value * 0.1f);// 默认超过最大限制，每次删除十分之一的日志
			}
		}

		private void SetRecord(ref string message, LogType type)
		{
			if (LastLog?.message == message)
			{
				LastLog.count += 1;
			}
			else
			{
				LastLog = pool.Get();
				LastLog.index = logs.Count + 1;
				LastLog.time = DateTime.Now.ToString("MM-dd HH:mm:ss");
				LastLog.type = type;
				LastLog.message = message;
				LastLog.count = 1;

				logs.Add(LastLog);
			}

			if (logs.Count > _maxLogCount)
			{
				for (int i = 0; i < _removeLogCount; ++i)
				{
					pool.Add(logs[i]);
				}

				logs.RemoveRange(0, _removeLogCount);
			}
		}

		public void Clear()
		{
			logs.Clear();
		}
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

	public class LogItem : IRecycle
	{
		public int index;
		public string time;
		public LogType type;
		public string message;
		public int count;

		public void Clear() { }
		public void Destroy() { }
	}
}