/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/5 12:35:35
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine
{
	public interface ITimerService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ITimerService Instance { get; internal set; }

		/// <summary>
		/// Update循环列表
		/// </summary>
		LoopList<Timer> UpdateList { get; }

		/// <summary>
		/// LateUpdate循环列表
		/// </summary>
		LoopList<Timer> LateUpdateList { get; }

		/// <summary>
		/// FixedUpdate循环列表
		/// </summary>
		LoopList<Timer> FixedUpdateList { get; }

		/// <summary>
		/// Timer对象池
		/// </summary>
		ObjectPool<Timer> TimerPool { get; }
	}

	public static class ITimerServiceExtension
	{
		public static void InitInterface(this ITimerService @object)
		{
			if (ITimerService.Instance != null)
				Debug.LogError("ITimerService重复创建实例");

			ITimerService.Instance = @object;
		}
	}
}