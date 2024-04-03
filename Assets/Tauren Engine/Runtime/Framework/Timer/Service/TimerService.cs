/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/2 14:10:08
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public class TimerService : ITimerService
	{
		public TimerService() 
		{
			this.InitInterface();
		}

		#region 调用接口
		public ITimer Create(Action action, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.Update, 0.0f, action, isLoop, autoStart);

		public ITimer Create(Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.Update, 0.0f, action, param, isLoop, autoStart);

		public ITimer Create(Action action, float interval, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.UpdateInterval, interval, action, isLoop, autoStart);

		public ITimer Create(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.UpdateInterval, interval, action, param, isLoop, autoStart);

		public ITimer CreateLate(Action action, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.LateUpdate, 0.0f, action, isLoop, autoStart);

		public ITimer CreateLate(Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.LateUpdate, 0.0f, action, param, isLoop, autoStart);

		public ITimer CreateLate(Action action, float interval, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.LateUpdateInterval, interval, action, isLoop, autoStart);

		public ITimer CreateLate(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.LateUpdateInterval, interval, action, param, isLoop, autoStart);

		public ITimer CreateFixed(Action action, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.FixedUpdate, 0.0f, action, isLoop, autoStart);

		public ITimer CreateFixed(Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.FixedUpdate, 0.0f, action, param, isLoop, autoStart);

		public ITimer CreateFixed(Action action, float interval, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.FixedUpdateInterval, interval, action, isLoop, autoStart);

		public ITimer CreateFixed(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true)
			=> Create(TimerType.FixedUpdateInterval, interval, action, param, isLoop, autoStart);

		private static Timer Create(TimerType type, float interval, Action action, bool isLoop, bool autoStart)
		{
			if (action == null)
			{
				Log.Error($"{type} Create action = null");
				return null;
			}

			var timer = Timer.GetFromPool();
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

			var timer = Timer.GetFromPool();
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