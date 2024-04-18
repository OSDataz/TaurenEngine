/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2021/12/4 23:22:08
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace Pro.TexasHoldEM
{
	public partial class GameData
	{
		private readonly PlayerPool playerPool = new PlayerPool();

		/// <summary>
		/// 主玩家
		/// </summary>
		public readonly GamePlayer mainPlayer = new GamePlayer();
		/// <summary>
		/// 其他玩家
		/// </summary>
		public readonly List<GamePlayer> players = new List<GamePlayer>();

		private void ClaerPlayer()
		{
			
		}

		private void InitPlayer()
		{
			playerPool.Get("Main Player", out var player);
			mainPlayer.player = player;
		}

		public void AddPlayer()
		{
			
		}

		public void RemovePlayer()
		{
			
		}
	}
}