/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/15 21:47:15
 *└────────────────────────┘*/

using System.IO;
using TaurenEngine.Editor;
using UnityEditor;
using UnityEngine;

namespace TaurenEditor.ModConfig
{
	/// <summary>
	/// Excel配表工具
	/// </summary>
	public class ExcelConfigWindow : DataWindow<ConfigEditorData>
	{
		[MenuItem("TaurenTools/Framework/Excel配置表工具")]
		private static void ShowWindow()
		{
			var window = EditorWindow.GetWindow<ExcelConfigWindow>("配置表工具");
			window.minSize = new Vector2(500, 100);
			window.Show();
		}

		private bool _isShowDetail = false;
		private bool _isShowList = true;
		private Vector2 _scrollPos;

		protected override ConfigEditorData CreateEditorData()
		{
			var data = new ConfigEditorData();
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
			_isShowDetail = EditorGUILayout.Foldout(_isShowDetail, "配置表生成设置");

			if (_isShowDetail)
			{
				editorData.ExcelPath.Draw("Excel文件路径：");
				editorData.ScriptNamespace.Draw("生成代码命名空间：");
				editorData.ScriptSavePath.Draw("生成代码保存路径：");
				editorData.ConfigSavePath.Draw("生成配置保存路径：");
				editorData.FormatType.Draw("格式类型：");
				editorData.EncodingType.Draw("编码类型：");
			}

			if (Directory.Exists(editorData.ExcelPath.Value))
			{
				var excelList = Directory.GetFiles(editorData.ExcelPath.Value, "*.xlsx");
				_isShowList = EditorGUILayout.Foldout(_isShowList, "Excel文档列表：");

				if (_isShowList)
				{
					_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

					foreach (var path in excelList)
					{
						EditorGUILayout.LabelField(path);
					}

					EditorGUILayout.EndScrollView();
				}

				EditorGUILayout.BeginHorizontal();
				if (GUILayout.Button("生成代码和配置"))
				{
					ConfigHelper.GenerateConfig(editorData.Data, excelList);

					AssetDatabase.Refresh();

					Debug.Log("配置表生成代码和配置文件成功");
				}
				EditorGUILayout.EndHorizontal();
			}

			base.OnGUI();
		}
	}
}