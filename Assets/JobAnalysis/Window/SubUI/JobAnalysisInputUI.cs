/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/4 20:26:16
 *└────────────────────────┘*/

using System;
using Tauren.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Tools.JobAnalysis
{
	/// <summary>
	/// 录入数据显示
	/// </summary>
	public class JobAnalysisInputUI
	{
		private JobAnalysisWindow _window;

		private EditorPopup _jobPlatformPopup;
		private EditorPopup _cityPopup;

		private Vector2 _scrollPos;
		private string _inputContent;

		private DataManager _dataMgr;

		private ParseBoss _parseBoss;
		private ParseLaGou _parseLaGou;

		public JobAnalysisInputUI(JobAnalysisWindow window)
		{
			_window = window;
		}

		public void OnEnable()
		{
			_scrollPos = Vector2.zero;
			_inputContent = string.Empty;

			_jobPlatformPopup ??= new EditorPopup(JobPlatform.GetArray());
			_cityPopup ??= new EditorPopup(JobCity.GetArray());

			_dataMgr = DataManager.Instance;

			_parseBoss ??= new ParseBoss();
			_parseLaGou ??= new ParseLaGou();
		}

		public void OnGUI()
		{
			if (_dataMgr.HasJobData())
			{
				_dataMgr.jobData.searchKeys = EditorGUILayout.TextField("搜索关键词：", _dataMgr.jobData.searchKeys);
			}

			EditorGUILayout.BeginHorizontal();
			_jobPlatformPopup.Draw();
			_cityPopup.Draw();

			if (GUILayout.Button("解析内容"))
			{
				ParseContent();
			}
			EditorGUILayout.EndHorizontal();

			_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
			_inputContent = EditorGUILayout.TextArea(_inputContent, GUILayout.ExpandHeight(true));
			EditorGUILayout.EndScrollView();
		}

		private void ParseContent()
		{
			if (string.IsNullOrEmpty(_inputContent))
			{
				_window.ToLog(true, "内容不能为空");
				return;
			}

			_window.CheckReadData(false);

			try
			{
				string log = string.Empty;

				switch (_jobPlatformPopup.Value)
				{
					case JobPlatform.Boss:
						log = _parseBoss.Parse(_inputContent, _cityPopup.Value);
						break;

					case JobPlatform.LaGou:
						log = _parseLaGou.Parse(_inputContent, _cityPopup.Value);
						break;
				}

				_window.ToLog(false, log);
			}
			catch (Exception ex)
			{
				_window.ToLog(true, $"解析失败：{ex}");
			}
		}
	}
}