/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/11 17:48:42
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 一局游戏
	/// </summary>
	public class Game
	{
		/// <summary>
		/// 轮次数据
		/// </summary>
		public List<Round> rounds;

		/// <summary>
		/// 手牌<playerid, 扑克id>
		/// </summary>
		public Dictionary<int, int[]> handCards;
		/// <summary>
		/// 公牌
		/// </summary>
		public List<int> publicCards;

		/// <summary>
		/// 玩家操作
		/// </summary>
		public List<PlayerOp> playerOps;

		/// <summary>
		/// 盲注（小盲）
		/// </summary>
		public long blindMoney;
	}
}