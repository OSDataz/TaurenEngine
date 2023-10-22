/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/4 22:36:25
 *└────────────────────────┘*/

using System.Text;
using TaurenEngine.Editor;
using UnityEditor;
using UnityEngine;
using static PlasticGui.LaunchDiffParameters;

namespace Tools.JobAnalysis
{
	public class JobAnalysisLookCompanyUI
	{
		private JobAnalysisWindow _window;

		private DataManager _dataMgr;

		private EditorPopup _wishStatusPopup;

		private string _companyName;

		private string _content;

		private CompanyItem _companyItem;

		public JobAnalysisLookCompanyUI(JobAnalysisWindow window)
		{
			_window = window;
		}

		public void OnEnable()
		{
			_content = string.Empty;
			_companyItem = null;

			_dataMgr = DataManager.Instance;

			_wishStatusPopup ??= new EditorPopup(WishStatus.GetArray());
		}

		public void OnGUI()
		{
			// 查询栏
			EditorGUILayout.BeginHorizontal();
			EditorGUIUtility.labelWidth = 60;
			_companyName = EditorGUILayout.TextField("公司全称：", _companyName);

			if (GUILayout.Button("查询", GUILayout.Width(100)))
			{
				FindData();
			}
			EditorGUILayout.EndHorizontal();

			// 可编辑信息栏
			if (_companyItem != null)
			{
				EditorGUILayout.BeginHorizontal();
				_companyItem.wishStatus = _wishStatusPopup.Draw("意向状态：", _companyItem.wishStatus);
				EditorGUILayout.EndHorizontal();

				_companyItem.notes = EditorGUILayout.TextArea(_companyItem.notes, GUILayout.Height(200));
			}

			// 固定信息栏
			EditorGUILayout.LabelField(_content, GUILayout.Height(120));
		}

		private void FindData()
		{
			_window.CheckReadData(false);

			_companyItem = _dataMgr.companyList.Find(item => item.name == _companyName);

			if (_companyItem != null)
			{
				var build = new StringBuilder();
				build.AppendLine($"公  司  名：{_companyItem.name}");
				build.AppendLine($"行        业：{_companyItem.industry}");
				build.AppendLine($"员  工  数：{_companyItem.size.ToText()}人");
				build.AppendLine($"融资情况：{_companyItem.financing}");

				_content = build.ToString();
				_window.ToLog(false, "查询成功");
			}
			else
			{
				_window.ToLog(true, "查询失败");
			}
		}
	}
}