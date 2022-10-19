/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/5 12:35:35
 *└────────────────────────┘*/

namespace TaurenEngine
{
	public interface ITimerService : IService
	{
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
}