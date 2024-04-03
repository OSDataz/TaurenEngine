/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/12 0:11:45
 *└────────────────────────┘*/

using Tauren.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Tools.JobAnalysis
{
	public class JobAnalysisFilterUI
	{
		private JobAnalysisWindow _window;

		private DataManager _dataMgr;

		private bool _isFilterKeys = true;
		private bool _isFilterCity = true;
		private bool _isFilterDirect = true;
		private EditorPopup _cityPopup;

		public JobAnalysisFilterUI(JobAnalysisWindow window)
		{
			_window = window;
		}

		public void OnEnable()
		{
			_dataMgr = DataManager.Instance;

			_cityPopup ??= new EditorPopup(JobCity.GetArray());
		}

		public void OnGUI()
		{
			_isFilterKeys = EditorGUILayout.Toggle("过滤关键词：", _isFilterKeys);

			if (_isFilterKeys && _dataMgr.HasJobData())
			{
				_dataMgr.jobData.removeKeys = EditorGUILayout.TextField("剔除关键词：", _dataMgr.jobData.removeKeys);
			}

			EditorGUILayout.BeginHorizontal();
			_isFilterCity = EditorGUILayout.Toggle("过滤城市：", _isFilterCity);
			if (_isFilterCity)
			{
				_cityPopup.Draw();
			}
			EditorGUILayout.EndHorizontal();

			_isFilterDirect = EditorGUILayout.Toggle("过滤非直招：", _isFilterDirect);

			EditorGUILayout.BeginHorizontal();
			if (GUILayout.Button("打印所有职位", GUILayout.Height(25)))
			{
				PrintAllJobInfo();
			}

			if (GUILayout.Button("打印筛选职位", GUILayout.Height(25)))
			{
				PrintFilterJobInfo();
			}
			EditorGUILayout.EndHorizontal();
		}

		#region 打印职位信息
		private void PrintAllJobInfo()
		{
			_window.CheckReadData(false);

			_dataMgr.PrintAllJobInfo();

			_window.ToLog(false, "打印成功");
		}

		private void PrintFilterJobInfo()
		{
			_window.CheckReadData(false);

			_dataMgr.PrintFilterJobInfo(
				_isFilterKeys,
				_isFilterCity ? _cityPopup.Value : string.Empty,
				_isFilterDirect);

			_window.ToLog(false, "打印成功");
		}
		#endregion
	}
}