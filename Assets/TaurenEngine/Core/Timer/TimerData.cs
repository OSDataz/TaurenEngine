/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/31 11:35:18
 *└────────────────────────┘*/

namespace TaurenEngine
{
	public sealed class TimerData
	{
		/// <summary>
		/// Update循环列表
		/// </summary>
		public readonly LoopList<Timer> updateList = new LoopList<Timer>();

		/// <summary>
		/// LateUpdate循环列表
		/// </summary>
		public readonly LoopList<Timer> lateUpdateList = new LoopList<Timer>();

		/// <summary>
		/// FixedUpdate循环列表
		/// </summary>
		public readonly LoopList<Timer> fixedUpdateList = new LoopList<Timer>();
	}
}