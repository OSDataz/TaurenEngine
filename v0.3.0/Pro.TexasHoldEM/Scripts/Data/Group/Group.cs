/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/3/29 13:59:06
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 扑克组合
	/// </summary>
	public class Group
	{
		public List<Poker> List { get; private set; }

		public Group()
		{
			List = new List<Poker>();
		}

		public Poker this[int index] => List[index];

		public virtual void Clear()
		{
			List.Clear();
		}

		public void Sort()
			=> List.Sort(Sort);

		private int Sort(Poker a, Poker b)
		{
			var value = a.Figure - b.Figure;
			if (value > 0) return -1;
			else if (value < 0) return 1;
			else return a.Pattern - b.Pattern;
		}
	}
}