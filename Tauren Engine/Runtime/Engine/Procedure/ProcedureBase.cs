/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/7 9:54:30
 *└────────────────────────┘*/

using Tauren.Framework.Runtime;

namespace Tauren.Engine.Runtime
{
	/// <summary>
	/// 流程基类
	/// </summary>
	public abstract class ProcedureBase : FsmState
	{
		internal ProcedureManager manager;

		/// <summary>
		/// 切换流程
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public void ChangeProcedure<T>() where T : ProcedureBase, new()
		{
			manager?.Change<T>();
		}
	}
}