/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 20:50:59
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.Text;
using Tauren.Core.Runtime;
using UnityEngine;

namespace Tools.JobAnalysis
{
	/// <summary>
	/// 数据管理器
	/// </summary>
	public class DataManager : Singleton<DataManager>
	{
		#region 职位数据
		/// <summary> 职位列表 </summary>
		public JobData jobData;

		public bool HasJobData() => jobData != null;

		public bool HasJobData(string name) => jobData != null && jobData.name == name;

		public int GetJobCount() => jobData?.jobItems.Count ?? 0;

		public JobItem GetJobItem(string platform, 
			string jobName, string companyName, string recruiter,
			float salaryMin, float salaryMax, float bonus)
		{
			var jobItem = jobData.jobItems.Find(item => item.platform == platform 
				&& item.jobName == jobName 
				&& item.company.name == companyName
				&& item.recruiter == recruiter
				&& item.salary.min == salaryMin
				&& item.salary.max == salaryMax
				&& item.salary.bonus == bonus
				);

			if (jobItem == null)
			{
				jobItem = new JobItem();
				jobItem.platform = platform;
				jobItem.jobName = jobName;
				jobItem.salary = new Salary();
				jobItem.salary.min = salaryMin;
				jobItem.salary.max = salaryMax;
				jobItem.salary.bonus = bonus;
				jobItem.recruiter = recruiter;
				jobItem.company = GetCompanyItem(companyName);

				jobData.jobItems.Add(jobItem);
			}

			jobItem.updateDate = DateTime.Now.ToString("yyyy-MM-dd");

			return jobItem;
		}

		public void ResetJobCompany()
		{
			foreach (var jobItem in jobData.jobItems)
			{
				jobItem.wishStatus = WishStatus.None;
				jobItem.interviewStatus = InterviewStatus.None;

				var companyItem = companyList.Find(item => item.name == jobItem.company.name);
				if (companyItem != null)
				{
					jobItem.company = companyItem;
				}
				else
				{
					companyList.Add(jobItem.company);

					Debug.LogWarning($"职位未找到匹配公司 {jobItem.ToText()}");
				}
			}

			foreach (var item in companyList)
			{
				item.wishStatus = WishStatus.None;
			}
		}

		public void GenerateRemoveKeyArray()
		{
			if (string.IsNullOrEmpty(jobData.removeKeys))
				return;

			jobData.removeKeys = jobData.removeKeys.Replace(",", "，");
			jobData.RemoveKeyList = jobData.removeKeys.Split("，");

			var len = jobData.RemoveKeyList.Length;
			for (int i = 0; i < len; ++i)
			{
				jobData.RemoveKeyList[i] = jobData.RemoveKeyList[i].ToLower();
			}
		}
		#endregion

		#region 公司数据
		/// <summary> 公司列表 </summary>
		public List<CompanyItem> companyList;

		public bool HasCompanyData() => companyList != null;

		public int GetCompanyCount() => companyList?.Count ?? 0;

		public CompanyItem GetCompanyItem(string companyName)
		{
			var companyItem = companyList.Find(item => item.name == companyName);

			if (companyItem == null)
			{
				companyItem = new CompanyItem();
				companyItem.name = companyName;
				companyItem.size = new NumRange();

				companyList.Add(companyItem);
			}

			return companyItem;
		}
		#endregion

		#region 筛选条件
		private bool CheckFilterJob(JobItem item)
		{
			if (item.wishStatus == WishStatus.Refuse)
				return false;

			if (item.interviewStatus == InterviewStatus.IRefuse || item.interviewStatus == InterviewStatus.Rejected)
				return false;

			var nameLow = item.jobName.ToLower();
			foreach (var key in jobData.RemoveKeyList)
			{
				if (nameLow.Contains(key))
					return false;
			}

			return true;
		}
		#endregion

		#region 打印复制文本
		public void PrintAllJobInfo()
		{
			var build = new StringBuilder();

			foreach (var item in jobData.jobItems)
			{
				var str = item.ToText();

				build.AppendLine(str);
				Debug.Log(str);
			}

			GUIUtility.systemCopyBuffer = build.ToString();
		}

		public void PrintFilterJobInfo(bool isFilterKeys, string city, bool isFilterDirect)
		{
			if (isFilterKeys)
				GenerateRemoveKeyArray();

			var isFilterCity = !string.IsNullOrEmpty(city);
			var build = new StringBuilder();

			foreach (var item in jobData.jobItems)
			{
				if (isFilterDirect && item.recruitType != RecruitType.Direct)
					continue;

				if (isFilterCity && item.city != city)
					continue;

				if (isFilterKeys && !CheckFilterJob(item))
					continue;

				var str = item.ToText();

				build.AppendLine(str);
				Debug.Log(str);
			}

			GUIUtility.systemCopyBuffer = build.ToString();
		}
		#endregion
	}
}