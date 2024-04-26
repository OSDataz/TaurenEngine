/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/11 10:10:10
 *└────────────────────────┘*/

using Tauren.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Tauren.Framework.Editor
{
	public class FileFormatWindow : EditorWindow
	{
		[MenuItem("TaurenTools/Tools/File/格式化文件")]
		private static void ShowWindow()
		{
			var window = EditorWindow.GetWindow<FileFormatWindow>("File Format");
			window.minSize = new Vector2(500, 100);
			window.Show();
		}

		private string _path;
		private EditorEnum<LineEndingsType> _lineEndings;
		private EditorEnum<EncodingType> _encoding;

		protected void OnEnable()
		{
			_lineEndings = new EditorEnum<LineEndingsType>();
			_encoding = new EditorEnum<EncodingType>();
		}

		protected void OnGUI()
		{
			EditorGUILayout.LabelField("文件/文件夹地址：");
			_path = EditorGUILayout.TextField(_path);

			EditorGUILayout.Space(10);

			_lineEndings.Draw("换行类型：");
			_encoding.Draw("编码类型：");

			if (GUILayout.Button("格式化", GUILayout.Height(30)))
			{
				FileFormatHelper.FormatPath(_path, _lineEndings.Value, _encoding.Value);
			}
		}
	}
}