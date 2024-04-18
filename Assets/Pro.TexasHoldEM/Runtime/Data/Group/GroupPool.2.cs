/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.4.0
 *│　Time    ：2022/3/25 9:53:17
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 2张牌组合池
	/// </summary>
	public partial class GroupPool
	{
		public List<Group2> Group2s { get; private set; }

		public void InitGroup2()
		{
			Group2s = new List<Group2>();

			var list = PokerPool.Instance.Pokers;
			var len = list.Length;

			Poker poker0;
			Group2 group2;
			for (int i1 = 0; i1 < len; ++i1)
			{
				poker0 = list[i1];
				for (int i2 = i1 + 1; i2 < len; ++i2)
				{
					group2 = new Group2();
					group2.List.Add(poker0);
					group2.List.Add(list[i2]);
					Group2s.Add(group2);

					Group2Calc.CalcGroup2Data(group2);
				}
			}

			// 牌力排序
			Group2s.Sort((a, b) => b.value - a.value);

			// 整理牌力值
			int value = 0;
			int rank = 0;
			for (int i = Group2s.Count - 1; i >= 0; --i)
			{
				group2 = Group2s[i];
				group2.index = i;

				if (group2.value != value)
				{
					value = group2.value;
					group2.value = ++rank;
				}
				else
				{
					group2.value = rank;
				}
			}
		}

		#region 精简配置
		/// <summary>
		/// 生成配置文件
		/// </summary>
		/// <returns></returns>
		public string GenerateGroup2Config()
		{
			InitGroup2();

			var builder = new StringBuilder();
			var len = Group2s.Count;

			Group2 group2;
			for (int i = 0; i < len; ++i)
			{
				group2 = Group2s[i];

				builder.AppendLine($"{group2.List[0].Id} {group2.List[1].Id} {group2.value}");
			}

			return builder.ToString();
		}

		/// <summary>
		/// 解析配置表
		/// </summary>
		/// <param name="config"></param>
		public void ParseGroup2Config(string config)
		{
			Group2s = new List<Group2>();

			var pool = PokerPool.Instance;

			var list = config.Split('\n');
			var len = list.Length;
			for (int i = 0; i < len; ++i)
			{
				if (string.IsNullOrEmpty(list[i]))
					continue;

				var slist = list[i].Split(' ');
				var group2 = new Group2();
				group2.List.Add(pool.Get(Convert.ToInt32(slist[0])));
				group2.List.Add(pool.Get(Convert.ToInt32(slist[1])));
				group2.value = Convert.ToInt32(slist[2]);
				group2.index = i;

				Group2s.Add(group2);
			}
		}
		#endregion
	}
}