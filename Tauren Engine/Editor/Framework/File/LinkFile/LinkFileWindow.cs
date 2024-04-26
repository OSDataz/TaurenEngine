/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2022/1/15 11:42:33
 *└────────────────────────┘*/

using Tauren.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Tauren.Framework.Editor
{
	public class LinkFileWindow : DataWindow<LinkFileEditorData>
	{
		[MenuItem("TaurenTools/Tools/File/链接文件工具")]
		private static void ShowWindow()
		{
			var window = EditorWindow.GetWindow<LinkFileWindow>("Link File");
			window.minSize = new Vector2(500, 100);
			window.Show();
		}

		private Vector2 _scrollPos;

		protected override LinkFileEditorData CreateEditorData()
		{
			var data = new LinkFileEditorData();
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