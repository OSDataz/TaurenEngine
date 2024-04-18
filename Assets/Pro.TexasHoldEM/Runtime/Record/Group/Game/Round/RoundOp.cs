/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2021/12/11 18:36:25
 *└────────────────────────┘*/

namespace Pro.TexasHoldEM
{
	public class RoundOp
	{
		/// <summary>
		/// 玩家ID
		/// </summary>
		public int playerId;
		/// <summary>
		/// 玩家操作
		/// </summary>
		public RoundOpType op;
		/// <summary>
		/// 操作金额
		/// </summary>
		public long money;
	}
}