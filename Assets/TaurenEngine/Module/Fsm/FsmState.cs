/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/12/14 20:50:25
 *└────────────────────────┘*/

namespace TaurenEngine.ModFsm
{
	/// <summary>
	/// 有限状态机状态基类
	/// </summary>
	public abstract class FsmState
	{
		/// <summary>
		/// 有限状态机状态初始化时调用
		/// </summary>
		protected internal virtual void OnInit() { }

		/// <summary>
		/// 有限状态机状态进入时调用
		/// </summary>
		protected internal virtual void OnEnter() { }

		/// <summary>
		/// 有限状态机状态离开时调用
		/// </summary>
		/// <param name="isDestroy">状态机是否被销毁</param>
		protected internal virtual void OnLeave(bool isDestroy) { }

		/// <summary>
		/// 有限状态机状态销毁时调用
		/// </summary>
		protected internal virtual void OnDestroy() { }
	}
}