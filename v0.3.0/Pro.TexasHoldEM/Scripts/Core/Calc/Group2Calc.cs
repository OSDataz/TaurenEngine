/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/3/25 9:23:35
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 两张手牌组合计算
	/// </summary>
	public static class Group2Calc
	{
		#region 计算2张牌
		/// <summary>
		/// 计算两张牌的战力，需先排序
		/// </summary>
		/// <param name="group2"></param>
		/// <returns></returns>
		public static void CalcGroup2Data(Group2 group2)
		{
			var list = group2.List;
			var id0 = list[0].Id;
			var id1 = list[1].Id;

			int value = 0;

			var group5s = GroupPool.Instance.Group5s;
			var len = group5s.Count;
			Group5 cell5;
			List<Poker> list5;
			int j;
			for (int i = 0; i < len; ++i)
			{
				cell5 = group5s[i];
				list5 = cell5.List;
				for (j = 0; j < 5; ++j)
				{
					if (list5[j].Id == id0 || list5[j].Id == id1)
					{
						value += cell5.value;
						break;
					}
				}
			}

			group2.value = value;
		}
		#endregion

		#region 计算胜率
		/// <summary>
		/// 计算两张牌胜率
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static float CalcWinningRate(Group group2)
		{
			var list = group2.List;
			var id0 = Mathf.Min(list[0].Id, list[1].Id);
			var id1 = Mathf.Max(list[0].Id, list[1].Id);

			var group2s = GroupPool.Instance.Group2s;
			var len = group2s.Count;
			List<Poker> list2;
			for (int i = 0; i < len; ++i)
			{
				list2 = group2s[i].List;
				if (list2[0].Id == id0 && list2[1].Id == id1)
				{
					return (float)group2s[i].value / group2s[0].value;
				}
			}

			return 0.0f;
		}
		#endregion
	}
}