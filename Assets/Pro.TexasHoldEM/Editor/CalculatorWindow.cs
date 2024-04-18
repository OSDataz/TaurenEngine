/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/17 17:15:22
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Pro.TexasHoldEM.Editor
{
	public class CalculatorWindow : EditorWindow
	{
		[MenuItem("Project/TexasHoldEM/Calculator")]
		private static void ShowCalculatorWindow()
		{
			var window = EditorWindow.GetWindow<CalculatorWindow>("Calculator");
			window.minSize = new Vector2(500, 100);
			window.Show();
		}

		

		private void OnEnable()
		{
			InitPokerItem();
			InitResultStyle();
		}

		private void OnGUI()
		{
			if (GUILayout.Button("Load Config"))
			{
				ConfigHelper.LoadConfig(true);
			}

			EditorGUILayout.Space(20);

			// 扑克列表
			DrawPokerList(Pattern.Spade);
			DrawPokerList(Pattern.Heart);
			DrawPokerList(Pattern.Club);
			DrawPokerList(Pattern.Diamond);

			// 手牌
			DrawHandItems();

			// 公牌
			DrawPublicItems();

			// 额外数据
			DrawExData();

			// 按钮
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("重置选牌"))
			{
				ResetPokerItem();
				ClearHandItems();
				ClearPublicItems();

				_selectHand = true;
				_selectPublic = false;
			}

			if (GUILayout.Button("重置"))
			{
				ResetPokerItem();
				ClearHandItems();
				ClearPublicItems();
				ClearExData();

				_selectHand = true;
				_selectPublic = false;
			}

			if (GUILayout.Button("计算胜率"))
			{
				Calc();
			}
			EditorGUILayout.EndHorizontal();

			// 结算显示
			EditorGUILayout.LabelField(_outResult, _resultStyle);
		}

		#region 扑克数据
		private class PokerItem
		{
			public Poker poker;
			public Texture texture;
			public bool selected = false;
		}

		private Dictionary<Pattern, List<PokerItem>> items = new Dictionary<Pattern, List<PokerItem>>();

		private void InitPokerItem()
		{
			if (items.Count != 0)
				return;

			InitPokerItemList(Pattern.Spade, "spade");
			InitPokerItemList(Pattern.Heart, "heart");
			InitPokerItemList(Pattern.Club, "club");
			InitPokerItemList(Pattern.Diamond, "diamond");
		}

		private void InitPokerItemList(Pattern pattern, string iconName)
		{
			items.Add(pattern, new List<PokerItem>()
			{
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._2), texture = Resources.Load<Texture>($"Poker/{iconName}_2") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._3), texture = Resources.Load<Texture>($"Poker/{iconName}_3") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._4), texture = Resources.Load<Texture>($"Poker/{iconName}_4") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._5), texture = Resources.Load<Texture>($"Poker/{iconName}_5") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._6), texture = Resources.Load<Texture>($"Poker/{iconName}_6") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._7), texture = Resources.Load<Texture>($"Poker/{iconName}_7") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._8), texture = Resources.Load<Texture>($"Poker/{iconName}_8") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._9), texture = Resources.Load<Texture>($"Poker/{iconName}_9") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._10), texture = Resources.Load<Texture>($"Poker/{iconName}_10") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._J), texture = Resources.Load<Texture>($"Poker/{iconName}_J") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._Q), texture = Resources.Load<Texture>($"Poker/{iconName}_Q") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._K), texture = Resources.Load<Texture>($"Poker/{iconName}_K") },
				new PokerItem() { poker = PokerPool.Instance.Get(pattern, Figure._A), texture = Resources.Load<Texture>($"Poker/{iconName}_A") },
			});
		}

		private void ResetPokerItem()
		{
			foreach (var kv in items)
			{
				foreach (var item in kv.Value)
				{
					item.selected = false;
				}
			}
		}
		#endregion

		#region 扑克排列
		private void DrawPokerList(Pattern pattern)
		{
			var list = items[pattern];

			EditorGUILayout.BeginHorizontal();

			foreach (var item in list)
			{
				if (item.selected)
				{
					EditorGUILayout.LabelField("", GUILayout.Width(35));
				}
				else
				{
					if (GUILayout.Button(item.texture, GUILayout.Width(35), GUILayout.Height(45)))
					{
						if (AddItem(item))
							item.selected = true;
					}
				}
			}

			EditorGUILayout.EndHorizontal();
		}

		private bool AddItem(PokerItem pokerItem)
		{
			if (_selectHand)
				return AddHandItem(pokerItem);

			if (_selectPublic)
				return AddPublicItem(pokerItem);

			return false;
		}
		#endregion

		#region 手牌
		private const int handLen = 2;
		private List<PokerItem> handItems = new List<PokerItem>(handLen);
		private bool _selectHand = true;

		private bool AddHandItem(PokerItem pokerItem)
		{
			if (handItems.Count >= handLen)
				return false;

			handItems.Add(pokerItem);
			return true;
		}

		private void DrawHandItems()
		{
			EditorGUILayout.BeginHorizontal();
			_selectHand = EditorGUILayout.Toggle(_selectHand, GUILayout.Width(15));
			if (_selectHand) _selectPublic = false;
			EditorGUILayout.LabelField("手牌：");
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			foreach (var item in handItems)
			{
				if (item == null)
					break;

				GUILayout.Label(item.texture, GUILayout.Width(35), GUILayout.Height(45));
			}
			EditorGUILayout.EndHorizontal();
		}

		private void ClearHandItems()
		{
			handItems.Clear();
		}
		#endregion

		#region 公牌
		private const int publicLen = 5;
		private List<PokerItem> publicItems = new List<PokerItem>(publicLen);
		private bool _selectPublic;

		private bool AddPublicItem(PokerItem pokerItem)
		{
			if (publicItems.Count >= publicLen)
				return false;

			publicItems.Add(pokerItem);
			return true;
		}

		private void DrawPublicItems()
		{
			EditorGUILayout.BeginHorizontal();
			_selectPublic = EditorGUILayout.Toggle(_selectPublic, GUILayout.Width(15));
			if (_selectPublic) _selectHand = false;
			EditorGUILayout.LabelField("公牌：");
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
			foreach (var item in publicItems)
			{
				if (item == null)
					break;

				GUILayout.Label(item.texture, GUILayout.Width(35), GUILayout.Height(45));
			}
			EditorGUILayout.EndHorizontal();
		}

		private void ClearPublicItems()
		{
			publicItems.Clear();
		}
		#endregion

		#region 额外数据
		private string _totalMoney;
		private string _myMoney;
		private int _playerCount;

		private void DrawExData()
		{
			_totalMoney = EditorGUILayout.TextField("总金额：", _totalMoney);
			_myMoney = EditorGUILayout.TextField("已支付：", _myMoney);
			_playerCount = EditorGUILayout.IntField("对手玩家：", _playerCount);
		}

		private void ClearExData()
		{
			_totalMoney = string.Empty;
			_myMoney = string.Empty;
			_playerCount = 0;
			_outResult = string.Empty;
		}
		#endregion

		#region 结果显示
		private GUIStyle _resultStyle;

		private string _outResult;

		private void InitResultStyle()
		{
			if (_resultStyle == null)
			{
				_resultStyle = new GUIStyle();
				_resultStyle.normal.textColor = Color.green;
			}
		}
		#endregion

		#region 开始计算
		private void Calc()
		{
			if (handItems.Count < 2)
			{
				Debug.Log("手牌未设置完");
				return;
			}

			CalculatorHelper.CalcWin(
				handItems.ConvertAll(item => item.poker), 
				publicItems.ConvertAll(item => item.poker),
				ParseMoney(_totalMoney), ParseMoney(_myMoney), _playerCount, ref _outResult);
		}

		private int ParseMoney(string value)
		{
			if (string.IsNullOrEmpty(value))
				return 0;

			int money = 0;
			var list = value.Split(' ');
			var len = list.Length;
			for (int i = 0; i < len; ++i)
			{
				if (int.TryParse(list[i], out var result))
					money += result;
			}

			return money;
		}
		#endregion
	}
}