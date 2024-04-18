/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2021/12/4 13:07:46
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 扑克大小
	/// </summary>
	public enum Figure
	{
		/// <summary>
		/// 盖住的
		/// </summary>
		None = 0,
		[Tag("2")]
		_2 = 2,
		[Tag("3")]
		_3 = 3,
		[Tag("4")]
		_4 = 4,
		[Tag("5")]
		_5 = 5,
		[Tag("6")]
		_6 = 6,
		[Tag("7")]
		_7 = 7,
		[Tag("8")]
		_8 = 8,
		[Tag("9")]
		_9 = 9,
		[Tag("10")]
		_10 = 10,
		[Tag("J")]
		_J = 11,
		[Tag("Q")]
		_Q = 12,
		[Tag("K")]
		_K = 13,
		[Tag("A")]
		_A = 14,
	}
}