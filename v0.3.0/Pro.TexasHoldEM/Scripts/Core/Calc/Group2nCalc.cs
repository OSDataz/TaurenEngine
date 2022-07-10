/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/3/25 9:27:39
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Core;

namespace Pro.TexasHoldEM
{
	/// <summary>
	/// 两张手牌+底牌组合计算
	/// </summary>
	public static class Group2nCalc
	{
		#region 统计对象
		private static StatsFigure statsFigure = new StatsFigure();
		private static StatsPattern statsPattern = new StatsPattern();

		private class StatsList<T>
		{
			protected class Item
			{
				public T key;
				public byte count;
			}

			protected readonly Item[] items;

			public int Length { get; private set; }

			public StatsList(int count)
			{
				items = new Item[count];
				for (int i = 0; i < count; ++i)
				{
					items[i] = new Item();
				}
			}

			public void Clear()
			{
				Length = 0;
			}

			public void Add(T key)
			{
				for (int i = 0; i < Length; ++i)
				{
					if (items[i].key.Equals(key))
					{
						items[i].count += 1;
						return;
					}
				}

				AddNew(key);
			}

			public void AddNew(T key)
			{
				var item = items[Length];
				item.key = key;
				item.count = 1;

				Length += 1;
			}

			public void AddCount()
			{
				items[Length - 1].count += 1;
			}

			public T GetKey(int index) => items[index].key;
		}

		private class StatsFigure : StatsList<Figure>
		{
			private readonly List<Item> _sortList;

			public StatsFigure() : base(7)
			{
				_sortList = new List<Item>();
			}

			#region 点数数量排序
			public void SortCount()
			{
				_sortList.Clear();

				for (int i = 0; i < Length; ++i)
				{
					_sortList.Add(items[i]);
				}

				_sortList.Sort(SortCount);
			}

			private int SortCount(Item a, Item b)
			{
				if (a.count > b.count) return -1;
				else if (a.count < b.count) return 1;
				else return b.key - a.key;
			}

			public Figure GetSortKey(int index) => _sortList[index].key;
			public int GetSortCount(int index) => _sortList[index].count;
			public int MaxCount => GetSortCount(0);
			#endregion
		}

		private class StatsPattern : StatsList<Pattern>
		{
			public StatsPattern() : base(4) { }

			/// <summary>
			/// 同花花色
			/// </summary>
			public Pattern FlushPattern
			{
				get
				{
					if (Length == 4)// 7张牌计算，有4种花色必定没同花
						return Pattern.None;

					for (int i = 0; i < Length; ++i)
					{
						if (items[i].count >= 5)
							return items[i].key;
					}
					return Pattern.None;
				}
			}
		}
		#endregion

