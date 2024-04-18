/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2021/12/5 17:17:42
 *└────────────────────────┘*/

namespace Pro.TexasHoldEM
{
	public class CardGroup
	{
		/// <summary>
		/// 公牌
		/// </summary>
		private GamePoker[] _publicCards;
		private const int _maxLen = 5;

		public void Init()
		{
			_publicCards = new GamePoker[_maxLen];
			for (int i = 0; i < _maxLen; ++i)
			{
				_publicCards[i] = new GamePoker();
			}
		}

		public void Clear()
		{
			for (int i = 0; i < _maxLen; ++i)
			{
				_publicCards[i].Clear();
			}
		}


	}
}