/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 12:14:39
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TaurenEngine.Core;
using TaurenEngine.Editor;
using UnityEditor;
using UnityEngine;

namespace Tools.JobAnalysis
{
	public class JobAnalysisWindow : DataWindow<JobAnalysisEditorData>
	{
		[MenuItem("Tools/职位分析")]
		private static void ShowWindow()
		{
			var window = EditorWindow.GetWindow<JobAnalysisWindow>("职位分析");
			window.minSize = new Vector2(500, 100);
			window.Show();
		}

		protected override JobAnalysisEditorData CreateEditorData()
		{
			var data = new JobAnalysisEditorData();
			data.LoadData();
			return data;
		}

		private JobAnalysisInputUI _inputUI;
		private JobAnalysisLookJobUI _lookJobUI;
		private JobAnalysisLookCompanyUI _lookCompanyUI;
		private JobAnalysisFilterUI _filterUI;

		private DataManager _dataMgr;

		private string _logStr;
		private GUIStyle _logStyle = new GUIStyle();

		protected override void OnEnable()
		{
			base.OnEnable();

			_dataMgr = DataManager.Instance;

			_inputUI ??= new JobAnalysisInputUI(this);
			_inputUI.OnEnable();

			_lookJobUI ??= new JobAnalysisLookJobUI(this);
			_lookJobUI.OnEnable();

			_lookCompanyUI ??= new JobAnalysisLookCompanyUI(this);
			_lookCompanyUI.OnEnable();

			_filterUI ??= new JobAnalysisFilterUI(this);
			_filterUI.OnEnable();
		}

		protected override void OnGUI()
		{
			// 文件信息
			editorData.CompanyFilePath.Draw("公司文件地址：");
			editorData.JobFilePath.Draw("职位文件地址：");

			// 公共信息
			EditorGUILayout.BeginHorizontal();


			if (GUILayout.Button("读取本地文件", GUILayout.Width(100)))
			{
				if (EditorUtility.DisplayDialog("读取", "是否确认读取本地文件，检查是否有未保存内容？", "保存", "取消"))
				{
					CheckReadData(true);
				}
			}

			if (GUILayout.Button("保存文件", GUILayout.Width(100)))
			{
				if (EditorUtility.DisplayDialog("保存文件", "是否确认保存文件？", "保存", "取消"))
				{
					SaveData();
				}
			}

			EditorGUILayout.LabelField($"职位总数：{_dataMgr.GetJobCount()}个");
			EditorGUILayout.LabelField($"公司总数：{_dataMgr.GetCompanyCount()}个");
			EditorGUILayout.EndHorizontal();

			// 日志
			EditorGUILayout.LabelField(_logStr, _logStyle);

			// 分页信息
			DrawSwitchGUI();

			base.OnGUI();
		}

		#region 切换
		private enum SwitchType
		{
			Input,
			LookJob,
			LookCompany,
			Filter
		}

		private SwitchType _switchType;

		private void DrawSwitchGUI()
		{
			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("输入信息", GUILayout.Height(30))) _switchType = SwitchType.Input;
			if (GUILayout.Button("查询职位", GUILayout.Height(30))) _switchType = SwitchType.LookJob;
			if (GUILayout.Button("查询公司", GUILayout.Height(30))) _switchType = SwitchType.LookCompany;
			if (GUILayout.Button("筛选数据", GUILayout.Height(30))) _switchType = SwitchType.Filter;
			EditorGUILayout.EndHorizontal();

			switch (_switchType)
			{
				case SwitchType.Input:
					_inputUI.OnGUI();
					break;

				case SwitchType.LookJob:
					_lookJobUI.OnGUI();
					break;

				case SwitchType.LookCompany:
					_lookCompanyUI.OnGUI();
					break;

				case SwitchType.Filter:
					_filterUI.OnGUI();
					break;
			}
		}
		#endregion

		#region 检测读取数据
		public void CheckReadData(bool force)
		{
			if (CheckReadCompanyData(force) && CheckReadJobData(force))
			{
				ToLog(false, "读取成功");
			}
		}

		private bool CheckReadJobData(bool force)
		{
			if (string.IsNullOrEmpty(editorData.JobFilePath.Value))
			{
				ToLog(true, "职位文件地址不能为空");
				return false;
			}

			var fileName = Path.GetFileNameWithoutExtension(editorData.JobFilePath.Value);
			if (!force && _dataMgr.HasJobData(fileName))
				return true;

			if (File.Exists(editorData.JobFilePath.Value))
			{
				var text = FileUtils.LoadText(editorData.JobFilePath.Value);
				_dataMgr.jobData = JsonHelper.ToObject<JobData>(text);
				_dataMgr.ResetJobCompany();
			}
			else
			{
				_dataMgr.jobData = new JobData();
				_dataMgr.jobData.name = fileName;
			}

			return true;
		}

		private bool CheckReadCompanyData(bool force)
		{
			if (!force && _dataMgr.HasCompanyData())
				return true;

			if (string.IsNullOrEmpty(editorData.CompanyFilePath.Value))
			{
				ToLog(true, "公司文件地址不能为空");
				return false;
			}

			if (File.Exists(editorData.CompanyFilePath.Value))
			{
				var text = FileUtils.LoadText(editorData.CompanyFilePath.Value);
				_dataMgr.companyList = JsonHelper.ToObject<List<CompanyItem>>(text);
			}
			else
			{
				_dataMgr.companyList = new List<CompanyItem>();
			}

			return true;
		}
		#endregion

		#region 保存数据
		public void SaveData()
		{
			if (SaveJobData() && SaveCompanyData())
			{
				ToLog(false, "保存成功");
			}
		}

		private bool SaveJobData()
		{
			if (string.IsNullOrEmpty(editorData.JobFilePath.Value))
			{
				ToLog(true, "职位文件地址不能为空");
				return false;
			}

			if (!_dataMgr.HasJobData())
			{
				ToLog(true, "无需保存的职位信息");
				return false;
			}

			FileUtils.SaveText(editorData.JobFilePath.Value, JsonHelper.ToJson(_dataMgr.jobData), Encoding.UTF8);
			return true;
		}

		private bool SaveCompanyData()
		{
			if (string.IsNullOrEmpty(editorData.CompanyFilePath.Value))
			{
				ToLog(true, "公司文件地址不能为空");
				return false;
			}

			if (!_dataMgr.HasCompanyData())
			{
				ToLog(true, "无需保存的公司信息");
				return false;
			}

			FileUtils.SaveText(editorData.CompanyFilePath.Value, JsonHelper.ToJson(_dataMgr.companyList), Encoding.UTF8);
			return true;
		}
		#endregion

		#region 日志
		public void ToLog(bool isError, string log)
		{
			if (isError)
			{
				_logStyle.normal.textColor = Color.red;

				Debug.LogError(log);
			}
			else
			{
				_logStyle.normal.textColor = Color.green;
			}

			_logStr = $"{DateTime.Now} {log}";
		}
		#endregion
	}
}