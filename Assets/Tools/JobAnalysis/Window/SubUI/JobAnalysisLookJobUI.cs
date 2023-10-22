/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/4 22:35:56
 *└────────────────────────┘*/

using System.Text;
using TaurenEngine.Editor;
using UnityEditor;
using UnityEngine;

namespace Tools.JobAnalysis
{
	public class JobAnalysisLookJobUI
	{
		private JobAnalysisWindow _window;

		private DataManager _dataMgr;

		private EditorPopup _jobPlatformPopup;
		private EditorPopup _wishStatusPopup;
		private EditorPopup _interviewStatusPopup;

		private string _jobName;
		private string _companyName;

		private string _content;

		private JobItem _jobItem;

		public JobAnalysisLookJobUI(JobAnalysisWindow window)
		{
			_window = window;
		}

		public void OnEnable()
		{
			_content = string.Empty;
			_jobItem = null;

			_dataMgr = DataManager.Instance;

			_jobPlatformPopup ??= new EditorPopup(JobPlatform.GetArray());
			_wishStatusPopup ??= new EditorPopup(WishStatus.GetArray());
			_interviewStatusPopup ??= new EditorPopup(InterviewStatus.GetArray());
		}

		public void OnGUI()
		{
			// 查询栏
			EditorGUILayout.BeginHorizontal();
			EditorGUIUtility.labelWidth = 60;
			_jobPlatformPopup.Draw("招聘平台：", GUILayout.Width(140));
			_jobName = EditorGUILayout.TextField("职位全称：", _jobName);
			_companyName = EditorGUILayout.TextField("公司全称：", _companyName);

			if (GUILayout.Button("查询", GUILayout.Width(100)))
			{
				FindData();
			}
			EditorGUILayout.EndHorizontal();

			// 可编辑信息栏
			if (_jobItem != null)
			{
				EditorGUILayout.BeginHorizontal();
				_jobItem.wishStatus = _wishStatusPopup.Draw("意向状态：", _jobItem.wishStatus);
				_jobItem.interviewStatus = _interviewStatusPopup.Draw("面试状态：", _jobItem.interviewStatus);
				EditorGUILayout.EndHorizontal();

				_jobItem.notes = EditorGUILayout.TextArea(_jobItem.notes, GUILayout.Height(200));
			}

			// 固定信息栏
			EditorGUILayout.LabelField(_content, GUILayout.Height(300));
		}

		private void FindData()
		{
			_window.CheckReadData(false);

			_jobItem = _dataMgr.jobData.jobItems.Find(item => item.platform == _jobPlatformPopup.Value
				&& item.jobName == _jobName
				&& item.company.name == _companyName
				);

			if (_jobItem != null)
			{
				var build = new StringBuilder();
				build.AppendLine($"平        台：{_jobItem.platform}");
				build.AppendLine($"职  位  名：{_jobItem.jobName}");
				build.AppendLine($"薪        资：{_jobItem.salary.ToText()}");
				build.AppendLine($"工作经验：{_jobItem.workExperience}");
				build.AppendLine($"学        历：{_jobItem.education}");
				build.AppendLine($"城市地区：{_jobItem.city} {_jobItem.area}");

				build.AppendLine("");
				build.AppendLine($"职位要求：{_jobItem.jobRequirementsStr}");
				build.AppendLine($"福利待遇：{_jobItem.welfareStr}");

				build.AppendLine("");
				build.AppendLine($"公  司  名：{_jobItem.company.name}");
				build.AppendLine($"行        业：{_jobItem.company.industry}");
				build.AppendLine($"员  工  数：{_jobItem.company.size.ToText()}人");
				build.AppendLine($"融资情况：{_jobItem.company.financing}");
				build.AppendLine($"招  聘  者：{_jobItem.recruiter}");

				build.AppendLine("");
				build.AppendLine($"发布日期：{_jobItem.releaseDate}");
				build.AppendLine($"更新时间：{_jobItem.updateDate}");

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