/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/4 23:37:42
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace Pro.TexasHoldEM
{
	public class PlayerPool
	{
		public readonly List<Player> players = new List<Player>();

		public void Init()
		{
			
		}

		public bool Get(string name, out Player player)
		{
			player = players.Find(item => item.name == name);
			if (player != null)
				return true;

			player = new Player();
			player.name = name;
			players.Add(player);

			return false;
		}
	}
}