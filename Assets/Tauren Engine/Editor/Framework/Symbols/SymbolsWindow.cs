/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/12/11 21:07:33
 *└────────────────────────┘*/

using Tauren.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Tauren.Framework.Editor
{
	public class SymbolsWindow : DataWindow<SymbolsEditorData>
	{
		[MenuItem("TaurenTools/Framework/宏标签设置")]
		private static void ShowWindow()
		{
			var window = EditorWindow.GetWindow<SymbolsWindow>("宏标签设置");
			window.minSize = new Vector2(800, 100);
			window.Show();
		}

		private Vector2 _scrollPos;
		private bool _isSave;

		protected override SymbolsEditorData CreateEditorData()
		{
			var data = new SymbolsEditorData();
			data.LoadData();
			return data;
		}

		protected override void OnEnable()
		{
			base.OnEnable();

			_scrollPos = Vector2.zero;
		}

		protected override void OnGUI()
		{
			_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

			editorData.SymbolsList.List.ForRemoveFunc(item => item.Draw());

			EditorGUILayout.EndScrollView();

			EditorGUILayout.BeginHorizontal();

			if (GUILayout.Button("Add"))
			{
				editorData.SymbolsList.Add();
			}

			_isSave = false;
			if (GUILayout.Button("Save"))
			{
				_isSave = true;
			}

			EditorGUILayout.EndHorizontal();

			base.OnGUI();

			if (_isSave)
			{
				ProjectSettings.Player.SetSelectedBuildScriptingDefineSymbols(editorData.GetSymbols());
				Debug.Log("设置完成");
			}
		}
	}
}