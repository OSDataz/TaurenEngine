/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/2/17 19:56:07
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TaurenEngine.Core;
using TaurenEngine.Editor;
using UnityEditor;
using UnityEngine;

namespace Pro.TexasHoldEM.Editor
{
	public class CalculatorWindow : EditorWindow
	{
		[MenuItem("Project/TexasHoldEM/Calculator")]
		private static void ShowCopyFileWindow()
		{
			var window = EditorWindow.GetWindow<CalculatorWindow>("Calculator");
			window.minSize = new Vector2(500, 100);
			window.Show();
		}

		private EditorEnum _pattern;
		private EditorEnum _figure;

		private List<string> _handPatterns;
		private List<string> _handFigure;
		private List<string> _publicPatterns;
		private List<string> _publicFigure;

		private GUIStyle _resultStyle;

		private string _totalMoney;
		private string _myMoney;
		private int _playerCount;
		private string _outResult;

		private const float ratio = 1.3f;

		void OnEnable()
		{
			_pattern = EditorEnum.Get<Pattern>();
			_figure = EditorEnum.Get<Figure>();

			_handPatterns ??= new List<string>(2) { Pattern.None.ToString(), Pattern.None.ToString() };
			_handFigure ??= new List<string>(2) { Figure.None.ToString(), Figure.None.ToString() };
			_publicPatterns ??= new List<string>(5) {
				Pattern.None.ToString(), Pattern.None.ToString(), Pattern.None.ToString(), Pattern.None.ToString(), Pattern.None.ToString() };
			_publicFigure ??= new List<string>(5) {
				Figure.None.ToString(), Figure.None.ToString(), Figure.None.ToString(), Figure.None.ToString(), Figure.None.ToString() };

			if (_resultStyle == null)
			{
				_resultStyle = new GUIStyle();
				_resultStyle.normal.textColor = Color.green;
			}
		}

		void OnGUI()
		{
			if (GUILayout.Button("Load Config"))
			{
				LoadConfig(true);
			}

			EditorGUILayout.Space(20);

			EditorGUILayout.LabelField("手牌：");
			EditorGUILayout.BeginHorizontal();
			DrawPoker(_handPatterns, _handFigure, 0);
			DrawPoker(_handPatterns, _handFigure, 1);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.LabelField("公牌：");
			EditorGUILayout.BeginHorizontal();
			DrawPoker(_publicPatterns, _publicFigure, 0);
			DrawPoker(_publicPatterns, _publicFigure, 1);
			DrawPoker(_publicPatterns, _publicFigure, 2);
			DrawPoker(_publicPatterns, _publicFigure, 3);
			DrawPoker(_publicPatterns, _publicFigure, 4);
			EditorGUILayout.EndHorizontal();

			_totalMoney = EditorGUILayout.TextField("总金额：", _totalMoney);
			_myMoney = EditorGUILayout.TextField("已支付：", _myMoney);
			_playerCount = EditorGUILayout.IntField("对手玩家：", _playerCount);

			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("重置"))
			{
				ResetShow();
			}

			if (GUILayout.Button("计算胜率"))
			{
				CalcWin();
			}
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.LabelField(_outResult, _resultStyle);
		}

		private void DrawPoker(List<string> patterns, List<string> figures, int i)
		{
			EditorGUILayout.BeginVertical();

			// 花色
			var index = Array.FindIndex(_pattern.nameArray, item => item == patterns[i]);
			var newIndex = EditorGUILayout.IntPopup(index, _pattern.tagArray, _pattern.indexArray);
			if (newIndex != index && newIndex >= -1)
			{
				patterns[i] = _pattern.nameArray[newIndex];
			}

			// 点数
			index = Array.FindIndex(_figure.nameArray, item => item == figures[i]);
			newIndex = EditorGUILayout.IntPopup(index, _figure.tagArray, _figure.indexArray);
			if (newIndex != index && newIndex >= -1)
			{
				figures[i] = _figure.nameArray[newIndex];
			}

			EditorGUILayout.EndVertical();
		}

