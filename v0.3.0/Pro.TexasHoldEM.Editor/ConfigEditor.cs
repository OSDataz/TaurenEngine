/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/2/10 19:40:36
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEditor;
using UnityEngine;

namespace Pro.TexasHoldEM.Editor
{
	public static class ConfigEditor
	{
		public static string GetPokerPower5Path()
			=> $"{Application.dataPath}/Pro.TexasHoldEM/Config/PokerPower5.txt";

		public static string GetPokerPower2Path()
			=> $"{Application.dataPath}/Pro.TexasHoldEM/Config/PokerPower2.txt";

		[MenuItem("Project/TexasHoldEM/Build Config")]
		private static void BuildConfig()
		{
			if (EditorUtility.DisplayDialog("确认", "是否重新生成组合配置", "重新生成"))
			{
				GroupPool.Instance.Init(true);

				FileEx.SaveText(GetPokerPower5Path(), GroupPool.Instance.GenerateGroup5Config());

				Debug.Log($"5牌组合配置表生成完成，一共匹配组合：{GroupPool.Instance.Group5s.Count}");

				FileEx.SaveText(GetPokerPower2Path(), GroupPool.Instance.GenerateGroup2Config()); ;

				Debug.Log($"2牌组合配置表生成完成，一共匹配组合：{GroupPool.Instance.Group2s.Count}");
			}
		}
	}
}