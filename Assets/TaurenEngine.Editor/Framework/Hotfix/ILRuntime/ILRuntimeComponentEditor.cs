/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/10 10:18:17
 *└────────────────────────┘*/

using TaurenEngine.Core;
using TaurenEngine.Framework;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	[CustomEditor(typeof(ILRuntimeComponent))]
	public class ILRuntimeComponentEditor : UnityEditor.Editor
	{
		private ILRuntimeEditorData _editorData;

		protected void OnEnable()
		{
			_editorData ??= new ILRuntimeEditorData();
			_editorData.LoadData();
		}

		protected void OnDisable()
		{
			_editorData.SaveAssets();
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			// 通过自动分析热更DLL生成CLR绑定
			EditorGUILayout.BeginVertical("box");
			_editorData.GenerateCLRSavePath.Draw("CLR绑定代码保存路径：");
			if (GUILayout.Button("通过自动分析热更DLL生成CLR绑定"))
			{
				ILRuntimeHelper.GenerateCLRBindingByAnalysis(_editorData.Data);

				Debug.Log("生成CLR绑定完成");
			}
			EditorGUILayout.EndVertical();

			// 生成跨域继承适配器
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("生成跨域继承适配器");

			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("生成所有组适配器"))
			{
				ILRuntimeHelper.GenerateCrossbindAdapter(_editorData.AdaptorGroupList);
			}
			if (GUILayout.Button("添加新的适配组"))
			{
				_editorData.AdaptorGroupList.Add();
			}
			EditorGUILayout.EndHorizontal();

			_editorData.AdaptorGroupList.List.ForFunc(item => item.Draw());
		}
	}
}