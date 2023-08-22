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
	public class CopyFileWindow : DataWindow<CopyFileEditorData>
	{
		[MenuItem("TaurenTools/Tools/File/复制文件工具")]
		private static void ShowWindow()
		{
			var window = EditorWindow.GetWindow<CopyFileWindow>("Copy File");
			window.minSize = new Vector2(500, 100);
			window.Show();
		}

		private Vector2 _scrollPos;

		protected override CopyFileEditorData CreateEditorData()
		{
			var data = new CopyFileEditorData();
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

			editorData.Groups.List.ForRemoveFunc(item => item.Draw());

			EditorGUILayout.EndScrollView();

			if (GUILayout.Button("New"))
			{
				editorData.Groups.Add();
			}

			base.OnGUI();
		}
	}
}