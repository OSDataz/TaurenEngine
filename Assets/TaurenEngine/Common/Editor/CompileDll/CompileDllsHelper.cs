/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/29 12:17:37
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEngine;

namespace TaurenEditor.Common
{
	/// <summary>
	/// TODO 待确定具体使用方式后修改
	/// </summary>
	public static class CompileDllsHelper
	{
		[MenuItem("TaurenTools/Common/CompileDll/ActiveBuildTarget")]
		private static void CompileDllActiveBuildTarget()
		{
			CompileDll(EditorUserBuildSettings.activeBuildTarget);
		}

		[MenuItem("TaurenTools/Common/CompileDll/Win64")]
		private static void CompileDllWin64()
		{
			CompileDll(BuildTarget.StandaloneWindows64);
		}

		[MenuItem("TaurenTools/Common/CompileDll/Android")]
		private static void CompileDllAndroid()
		{
			CompileDll(BuildTarget.Android);
		}

		[MenuItem("TaurenTools/Common/CompileDll/IOS")]
		private static void CompileDllIOS()
		{
			CompileDll(BuildTarget.iOS);
		}

		private static void CompileDll(BuildTarget buildTarget)
		{
			var path = $"{EditorHelper.ProjectConfigPath}/Temp~/{buildTarget}";
			Filex.CreateDirectoryByDirectoryPath(path);

			var scriptCompilationSettings = new ScriptCompilationSettings();
			scriptCompilationSettings.group = BuildPipeline.GetBuildTargetGroup(buildTarget);
			scriptCompilationSettings.target = buildTarget;

			PlayerBuildInterface.CompilePlayerScripts(scriptCompilationSettings, path);

			Debug.Log($"{buildTarget}平台生成Dll成功，文件目录：{path}");
		}
	}
}