		#region 计算5张牌
		/// <summary>
		/// 需先排序
		/// </summary>
		/// <param name="group5"></param>
		public static void CalcGroup5Data(Group5 group5)
		{
			statsFigure.Clear();

			int value = 0;

			var list = group5.List;
			var first = list[0];

			var isA = first.Figure == Figure._A;
			var isA5 = isA && first.Figure - list[1].Figure == 9;// A5组合
			var isFlush = true;// 同花
			var isStraight = true;// 顺子

			statsFigure.Add(first.Figure);

			Poker poker;
			int diff;

			for (int i = 1; i < 5; ++i)
			{
				poker = list[i];

				// 检测同花
				if (isFlush)
				{
					if (first.Pattern != poker.Pattern)
						isFlush = false;
				}

				// 检测点数（顺子，金刚，三条，对子）
				diff = list[i - 1].Figure - poker.Figure;
				if (diff == 0)
				{
					statsFigure.AddCount();
					if (isStraight) isStraight = false;
				}
				else
				{
					statsFigure.AddNew(poker.Figure);

					if (isStraight && diff > 1 && !(isA5 && i == 1))
						isStraight = false;
				}
			}

			if (isFlush)
			{
				if (isStraight)
				{
					if (isA)
					{
						if (isA5)
						{
							// 同花顺
							value += SetType(group5, GroupType.StraightFlush);
							value += ToGroup1(statsFigure.GetKey(1));
						}
						else
						{
							// 皇家同花顺
							value += SetType(group5, GroupType.RoyalFlush);
						}
					}
					else
					{
						// 同花顺
						value += SetType(group5, GroupType.StraightFlush);
						value += ToGroup1(statsFigure.GetKey(0));
					}
				}
				else
				{
					// 同花
					value += SetType(group5, GroupType.Flush);
					value += ToGroup1(statsFigure.GetKey(0));
					value += ToGroup2(statsFigure.GetKey(1));
					value += ToGroup3(statsFigure.GetKey(2));
					value += ToGroup4(statsFigure.GetKey(3));
					value += ToGroup5(statsFigure.GetKey(4));
				}
			}
			else if (isStraight)
			{
				// 顺子
				value += SetType(group5, GroupType.Straight);
				value += ToGroup1(statsFigure.GetKey(0));
			}
			else
			{
				if (statsFigure.Length == 5)
				{
					// 高牌
					value += SetType(group5, GroupType.HighCard);
					value += ToGroup1(statsFigure.GetKey(0));
					value += ToGroup2(statsFigure.GetKey(1));
					value += ToGroup3(statsFigure.GetKey(2));
					value += ToGroup4(statsFigure.GetKey(3));
					value += ToGroup5(statsFigure.GetKey(4));
				}
				else
				{
					statsFigure.SortCount();

					if (statsFigure.Length == 4)
					{
						// 一对
						value += SetType(group5, GroupType.OnePair);
						value += ToGroup1(statsFigure.GetSortKey(0));
						value += ToGroup2(statsFigure.GetSortKey(1));
						value += ToGroup3(statsFigure.GetSortKey(2));
						value += ToGroup4(statsFigure.GetSortKey(3));
					}
					else if (statsFigure.Length == 3)
					{
						if (statsFigure.MaxCount == 3)
						{
							// 三条
							value += SetType(group5, GroupType.ThreeOfAKind);
						}
						else
						{
							// 两对
							value += SetType(group5, GroupType.TwoRair);
						}

						value += ToGroup1(statsFigure.GetSortKey(0));
						value += ToGroup2(statsFigure.GetSortKey(1));
						value += ToGroup3(statsFigure.GetSortKey(2));
					}
					else if (statsFigure.Length == 2)
					{
						if (statsFigure.MaxCount == 4)
						{
							// 金刚
							value += SetType(group5, GroupType.FourOfAKind);
						}
						else
						{
							// 葫芦
							value += SetType(group5, GroupType.FullHouse);
						}

						value += ToGroup1(statsFigure.GetSortKey(0));
						value += ToGroup2(statsFigure.GetSortKey(1));
					}
					else
						TDebug.Error("代码逻辑错误！");
				}
			}

			group5.value = value;
		}

		private static int SetType(Group5 group5, GroupType type)
		{
			group5.type = type;
			return (int)type << 20;
		}
		private static int ToGroup1(Figure figure) => (int)figure << 16;
		private static int ToGroup2(Figure figure) => (int)figure << 12;
		private static int ToGroup3(Figure figure) => (int)figure << 8;
		private static int ToGroup4(Figure figure) => (int)figure << 4;
		private static int ToGroup5(Figure figure) => (int)figure;
		#endregion

		#region 计算 2 + 3 + n 张牌
		private static Group tempGroup = new Group();
		private static Group maxGroup = new Group();

		public static void CalcGroup5nMax(GroupData data)
		{
			tempGroup.Clear();
			tempGroup.List.AddRange(data.handPokers.List);
			tempGroup.List.AddRange(data.publicPokers.List);
			tempGroup.Sort();

			data.maxPokers = GetGroup5nMax(tempGroup.List);
		}

