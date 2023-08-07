/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/10/8 20:41:37
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;
using TaurenEngine.Editor;

namespace TaurenEditor.Tools
{
	public class CopyFileWindow : EditorWindow
	{
		[MenuItem("TaurenTools/Tools/File/复制文件工具")]
		private static void ShowWindow()
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
			_editorData.LoadData();
		}

		void OnDisable()
		{
			_editorData.SaveAssets();
		}

		void OnGUI()
		{
			_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

			_editorData.Groups.List.ForRemoveFunc(item => item.Draw());

			EditorGUILayout.EndScrollView();

			if (GUILayout.Button("New"))
			{
				_editorData.Groups.Add();
			}
		}
	}
}