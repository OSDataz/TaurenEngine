/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/10/8 20:41:37
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;
using TaurenEngine.Core;
using TaurenEngine.Editor.Common;

namespace TaurenEngine.Editor.CopyFile
{
	public class CopyFileWindow : EditorWindow
	{
		[MenuItem(MenuName.CopyFile)]
		private static void ShowCopyFileWindow()
		{
			var window = EditorWindow.GetWindow<CopyFileWindow>("Copy File");
			window.minSize = new Vector2(500, 100);
			window.Show();
		}

		private CopyFileEditorData _editorData;

		private Vector2 _scrollPos;

		void OnEnable()
		{
			_editorData ??= new CopyFileEditorData();
			_editorData.LoadData("Assets/EditorConfig/CopyFileConfig.asset");
		}

		void OnDisable()
		{
			_editorData.SaveAssets();
		}

		void OnGUI()
		{
			_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

			_editorData.Groups.List.ForFunc(item => item.Draw());

			EditorGUILayout.EndScrollView();

			if (GUILayout.Button("New"))
			{
				_editorData.Groups.Add();
			}
		}
	}
}