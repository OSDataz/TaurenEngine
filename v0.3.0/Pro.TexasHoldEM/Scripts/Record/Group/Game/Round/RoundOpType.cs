/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/11 18:09:00
 *└────────────────────────┘*/

namespace Pro.TexasHoldEM
{
	public enum RoundOpType
	{
		/// <summary>
		/// 小盲
		/// </summary>
		SmallBlind,
		/// <summary>
		/// 大盲
		/// </summary>
		BigBlind,
		/// <summary>
		/// 跟注
		/// </summary>
		Call,
		/// <summary>
		/// 过牌
		/// </summary>
		Check,
		/// <summary>
		/// 下注所有筹码
		/// </summary>
		Allin,
		/// <summary>
		/// 盖牌、弃牌
		/// </summary>
		Muck,
	}
}