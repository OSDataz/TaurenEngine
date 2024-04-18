/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2021/12/5 0:06:37
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;

namespace Pro.TexasHoldEM
{
	public class PokerPool : Singleton<PokerPool>
	{
		public readonly Poker defaultPoker = new Poker(0, Pattern.None, Figure.None);

		public Poker[] Pokers { get; private set; }

		public PokerPool()
		{
			var patterns = Enum.GetValues(typeof(Pattern));
			var figures = Enum.GetValues(typeof(Figure));

			var pLen = patterns.Length;
			var fLen = figures.Length;
			var toId = 0;

			Pokers = new Poker[(pLen - 1) * (fLen - 1)];

			var index = 0;
			for (int i = fLen - 1; i >= 1; --i)
			{
				for (int j = 1; j < pLen; ++j)
				{
					Pokers[index++] = new Poker(++toId, (Pattern)patterns.GetValue(j), (Figure)figures.GetValue(i));
				}
			}
		}

		public Poker Get(Pattern pattern, Figure figure)
		{
			var len = Pokers.Length;
			for (int i = 0; i < len; ++i)
			{
				if (Pokers[i].Figure == figure && Pokers[i].Pattern == pattern)
					return Pokers[i];
			}

			return defaultPoker;
		}

		public Poker Get(int id)
		{
			var len = Pokers.Length;
			for (int i = 0; i < len; ++i)
			{
				if (Pokers[i].Id == id)
					return Pokers[i];
			}

			return defaultPoker;
		}
	}
}