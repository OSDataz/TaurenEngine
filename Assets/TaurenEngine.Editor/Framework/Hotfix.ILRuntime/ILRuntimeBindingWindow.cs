/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/6 14:56:00
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public class ILRuntimeBindingWindow : EditorWindow
	{
		[MenuItem("TaurenEngine/ILRuntime/热更绑定设置")]
		private static void ShowWindow()
		{
			var window = EditorWindow.GetWindow<ILRuntimeBindingWindow>("热更绑定设置");
			window.minSize = new Vector2(500, 100);
			window.Show();
		}

		private ILRuntimeEditorData _editorData;

		private Vector2 _scrollPos;

		void OnEnable()
		{
			_editorData ??= new ILRuntimeEditorData();
			_editorData.LoadData();
		}

		void OnDisable()
		{
			_editorData.SaveAssets();
		}

		void OnGUI()
		{
			// 通过自动分析热更DLL生成CLR绑定
			EditorGUILayout.BeginVertical("box");
			_editorData.DLLPath.Draw("DLL路径：");
			_editorData.GenerateCodeSavePath.Draw("CLR绑定代码保存路径：");
			if (GUILayout.Button("通过自动分析热更DLL生成CLR绑定"))
			{
				ILRuntimeHelper.GenerateCLRBindingByAnalysis(_editorData.Data);

				Debug.Log("生成CLR绑定完成");
			}
			EditorGUILayout.EndVertical();

			EditorGUILayout.Space();

			// 生成跨域继承适配器
			EditorGUILayout.BeginVertical("box");

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("生成跨域继承适配器");
			if (GUILayout.Button("New", GUILayout.Width(277)))
			{
				_editorData.AdaptorGroupList.Add();
			}
			EditorGUILayout.EndHorizontal();

			_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
			_editorData.AdaptorGroupList.List.ForFunc(item => item.Draw());
			EditorGUILayout.EndScrollView();

			EditorGUILayout.EndVertical();
		}
	}
}