/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/3/29 14:07:11
 *└────────────────────────┘*/

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 5张牌组合
	/// </summary>
	public class Group5 : Group
	{
		/// <summary>
		/// 排序
		/// </summary>
		public int index;
		/// <summary>
		/// 牌力值
		/// </summary>
		public int value;
		/// <summary>
		/// 牌型
		/// </summary>
		public GroupType type;

		#region 临时计算数据
		/// <summary>
		/// 共同的组合（全公牌）
		/// </summary>
		public bool isCommon;
		#endregion

		public override void Clear()
		{


			base.Clear();
		}
	}
}