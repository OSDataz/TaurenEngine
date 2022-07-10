/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/4/6 9:38:20
 *└────────────────────────┘*/

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 扑克统计数据
	/// </summary>
	public class PokerStats
	{
		/// <summary>
		/// 手牌统计数据
		/// </summary>
		public PokerStatsItem handStats;
		/// <summary>
		/// 公牌统计数据
		/// </summary>
		public PokerStatsItem publicStats;
	}

	public class PokerStatsItem
	{
		/// <summary>
		/// 出现次数
		/// </summary>
		public int count;
		/// <summary>
		/// 胜利次数（包含翻牌和不翻牌）
		/// </summary>
		public int winCount;
		/// <summary>
		/// 翻牌胜利次数
		/// </summary>
		public int showWinCount;
	}
}