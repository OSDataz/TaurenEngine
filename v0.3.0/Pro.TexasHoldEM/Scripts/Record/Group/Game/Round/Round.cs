/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/11 17:50:38
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 一轮游戏（游戏一回合）
	/// </summary>
	public class Round
	{
		/// <summary>
		/// 轮次操作
		/// </summary>
		public List<RoundOp> roundOps;
	}
}