/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/29 20:30:28
 *└────────────────────────┘*/

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 牌型
	/// </summary>
	public enum GroupType
	{
		None = -1,
		/// <summary>
		/// 高牌
		/// </summary>
		HighCard = 0,
		/// <summary>
		/// 一对
		/// </summary>
		OnePair = 1,
		/// <summary>
		/// 两对
		/// </summary>
		TwoRair = 2,
		/// <summary>
		/// 三条
		/// </summary>
		ThreeOfAKind = 3,
		/// <summary>
		/// 顺子
		/// </summary>
		Straight = 4,
		/// <summary>
		/// 同花
		/// </summary>
		Flush = 5,
		/// <summary>
		/// 葫芦
		/// </summary>
		FullHouse = 6,
		/// <summary>
		/// 四条(金刚)
		/// </summary>
		FourOfAKind = 7,
		/// <summary>
		/// 同花顺
		/// </summary>
		StraightFlush = 8,
		/// <summary>
		/// 皇家同花顺
		/// </summary>
		RoyalFlush = 9
	}
}