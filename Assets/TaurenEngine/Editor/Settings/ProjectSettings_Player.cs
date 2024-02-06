/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/12/11 20:42:45
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEditor;

namespace TaurenEditor
{
	public class ProjectSettings_Player
	{
		#region Script Define Symbols
		/// <summary>
		/// 获取当前选择构建的宏标签列表
		/// </summary>
		/// <returns></returns>
		public string[] GetSelectedBuildScriptingDefineSymbols()
		{
			PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, out var symbos);
			return symbos;
		}

		/// <summary>
		/// 设置当前选择构建的宏标签列表
		/// </summary>
		/// <param name="symbos"></param>
		public void SetSelectedBuildScriptingDefineSymbols(List<string> symbos)
		{
			PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, string.Join(";", symbos.ToArray()));
		}
		#endregion
	}
}