		private static Group5 GetGroup5nMax(List<Poker> list)
		{
			CalcGroup5nMax(list);

			maxGroup.Sort();
			return GroupPool.Instance.Group5Map[maxGroup[0].Id][maxGroup[1].Id][maxGroup[2].Id][maxGroup[3].Id][maxGroup[4].Id];
		}

		private static void CalcGroup5nMax(List<Poker> list)
		{
			statsFigure.Clear();
			statsPattern.Clear();

			maxGroup.Clear();
			var maxList = maxGroup.List;

			var poker = list[0];
			Figure sStart = poker.Figure;
			Figure sEnd = poker.Figure;
			statsPattern.AddNew(poker.Pattern);
			statsFigure.AddNew(poker.Figure);

			bool isA = poker.Figure == Figure._A;
			bool isA5 = false;
			int diff;

			var len = list.Count;
			for (int i = 1; i < len; ++i)
			{
				poker = list[i];

				// 检测同花
				statsPattern.Add(poker.Pattern);

				// 检测点数（顺子，金刚，三条，对子）
				diff = list[i - 1].Figure - poker.Figure;
				if (diff == 0)
				{
					statsFigure.AddCount();
				}
				else
				{
					statsFigure.AddNew(poker.Figure);

					if (diff > 1)
					{
						diff = sEnd - sStart;
						if (diff < 4)
						{
							sStart = sEnd = poker.Figure;
						}
						else if (diff == 4)
						{
							if (sEnd == Figure._2 && isA)
								isA5 = true;
							else
								sStart = sEnd = poker.Figure;
						}
					}
					else
					{
						sEnd = poker.Figure;
					}
				}
			}

			var flushPattern = statsPattern.FlushPattern;// 同花花色
			var isStraight = isA5 || sStart - sEnd >= 4;// 顺子

			if (flushPattern == Pattern.None)
			{
				if (isStraight)
				{
					// 顺子
					Figure value = Figure.None;
					var i = GetStartFigureIndex(list, sStart);
					for (int ci = 0; ci < 5; ++i)
					{
						if (list[i].Figure != value)
						{
							value = list[i].Figure;
							maxList.Add(list[i]);
							ci += 1;
						}
					}
				}
				else
				{
					statsFigure.SortCount();

					var max0 = statsFigure.GetSortCount(0);
					if (max0 == 4)
					{
						// 金刚
						var i = GetStartFigureIndex(list, statsFigure.GetSortKey(0));
						maxList.Add(list[i]);
						maxList.Add(list[i + 1]);
						maxList.Add(list[i + 2]);
						maxList.Add(list[i + 3]);
						maxList.Add(list[i == 0 ? 4 : 0]);
					}
					else if (max0 == 3)
					{
						var max1 = statsFigure.GetSortCount(1);
						if (max1 >= 2)
						{
							// 葫芦
							var i = GetStartFigureIndex(list, statsFigure.GetSortKey(0));
							maxList.Add(list[i]);
							maxList.Add(list[i + 1]);
							maxList.Add(list[i + 2]);
							i = GetStartFigureIndex(list, statsFigure.GetSortKey(1));
							maxList.Add(list[i]);
							maxList.Add(list[i + 1]);
						}
						else
						{
							// 三条
							var i = GetStartFigureIndex(list, statsFigure.GetSortKey(0));
							maxList.Add(list[i]);
							maxList.Add(list[i + 1]);
							maxList.Add(list[i + 2]);
							for (int j = 0, li = 3; j < len && li < 5;)
							{
								if (j == i) j += 3;
								else
								{
									maxList.Add(list[j++]);
									li += 1;
								}
							}
						}
					}
					else if (max0 == 2)
					{
						var max1 = statsFigure.GetSortCount(1);
						if (max1 == 2)
						{
							// 两对
							var i = GetStartFigureIndex(list, statsFigure.GetSortKey(0));
							maxList.Add(list[i]);
							maxList.Add(list[i + 1]);
							var j = GetStartFigureIndex(list, statsFigure.GetSortKey(1));
							maxList.Add(list[j]);
							maxList.Add(list[j + 1]);
							for (int k = 0; k < len;)
							{
								if (k == i || k == j)
									k += 2;
								else
								{
									maxList.Add(list[k]);
									break;
								}
							}
						}
						else
						{
							// 一对
							var i = GetStartFigureIndex(list, statsFigure.GetSortKey(0));
							maxList.Add(list[i]);
							maxList.Add(list[i + 1]);
							for (int j = 0, li = 2; j < len && li < 5;)
							{
								if (j == i) j += 2;
								else
								{
									maxList.Add(list[j++]);
									li += 1;
								}
							}
						}
					}
					else
					{
						// 高牌
						for (int i = 0; i < 5; ++i)
						{
							maxList.Add(list[i]);
						}
					}
				}
			}
			else
			{
				if (isStraight)
				{
					var fsCount = 0;
					Figure sFigure = Figure.None;

					for (int i = 0; i < len; ++i)
					{
						if (list[i].Figure > sStart)
							continue;

						if (list[i].Figure < sEnd)
							break;

						if (list[i].Pattern == flushPattern)
						{
							if (sFigure == Figure.None)
								sFigure = list[i].Figure;

							fsCount += 1;
						}
						else
						{
							if (fsCount >= 5)
							{
								if (sFigure == Figure._A)
								{
									// 皇家同花顺
									for (int j = 0, li = 0; j < len && li < 5; ++j)
									{
										if (list[j].Pattern == flushPattern)
										{
											maxList.Add(list[j]);
											li += 1;
										}
									}
								}
								else
								{
									// 同花顺
									var j = GetStartFigureIndex(list, sFigure);
									for (int li = 0; j < len && li < 5; ++j)
									{
										if (list[j].Pattern == flushPattern)
										{
											maxList.Add(list[j]);
											li += 1;
										}
									}
								}

								return;
							}
							else if (fsCount == 4 && sFigure == Figure._5 && isA)
							{
								for (int j = 0; j < len; ++j)
								{
									if (list[j].Figure == Figure._A)
									{
										if (list[j].Pattern == flushPattern)
										{
											// A5同花顺
											maxList.Add(list[j]);// A

											var k = GetStartFigureIndex(list, sFigure);
											for (int li = 1; k < len; ++k)
											{
												if (list[k].Pattern == flushPattern)
												{
													maxList.Add(list[k]);
													li += 1;
												}
											}

											return;
										}
									}
									else
										break;
								}
							}
							else if (fsCount > 2)
								break;
						}
					}
				}

				// 同花
				for (int i = 0, li = 0; i < len && li < 5; ++i)
				{
					if (list[i].Pattern == flushPattern)
					{
						maxList.Add(list[i]);
						li += 1;
					}
				}
			}
		}

