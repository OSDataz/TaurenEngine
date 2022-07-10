/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/3/25 9:51:13
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 5张牌组合池
	/// </summary>
	public partial class GroupPool
	{
		public List<Group5> Group5s { get; private set; }
		public Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Group5>>>>> Group5Map { get; private set; }

		public void InitGroup5()
		{
			Group5s = new List<Group5>();
			Group5Map = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Group5>>>>>();

			var list = PokerPool.Instance.Pokers;
			var len = list.Length;
			Group5 group5;
			for (int i1 = 0; i1 < len; ++i1)
			{
				var poker1 = list[i1];
				var map4 = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Group5>>>>();
				Group5Map.Add(poker1.Id, map4);

				for (int i2 = i1 + 1; i2 < len; ++i2)
				{
					var poker2 = list[i2];
					var map3 = new Dictionary<int, Dictionary<int, Dictionary<int, Group5>>>();
					map4.Add(poker2.Id, map3);

					for (int i3 = i2 + 1; i3 < len; ++i3)
					{
						var poker3 = list[i3];
						var map2 = new Dictionary<int, Dictionary<int, Group5>>();
						map3.Add(poker3.Id, map2);

						for (int i4 = i3 + 1; i4 < len; ++i4)
						{
							var poker4 = list[i4];
							var map1 = new Dictionary<int, Group5>();
							map2.Add(poker4.Id, map1);

							for (int i5 = i4 + 1; i5 < len; ++i5)
							{
								var poker5 = list[i5];

								group5 = new Group5();
								group5.List.Add(poker1);
								group5.List.Add(poker2);
								group5.List.Add(poker3);
								group5.List.Add(poker4);
								group5.List.Add(poker5);

								Group5s.Add(group5);
								map1.Add(poker5.Id, group5);

								Group2nCalc.CalcGroup5Data(group5);
							}
						}
					}
				}
			}

			// 牌力排序
			Group5s.Sort((a, b) => b.value - a.value);

			// 整理牌力值
			int value = 0;
			int rank = 0;
			for (int i = Group5s.Count - 1; i >= 0; --i)
			{
				group5 = Group5s[i];
				group5.index = i;

				if (group5.value != value)
				{
					value = group5.value;
					group5.value = ++rank;
				}
				else
				{
					group5.value = rank;
				}
			}
		}

		#region 精简配置
		/// <summary>
		/// 生成配置文件
		/// </summary>
		/// <returns></returns>
		public string GenerateGroup5Config()
		{
			InitGroup5();

			var builder = new StringBuilder();
			var len = Group5s.Count;
			Group5 group5;
			for (int i = 0; i < len; ++i)
			{
				group5 = Group5s[i];

				builder.AppendLine($"{group5.List[0].Id} {group5.List[1].Id} {group5.List[2].Id} {group5.List[3].Id} {group5.List[4].Id} {group5.value} {(int)group5.type}");
			}

			return builder.ToString();
		}

		/// <summary>
		/// 解析配置表
		/// </summary>
		/// <param name="config"></param>
		public void ParseGroup5Config(string config)
		{
			Group5s = new List<Group5>();
			Group5Map = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Group5>>>>>();

			var pool = PokerPool.Instance;

			var list = config.Split('\n');
			var len = list.Length;
			for (int i = 0; i < len; ++i)
			{
				if (string.IsNullOrEmpty(list[i]))
					continue;

				var slist = list[i].Split(' ');
				var group5 = new Group5();
				group5.List.Add(pool.Get(Convert.ToInt32(slist[0])));
				group5.List.Add(pool.Get(Convert.ToInt32(slist[1])));
				group5.List.Add(pool.Get(Convert.ToInt32(slist[2])));
				group5.List.Add(pool.Get(Convert.ToInt32(slist[3])));
				group5.List.Add(pool.Get(Convert.ToInt32(slist[4])));
				group5.value = Convert.ToInt32(slist[5]);
				group5.type = (GroupType)Convert.ToInt32(slist[6]);
				group5.index = i;
				
				Group5s.Add(group5);
				if (!Group5Map.TryGetValue(group5.List[0].Id, out var map4))
				{
					map4 = new Dictionary<int, Dictionary<int, Dictionary<int, Dictionary<int, Group5>>>>();
					Group5Map.Add(group5.List[0].Id, map4);
				}

				if (!map4.TryGetValue(group5.List[1].Id, out var map3))
				{
					map3 = new Dictionary<int, Dictionary<int, Dictionary<int, Group5>>>();
					map4.Add(group5.List[1].Id, map3);
				}

				if (!map3.TryGetValue(group5.List[2].Id, out var map2))
				{
					map2 = new Dictionary<int, Dictionary<int, Group5>>();
					map3.Add(group5.List[2].Id, map2);
				}

				if (!map2.TryGetValue(group5.List[3].Id, out var map1))
				{
					map1 = new Dictionary<int, Group5>();
					map2.Add(group5.List[3].Id, map1);
				}

				map1.Add(group5.List[4].Id, group5);
			}
		}
		#endregion
	}
}