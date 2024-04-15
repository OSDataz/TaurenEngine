/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/7 11:09:06
 *└────────────────────────┘*/

using Tauren.Framework.Runtime;

namespace Tauren.Engine.Runtime
{
	/// <summary>
	/// 流程管理器
	/// </summary>
	public class ProcedureManager : Fsm<ProcedureBase>
	{
		protected override bool Add(ProcedureBase state)
		{
			if (base.Add(state))
			{
				state.manager = this;
				return true;
			}

			return false;
		}
	}
}