		private static int GetStartFigureIndex(List<Poker> list, Figure figure)
		{
			var len = list.Count;
			for (int i = 0; i < len; ++i)
			{
				if (list[i].Figure == figure)
					return i;
			}

			return 0;
		}
		#endregion

		#region 计算胜率
		private static List<Poker> rePokers = new List<Poker>();// 除手牌公牌的卡牌
		private static List<Group5> reGroup5s = new List<Group5>();// 他人可能牌型
		private static List<Poker> myPokers = new List<Poker>();// 我的卡牌
		private static List<Poker> otherPokers = new List<Poker>();// 他人的卡牌

		public static float CalcWinningRate(GroupData data)
		{
			rePokers.Clear();
			reGroup5s.Clear();
			myPokers.Clear();
			otherPokers.Clear();

			var winRate = 0.0f;
			var count = 0;

			// 标记卡牌
			var pokers = PokerPool.Instance.Pokers;
			var len = pokers.Length;
			int i, j;
			Poker poker;

			for (i = 0; i < len; ++i)
			{
				poker = pokers[i];
				if (data.handPokers.List.Contains(poker))
					poker.type = PokerType.Hand;
				else if (data.publicPokers.List.Contains(poker))
					poker.type = PokerType.Public;
				else
				{
					poker.type = PokerType.None;
					rePokers.Add(poker);
				}
			}

			// 更新5牌组合数据
			var group5s = GroupPool.Instance.Group5s;
			len = group5s.Count;
			Group5 group5;
			List<Poker> list5;
			bool isFind;
			int publicCount, minCount = data.publicPokers.List.Count - 2;
			for (i = 0; i < len; ++i)
			{
				group5 = group5s[i];
				list5 = group5.List;

				isFind = false;
				publicCount = 0;
				for (j = 0; j < 5; ++j)
				{
					if (list5[j].type == PokerType.Hand)
					{
						isFind = true;
						break;
					}
					else if (list5[j].type == PokerType.Public)
					{
						publicCount += 1;
					}
				}

				if (!isFind && publicCount >= minCount)
				{
					group5.isCommon = publicCount >= 5;
					reGroup5s.Add(group5);
				}
			}

			// 遍历剩下的公牌
			myPokers.AddRange(data.handPokers.List);
			myPokers.AddRange(data.publicPokers.List);

			otherPokers.AddRange(data.publicPokers.List);

			len = otherPokers.Count;
			if (len == 3)
			{
				len = rePokers.Count;
				Poker poker1;
				for (i = 0; i < len; ++i)
				{
					poker = rePokers[i];
					myPokers.Add(poker);
					otherPokers.Add(poker);
					for (j = i + 1; j < len; ++j)
					{
						poker1 = rePokers[j];
						myPokers.Add(poker1);
						otherPokers.Add(poker1);

						CalcValue(ref winRate, ref count);

						myPokers.Remove(poker1);
						otherPokers.Remove(poker1);
					}
					myPokers.Remove(poker);
					otherPokers.Remove(poker);
				}
			}
			else if (len == 4)
			{
				len = rePokers.Count;
				for (i = 0; i < len; ++i)
				{
					poker = rePokers[i];
					myPokers.Add(poker);
					otherPokers.Add(poker);

					CalcValue(ref winRate, ref count);

					myPokers.Remove(poker);
					otherPokers.Remove(poker);
				}
			}
			else if (len == 5)
			{
				CalcValue(ref winRate, ref count);
			}
			else
				return 0.0f;

			//Debug.Log($"总胜率：{_winRate} 次数：{_count}");
			return winRate / count;
		}

