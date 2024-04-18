/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2021/11/9 11:29:35
 *└────────────────────────┘*/

namespace Pro.TexasHoldEM
{
	public class Poker
	{
		/// <summary>
		/// 扑克ID
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// 扑克花色
		/// </summary>
		public Pattern Pattern { get; private set; }
		/// <summary>
		/// 扑克大小
		/// </summary>
		public Figure Figure { get; private set; }

		#region 统计数据
		/// <summary>
		/// 统计数据
		/// </summary>
		public readonly PokerStats Stats = new PokerStats();
		#endregion

		#region 临时计算数据
		/// <summary>
		/// 扑克所属类型
		/// </summary>
		public PokerType type;
		#endregion

		public Poker(int id, Pattern pattern, Figure figure)
		{
			Id = id;
			Pattern = pattern;
			Figure = figure;
		}

		public override string ToString()
		{
			string str = "";
			if (Pattern == Pattern.Club) str = "梅";
			else if (Pattern == Pattern.Diamond) str = "方";
			else if (Pattern == Pattern.Heart) str = "红";
			else if (Pattern == Pattern.Spade) str = "黑";

			return str + Figure.ToString().Substring(1);
		}
	}

	public enum PokerType
	{
		None,
		/// <summary>
		/// 手牌
		/// </summary>
		Hand,
		/// <summary>
		/// 公牌
		/// </summary>
		Public
	}
}