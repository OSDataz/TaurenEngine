/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2021/12/11 18:35:24
 *└────────────────────────┘*/

namespace Pro.TexasHoldEM
{
	public class PlayerOp
	{
		/// <summary>
		/// 玩家ID
		/// </summary>
		public int playerId;
		/// <summary>
		/// 玩家操作
		/// </summary>
		public PlayerOpType op;
		/// <summary>
		/// 操作金额
		/// </summary>
		public long money;
	}
}