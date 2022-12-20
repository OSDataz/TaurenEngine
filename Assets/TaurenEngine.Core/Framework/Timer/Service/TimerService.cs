/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/5 12:44:01
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 计时器服务
	/// </summary>
	internal class TimerService : ITimerService
	{
		public LoopList<Timer> UpdateList { get; } = new LoopList<Timer>();
		public LoopList<Timer> LateUpdateList { get; } = new LoopList<Timer>();
		public LoopList<Timer> FixedUpdateList { get; } = new LoopList<Timer>();

		public ObjectPool<Timer> TimerPool { get; }

		public TimerService()
		{
			this.InitInterface();
			TimerPool = PoolHelper.GetPool<Timer>();
		}
	}
}