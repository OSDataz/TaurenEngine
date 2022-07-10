/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/3/29 13:54:50
 *└────────────────────────┘*/

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 牌局扑克数据
	/// </summary>
	public class GroupData
	{
		/// <summary>
		/// 手牌数据
		/// </summary>
		public readonly Group handPokers = new Group();
		/// <summary>
		/// 公牌数据
		/// </summary>
		public readonly Group publicPokers = new Group();
		/// <summary>
		/// 最大组合数据
		/// </summary>
		public Group5 maxPokers;

		public void Clear()
		{
			handPokers.Clear();
			publicPokers.Clear();
		}
	}
}