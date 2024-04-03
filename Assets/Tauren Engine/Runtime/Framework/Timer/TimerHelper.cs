/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/2 14:14:07
 *└────────────────────────┘*/

using System;

namespace Tauren.Framework.Runtime
{
	public static class TimerHelper
	{
		public static ITimer Create(Action action, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, isLoop, autoStart);

		public static ITimer Create(Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, param, isLoop, autoStart);

		public static ITimer Create(Action action, float interval, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, interval, isLoop, autoStart);

		public static ITimer Create(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, param, interval, isLoop, autoStart);

		public static ITimer CreateLate(Action action, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, isLoop, autoStart);

		public static ITimer CreateLate(Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, param, isLoop, autoStart);

		public static ITimer CreateLate(Action action, float interval, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, interval, isLoop, autoStart);

		public static ITimer CreateLate(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, param, interval, isLoop, autoStart);

		public static ITimer CreateFixed(Action action, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, isLoop, autoStart);

		public static ITimer CreateFixed(Action<object> action, object param, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, param, isLoop, autoStart);

		public static ITimer CreateFixed(Action action, float interval, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, interval, isLoop, autoStart);

		public static ITimer CreateFixed(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true)
			=> ITimerService.Instance.Create(action, param, interval, isLoop, autoStart);
	}
}