		private static void CalcValue(ref float winRate, ref int count)
		{
			// 计算自身战力
			var myValue = GetGroup5nMax(myPokers).value;

			// 计算底牌战力
			var publicValue = GetGroup5nMax(otherPokers).value;

			//Debug.Log($"我的牌：{_group5n.GetGroup5()}");

			int len = reGroup5s.Count, j, publicCount, winCount = 0, failCount = 0;
			Group5 group5;
			List<Poker> list5;
			for (int i = 0; i < len; ++i)
			{
				group5 = reGroup5s[i];
				if (group5.value < publicValue)
					continue;// 排除小于公牌战力得组合，不可能组成的牌型

				if (!group5.isCommon)
				{
					list5 = group5.List;
					publicCount = 0;
					for (j = 0; j < 5; ++j)
					{
						if (list5[j].type == PokerType.Public)
							publicCount += 1;
					}

					if (publicCount < 3)
						continue;
				}

				if (myValue >= group5.value)
					winCount += 1;
				else
					failCount += 1;

				//Debug.Log($"{cell5} {_myValue >= cell5.value}");
			}

			//Debug.Log($"胜率：{(float)winCount / (winCount + failCount)}");
			winRate += (float)winCount / (winCount + failCount);
			count += 1;
		}
		#endregion
	}
}