		private void LoadConfig(bool force)
		{
			if (!force && GroupPool.Instance.HasData)
				return;

			var path = ConfigEditor.GetPokerPower5Path();
			if (File.Exists(path))
			{
				GroupPool.Instance.ParseGroup5Config(FileEx.LoadText(path));

				Debug.Log($"5牌组合配置表解析完成完成。");
			}
			else
				Debug.LogWarning($"未找到配置文件：{path}");

			path = ConfigEditor.GetPokerPower2Path();
			if (File.Exists(path))
			{
				GroupPool.Instance.ParseGroup2Config(FileEx.LoadText(path));

				Debug.Log($"2牌组合配置表解析完成完成。");
			}
			else
				Debug.LogWarning($"未找到配置文件：{path}");
		}

		private void ResetShow()
		{
			ResetList(_handPatterns, Pattern.Club.ToString());
			ResetList(_handFigure, Figure.None.ToString());
			ResetList(_publicPatterns, Pattern.Club.ToString());
			ResetList(_publicFigure, Figure.None.ToString());

			_totalMoney = string.Empty;
			_myMoney = string.Empty;
			_playerCount = 0;
			_outResult = string.Empty;
		}

		private void ResetList(List<string> list, string value)
		{
			var len = list.Count;
			for (int i = 0; i < len; ++i)
			{
				list[i] = value;
			}
		}

		private void CalcWin()
		{
			LoadConfig(false);

			var data = new GroupData();

			var pool = PokerPool.Instance;

			var alls = new List<Poker>();
			Poker poker;
			for (int i = 0; i < 2; ++i)
			{
				var pattern = _handPatterns[i].ToEnum<Pattern>();
				if (pattern == Pattern.None)
				{
					Debug.Log($"第{i + 1}张手牌花色为 None");
					return;
				}

				var figure = _handFigure[i].ToEnum<Figure>();
				if (figure == Figure.None)
				{
					Debug.Log($"第{i + 1}张手牌点数为 None");
					return;
				}

				poker = pool.Get(pattern, figure);
				if (alls.Contains(poker))
				{
					Debug.Log($"第{i + 1}张手牌重复");
					return;
				}

				data.handPokers.List.Add(poker);
				alls.Add(poker);
			}

			for (int i = 0; i < 5; ++i)
			{
				var pattern = _publicPatterns[i].ToEnum<Pattern>();
				if (pattern == Pattern.None)
					break;

				var figure = _publicFigure[i].ToEnum<Figure>();
				if (figure == Figure.None)
					break;

				poker = pool.Get(pattern, figure);
				if (alls.Contains(poker))
				{
					Debug.Log($"第{i + 1}张公牌重复");
					return;
				}

				data.publicPokers.List.Add(poker);
				alls.Add(poker);
			}

			var len = data.publicPokers.List.Count;
			if (len == 3)
				return;// 耗时过长，暂时屏蔽

			var outStr = new StringBuilder();
			outStr.AppendLine($"手牌：{data.handPokers[0]} {data.handPokers[1]}");
			var totalMoney = ParseMoney(_totalMoney);
			var myMoney = ParseMoney(_myMoney);
			float winRate, logicRate;
			if (len < 3)
			{
				winRate = Group2Calc.CalcWinningRate(data.handPokers);
				logicRate = winRate / (_playerCount + 1);
			}
			else
			{
				outStr.Append($"公牌：{data.publicPokers[0]} {data.publicPokers[1]} {data.publicPokers[2]}");
				for (int i = 3; i < len; ++i)
				{
					outStr.Append($" {data.publicPokers[i]}");
				}
				outStr.AppendLine("");
				winRate = Group2nCalc.CalcWinningRate(data);
				logicRate = winRate / (_playerCount + Mathf.Pow(ratio, len - 2));
			}

			var weight = logicRate * (totalMoney - myMoney) - (1 - logicRate) * myMoney;
			outStr.AppendLine($"牌库胜率：{(winRate * 100).ToString("F2")}%   逻辑胜率阈值：{(logicRate * 100).ToString("F2")}%   胜负权重：{weight.ToString("F2")} 【{weight > 0}】");

			_outResult = outStr.ToString();
			Debug.Log(_outResult);
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
	}
}