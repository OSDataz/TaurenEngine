/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/9 12:15:29
 *└────────────────────────┘*/
#if UNITY_2020_1_OR_NEWER

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.UnityLinker;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TaurenEngine.Editor.Framework
{
	/// <summary>
	/// 打包时剔除热更代码
	/// </summary>
	public class BuildProcessor_2020_1_OR_NEWER
		: IPreprocessBuildWithReport,
#if UNITY_ANDROID
        IPostGenerateGradleAndroidProject,
#else
		IPostprocessBuildWithReport,
#endif
		IProcessSceneWithReport, IFilterBuildAssemblies, IPostBuildPlayerScriptDLLs, IUnityLinkerProcessor
	{
		public int callbackOrder => 0;

		public void OnPreprocessBuild(BuildReport report)
		{
		}

		public void OnProcessScene(Scene scene, BuildReport report)
		{
		}

		public string[] OnFilterAssemblies(BuildOptions buildOptions, string[] assemblies)
		{
			// 将热更dll从打包列表中移除
			var dlls = HotfixEditorData.Instance.Data.dlls;
			return assemblies.Where(ass => dlls.All(dll => !ass.EndsWith(dll.name, StringComparison.OrdinalIgnoreCase))).ToArray();
		}

		public void OnPostBuildPlayerScriptDLLs(BuildReport report)
		{
		}

#if UNITY_ANDROID
        public void OnPostGenerateGradleAndroidProject(string path)
        {
            // 由于 Android 平台在 OnPostprocessBuild 调用时已经生成完 apk 文件，因此需要提前调用
            AddBackHotFixAssembliesToJson(null, path);
        }
#else
		public void OnPostprocessBuild(BuildReport report)
		{
			AddBackHotFixAssembliesToJson(report, report.summary.outputPath);
		}
#endif

		[Serializable]
		public class ScriptingAssemblies
		{
			public List<string> names;
			public List<int> types;
		}

		private void AddBackHotFixAssembliesToJson(BuildReport report, string path)
		{
			/*
             * ScriptingAssemblies.json 文件中记录了所有的dll名称，此列表在游戏启动时自动加载，
             * 不在此列表中的dll在资源反序列化时无法被找到其类型
             * 因此 OnFilterAssemblies 中移除的条目需要再加回来
             */
			string[] jsonFiles = Directory.GetFiles(Path.GetDirectoryName(path), "ScriptingAssemblies.json", SearchOption.AllDirectories);

			if (jsonFiles.Length == 0)
			{
				Debug.LogError("can not find file ScriptingAssemblies.json");
				return;
			}

			// 需要在Prefab上挂脚本的热更dll名称列表，不需要挂到Prefab上的脚本可以不放在这里
			// 但放在这里的dll即使勾选了 AnyPlatform 也会在打包过程中被排除
			var dlls = HotfixEditorData.Instance.Data.dlls;

			foreach (string file in jsonFiles)
			{
				string content = File.ReadAllText(file);
				ScriptingAssemblies scriptingAssemblies = JsonUtility.FromJson<ScriptingAssemblies>(content);
				foreach (var dll in dlls)
				{
					if (!dll.useByMono)
						continue;

					if (!scriptingAssemblies.names.Contains(dll.name))
					{
						scriptingAssemblies.names.Add(dll.name);
						scriptingAssemblies.types.Add(16); // user dll type
					}
				}
				content = JsonUtility.ToJson(scriptingAssemblies);

				File.WriteAllText(file, content);
			}
		}

		public string GenerateAdditionalLinkXmlFile(BuildReport report, UnityLinkerBuildPipelineData data)
		{
			return String.Empty;
		}

		[UnityEditor.Callbacks.DidReloadScripts]
		private static void OnDidReloadScripts()
		{
			// Unity更新（修改代码后），自动将热更DLL拷贝到指定目录
			if (!HotfixEditorData.Instance.Data.isDidReloadScripts)
				return;

			HotfixTool.UpdateHotfixDll();
		}

#if UNITY_IOS
		//hook UnityEditor.BuildCompletionEventsHandler.ReportPostBuildCompletionInfo() ? 因为没有 mac 打包平台因此不清楚
#endif
	}
}
#endif