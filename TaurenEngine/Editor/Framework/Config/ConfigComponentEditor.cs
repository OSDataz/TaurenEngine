/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/16 20:20:16
 *└────────────────────────┘*/

using System.IO;
using TaurenEngine.Framework;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	[CustomEditor(typeof(ConfigComponent))]
	public class ConfigComponentEditor : UnityEditor.Editor
	{
		private ConfigEditorData _configData;

		private bool _isShowDetail = false;
		private bool _isShowList = false;
		private Vector2 _scrollPos;

		protected void OnEnable()
		{
			_configData ??= new ConfigEditorData();
			_configData.LoadData();

			_scrollPos = Vector2.zero;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			_isShowDetail = EditorGUILayout.Foldout(_isShowDetail, "配置表生成设置");

			if (_isShowDetail)
			{
				_configData.ExcelPath.Draw("Excel文件路径：");
				_configData.ScriptNamespace.Draw("生成配置代码命名空间：");
				_configData.ScriptSavePath.Draw("生成配置代码保存路径：");
				_configData.ConfigSavePath.Draw("生成配置文件保存路径：");
				_configData.FormatType.Draw("格式类型：");
				_configData.EncodingType.Draw("编码类型：");
			}

			if (Directory.Exists(_configData.ExcelPath.Value))
			{
				var excelList = Directory.GetFiles(_configData.ExcelPath.Value, "*.xlsx");
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
				if (GUILayout.Button("仅生成配置"))
				{


					AssetDatabase.Refresh();
				}

				if (GUILayout.Button("生成代码和配置"))
				{


					AssetDatabase.Refresh();
				}
				EditorGUILayout.EndHorizontal();
			}
		}
	}
}