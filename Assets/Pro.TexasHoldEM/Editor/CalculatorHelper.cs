/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/17 17:04:37
 *└────────────────────────┘*/

using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Pro.TexasHoldEM.Editor
{
	public static class CalculatorHelper
	{
		public static void CalcWin(List<Poker> handPokers, List<Poker> publicPokers, 
			int totalMoney, int myMoney, int playerCount, ref string outResult)
		{
			ConfigHelper.LoadConfig(false);

			var data = new GroupData();

			var alls = new List<Poker>();
			Poker poker;
			for (int i = 0; i < 2; ++i)
			{
				poker = handPokers[i];
				if (poker == null)
				{
					Debug.Log($"第{i + 1}张手牌未设置");
					return;
				}

				if (alls.Contains(poker))
				{
					Debug.Log($"第{i + 1}张手牌重复");
					return;
				}

				data.handPokers.List.Add(poker);
				alls.Add(poker);
			}

			var len = Mathf.Min(publicPokers.Count, 5);
			for (int i = 0; i < len; ++i)
			{
				poker = publicPokers[i];
				if (poker == null)
					break;

				if (alls.Contains(poker))
				{
					Debug.Log($"第{i + 1}张公牌重复");
					return;
				}

				data.publicPokers.List.Add(poker);
				alls.Add(poker);
			}

			len = data.publicPokers.List.Count;
			if (len == 3)
				return;// 耗时过长，暂时屏蔽

			var outStr = new StringBuilder();
			//outStr.AppendLine($"手牌：{data.handPokers[0]} {data.handPokers[1]}");
			float winRate, logicRate;
			if (len < 3)
			{
				winRate = Group2Calc.CalcWinningRate(data.handPokers);
				logicRate = winRate / (playerCount + 1);
			}
			else
			{
				//outStr.Append($"公牌：{data.publicPokers[0]} {data.publicPokers[1]} {data.publicPokers[2]}");
				//for (int i = 3; i < len; ++i)
				//{
				//	outStr.Append($" {data.publicPokers[i]}");
				//}
				//outStr.AppendLine("");
				winRate = Group2nCalc.CalcWinningRate(data);
				logicRate = winRate / (playerCount + Mathf.Pow(1.3f, len - 2));// 概率比例
			}

			var weight = logicRate * (totalMoney - myMoney) - (1 - logicRate) * myMoney;
			outStr.AppendLine($"牌库胜率：{(winRate * 100).ToString("F2")}%   逻辑胜率阈值：{(logicRate * 100).ToString("F2")}%   胜负权重：{weight.ToString("F2")} 【{weight > 0}】");

			outResult = outStr.ToString();
			Debug.Log(outResult);
		}
	}
}