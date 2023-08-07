/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/15 17:56:29
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Runtime.Framework
{
	public partial class Timer
	{
		#region 列表管理
		internal static readonly TimerList UpdateList = new TimerList();
		internal static readonly TimerList LateUpdateList = new TimerList();
		internal static readonly TimerList FixedUpdateList = new TimerList();
		#endregion

		#region 调用接口
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
		/// <param name="action"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer Create(Action action, float interval, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.UpdateInterval, interval, action, isLoop, autoStart);

		/// <summary>
		/// Update Interval带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer Create(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true)
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
		/// <param name="action"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateLate(Action action, float interval, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.LateUpdateInterval, interval, action, isLoop, autoStart);

		/// <summary>
		/// LateUpdate Interval带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateLate(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true)
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
		/// <param name="action"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateFixed(Action action, float interval, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.FixedUpdateInterval, interval, action, isLoop, autoStart);

		/// <summary>
		/// FixedUpdate Interval带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		public static Timer CreateFixed(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.FixedUpdateInterval, interval, action, param, isLoop, autoStart);

		private static Timer Create(TimerType type, float interval, Action action, bool isLoop, bool autoStart)
		{
			if (action == null)
			{
				Log.Error($"{type} Create action = null");
				return null;
			}

			var timer = GetPool().Get();
			timer.Type = type;
			timer.Interval = interval;
			timer.IsInterval = interval > 0.0f;
			timer.IsLoop = isLoop;
			timer.callAction = action;
			if (autoStart) timer.Start();

			return timer;
		}

		private static Timer Create(TimerType type, float interval, Action<object> action, object param, bool isLoop, bool autoStart)
		{
			if (action == null)
			{
				Log.Error($"{type} Create action = null");
				return null;
			}

			var timer = GetPool().Get();
			timer.Type = type;
			timer.Interval = interval;
			timer.IsInterval = interval > 0.0f;
			timer.IsLoop = isLoop;
			timer.callParamAction = action;
			timer.param = param;
			if (autoStart) timer.Start();

			return timer;
		}
		#endregion
	}
}