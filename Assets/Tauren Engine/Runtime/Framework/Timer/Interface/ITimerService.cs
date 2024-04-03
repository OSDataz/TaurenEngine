/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/2 21:12:36
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public interface ITimerService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ITimerService Instance { get; internal set; }

		/// <summary>
		/// Update
		/// </summary>
		/// <param name="action"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer Create(Action action, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// Update带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer Create(Action<object> action, object param, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// Update Interval
		/// </summary>
		/// <param name="action"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer Create(Action action, float interval, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// Update Interval带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer Create(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// LateUpdate
		/// </summary>
		/// <param name="action"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer CreateLate(Action action, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// LateUpdate带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer CreateLate(Action<object> action, object param, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// LateUpdate Interval
		/// </summary>
		/// <param name="action"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer CreateLate(Action action, float interval, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// LateUpdate Interval带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer CreateLate(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// FixedUpdate
		/// </summary>
		/// <param name="action"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer CreateFixed(Action action, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// FixedUpdate带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer CreateFixed(Action<object> action, object param, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// FixedUpdate Interval
		/// </summary>
		/// <param name="action"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer CreateFixed(Action action, float interval, bool isLoop = false, bool autoStart = true);

		/// <summary>
		/// FixedUpdate Interval带参
		/// </summary>
		/// <param name="action"></param>
		/// <param name="param"></param>
		/// <param name="interval"></param>
		/// <param name="isLoop"></param>
		/// <param name="autoStart"></param>
		/// <returns></returns>
		ITimer CreateFixed(Action<object> action, object param, float interval, bool isLoop = false, bool autoStart = true);
	}

	public static class ITimerServiceExtension
	{
		public static void InitInterface(this ITimerService @object)
		{
			ITimerService.Instance = @object;
		}
	}
}