/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.4.0
 *│　Time    ：2022/2/10 19:40:36
 *└────────────────────────┘*/

using System.IO;
using Tauren.Core.Runtime;
using UnityEditor;
using UnityEngine;

namespace Pro.TexasHoldEM.Editor
{
	public static class ConfigHelper
	{
		private static string GetPokerPower5Path()
			=> $"{Application.dataPath}/Pro.TexasHoldEM/Config/PokerPower5.txt";

		private static string GetPokerPower2Path()
			=> $"{Application.dataPath}/Pro.TexasHoldEM/Config/PokerPower2.txt";

		[MenuItem("Project/TexasHoldEM/Build Config")]
		private static void BuildConfig()
		{
			if (EditorUtility.DisplayDialog("确认", "是否重新生成组合配置", "重新生成"))
			{
				GroupPool.Instance.Init(true);

				FileUtils.SaveText(GetPokerPower5Path(), GroupPool.Instance.GenerateGroup5Config());

				Debug.Log($"5牌组合配置表生成完成，一共匹配组合：{GroupPool.Instance.Group5s.Count}");

				FileUtils.SaveText(GetPokerPower2Path(), GroupPool.Instance.GenerateGroup2Config()); ;

				Debug.Log($"2牌组合配置表生成完成，一共匹配组合：{GroupPool.Instance.Group2s.Count}");
			}
		}

		public static void LoadConfig(bool force)
		{
			if (!force && GroupPool.Instance.HasData)
				return;

			var path = GetPokerPower5Path();
			if (File.Exists(path))
			{
				GroupPool.Instance.ParseGroup5Config(FileUtils.LoadText(path));

				Debug.Log($"5牌组合配置表解析完成完成。");
			}
			else
				Debug.LogWarning($"未找到配置文件：{path}");

			path = GetPokerPower2Path();
			if (File.Exists(path))
			{
				GroupPool.Instance.ParseGroup2Config(FileUtils.LoadText(path));

				Debug.Log($"2牌组合配置表解析完成完成。");
			}
			else
				Debug.LogWarning($"未找到配置文件：{path}");
		}
	}
}