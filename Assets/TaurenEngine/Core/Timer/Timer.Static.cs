﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/26 23:47:42
 *└────────────────────────┘*/

using System;

namespace TaurenEngine
{
	public partial class Timer
	{
		private static readonly LoopList<Timer> updateList = InstanceManager.Instance.Get<TimerData>().updateList;
		private static readonly LoopList<Timer> lateUpdateList = InstanceManager.Instance.Get<TimerData>().updateList;
		private static readonly LoopList<Timer> fixedUpdateList = InstanceManager.Instance.Get<TimerData>().updateList;

		/// <summary>
		/// Update
		/// </summary>
		/// <param name="action"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer Create(Action action, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.Update, 0.0f, action, isLoop, autoStart);

		/// <summary>
		/// Update带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer Create(Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.Update, 0.0f, action, param, isLoop, autoStart);

		/// <summary>
		/// Update Interval
		/// </summary>
		/// <param name="interval"></param>
		/// <param name="action"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer Create(float interval, Action action, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.UpdateInterval, interval, action, isLoop, autoStart);

		/// <summary>
		/// Update Interval带参
		/// </summary>
		/// <param name="interval"></param>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer Create(float interval, Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.UpdateInterval, interval, action, param, isLoop, autoStart);

		/// <summary>
		/// LateUpdate
		/// </summary>
		/// <param name="action"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateLate(Action action, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.LateUpdate, 0.0f, action, isLoop, autoStart);

		/// <summary>
		/// LateUpdate带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateLate(Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.LateUpdate, 0.0f, action, param, isLoop, autoStart);

		/// <summary>
		/// LateUpdate Interval
		/// </summary>
		/// <param name="interval"></param>
		/// <param name="action"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateLate(float interval, Action action, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.LateUpdateInterval, interval, action, isLoop, autoStart);

		/// <summary>
		/// LateUpdate Interval带参
		/// </summary>
		/// <param name="interval"></param>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateLate(float interval, Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.LateUpdateInterval, interval, action, param, isLoop, autoStart);

		/// <summary>
		/// FixedUpdate
		/// </summary>
		/// <param name="action"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateFixed(Action action, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.FixedUpdate, 0.0f, action, isLoop, autoStart);

		/// <summary>
		/// FixedUpdate带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateFixed(Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.FixedUpdate, 0.0f, action, param, isLoop, autoStart);

		/// <summary>
		/// FixedUpdate Interval
		/// </summary>
		/// <param name="interval"></param>
		/// <param name="action"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateFixed(float interval, Action action, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.FixedUpdateInterval, interval, action, isLoop, autoStart);

		/// <summary>
		/// FixedUpdate Interval带参
		/// </summary>
		/// <param name="interval"></param>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateFixed(float interval, Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.FixedUpdateInterval, interval, action, param, isLoop, autoStart);

		private static Timer Create(TimerType type, float interval, Action action, bool isLoop, bool autoStart)
		{
			if (action == null)
			{
				DebugEx.Error($"{type} Create action = null");
				return null;
			}

			var timer = GetFromPool();
			timer.type = type;
			timer.interval = interval;
			timer.IsInterval = interval > 0.0f;
			timer.isLoop = isLoop;
			timer.callAction = action;
			if (autoStart) timer.Start();

			return timer;
		}

		private static Timer Create(TimerType type, float interval, Action<object> action, object param, bool isLoop, bool autoStart)
		{
			if (action == null)
			{
				DebugEx.Error($"{type} Create action = null");
				return null;
			}

			var timer = GetFromPool();
			timer.type = type;
			timer.interval = interval;
			timer.IsInterval = interval > 0.0f;
			timer.isLoop = isLoop;
			timer.callParamAction = action;
			timer.param = param;
			if (autoStart) timer.Start();

			return timer;
		}
	}
}