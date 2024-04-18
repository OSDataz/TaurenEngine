/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2021/12/4 13:07:24
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 扑克花色
	/// </summary>
	public enum Pattern
	{
		/// <summary>
		/// 默认（盖住的或者大小王）
		/// </summary>
		None = 0,
		/// <summary>
		/// 梅花
		/// </summary>
		[Tag("梅花")]
		Club = 1,
		/// <summary>
		/// 方块
		/// </summary>
		[Tag("方块")]
		Diamond = 2,
		/// <summary>
		/// 红桃
		/// </summary>
		[Tag("红桃")]
		Heart = 3,
		/// <summary>
		/// 黑桃
		/// </summary>
		[Tag("黑桃")]
		Spade = 4
	}
}