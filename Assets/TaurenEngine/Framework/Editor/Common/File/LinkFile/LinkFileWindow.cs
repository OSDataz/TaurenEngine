﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2022/1/15 11:42:33
 *└────────────────────────┘*/

using TaurenEngine;
using UnityEditor;
using UnityEngine;

namespace TaurenEditor
{
	public class LinkFileWindow : EditorWindow
	{
		[MenuItem("TaurenEngine/File/链接文件工具")]
		private static void ShowWindow()
		{
			var window = EditorWindow.GetWindow<LinkFileWindow>("Link File");
			window.minSize = new Vector2(500, 100);
			window.Show();
		}

		private LinkFileEditorData _editorData;

		private Vector2 _scrollPos;

		void OnEnable()
		{
			_editorData ??= new LinkFileEditorData();
			_editorData.LoadData();
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