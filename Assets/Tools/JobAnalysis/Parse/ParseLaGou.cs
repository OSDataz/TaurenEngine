/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/6 12:23:59
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace Tools.JobAnalysis
{
	public class ParseLaGou : ParseBase
	{
		public string Parse(string content, string city)
		{
			content = content.Replace(" ", "");

			var list = content.Split("\r\n");
			if (list.Length == 0)
				return "无数据";

			var dataMgr = DataManager.Instance;

			var len = list.Length;
			int i = list[0] == "排序方式：默认最新" ? 1 : 0;
			for (; i < len; ++i)
			{
				// 职位名、城市、地区
				if (!GetStr(ref list, ref i, out var str))
					return $"第{i}行职位地区解析异常";

				if (!ParseNameArea(str, out var jobName, out var area))
					return $"第{i}行职位地区解析异常：{str}";

				// 发布时间
				if (!GetNextStr(ref list, ref i, out str))
					return $"第{i}行发布时间解析异常";

				if (!ParseReleaseDate(str, out var releaseDate))
					return $"第{i}行发布时间解析异常：{str}";

				if (list[i + 1] == "前程无忧")
					i += 1;

				// 薪资、工作经验、学历
				if (!GetNextStr(ref list, ref i, out str))
					return $"第{i}行薪资、工作经验、学历解析异常";

				if (!ParseSalaryExpEdu(str, out var salaryMin, out var salaryMax, out var workExperience, out var education))
					return $"第{i}行薪资、工作经验、学历解析异常：{str}";

				// 公司名
				if (!GetNextStr(ref list, ref i, out string companyName))
					return $"第{i}行公司名解析异常";

				// 公司行业、融资情况、公司规模
				if (!GetNextStr(ref list, ref i, out str))
					return $"第{i}行公司行业、融资情况、公司规模解析异常";

				if (!ParseCompanyInfo(str, companyName, ref i, out var companyIndustry, out var companyFinancing, out var sizeMin, out var sizeMax))
					return $"第{i}行公司行业、融资情况、公司规模解析异常：{str}";

				// 工作要求和福利待遇
				if (!GetNextStr(ref list, ref i, out str))
					return $"第{i}行工作要求和福利待遇解析异常";

				if (!ParseRequirementsWelfare(str, out var jobRequirementsStr, out var welfareStr))
					return $"第{i}行工作要求和福利待遇解析异常：{str}";

				var jobItem = dataMgr.GetJobItem(JobPlatform.LaGou,
						jobName, companyName, string.Empty, salaryMin, salaryMax, 0);

				jobItem.recruitType = RecruitType.Direct;
				jobItem.city = city;
				jobItem.area = area;
				jobItem.workExperience = workExperience;
				jobItem.education = education;
				jobItem.jobRequirementsStr = jobRequirementsStr;
				jobItem.welfareStr = welfareStr;
				jobItem.company.SetData(companyIndustry, companyFinancing, sizeMin, sizeMax);
				jobItem.releaseDate = releaseDate;

				Debug.Log(jobItem.ToText());
			}

			return "解析成功";
		}

		private bool IsFirstData(string str)
		{
			return str.Contains('[');
		}

		private bool ParseNameArea(string str, out string jobName, out string area)
		{
			var list = str.Split('[');
			if (list.Length <= 1)
			{
				jobName = string.Empty;
				area = string.Empty;
				return false;
			}

			jobName = list[0];
			area = list[1].Replace("]", "");
			return true;
		}

		private bool ParseReleaseDate(string str, out string releaseDate)
		{
			if (str.Contains(":"))
			{
				releaseDate = DateTime.Now.ToString("yyyy-MM-dd");
			}
			else if (str == "1天前发布")
			{
				releaseDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
			}
			else
			{
				releaseDate = str;
			}

			return true;
		}

		private bool ParseSalaryExpEdu(string str,
			out float salaryMin, out float salaryMax, out string workExperience, out string education)
		{
			var index = str.LastIndexOf("/");
			if (index == -1)
			{
				salaryMin = 0;
				salaryMax = 0;
				workExperience = string.Empty;
				education = string.Empty;
				return false;
			}

			education = str.Substring(index + 1);

			var list = str.Substring(0, index).Split("经验");
			if (list.Length != 2)
			{
				salaryMin = 0;
				salaryMax = 0;
				workExperience = string.Empty;
				return false;
			}

			workExperience = list[1];

			list = list[0].Replace("k", "").Split("-");
			if (list.Length != 2)
			{
				salaryMin = 0;
				salaryMax = 0;
				return false;
			}

			salaryMin = float.Parse(list[0]);
			salaryMax = float.Parse(list[1]);
			return true;
		}

		private bool ParseCompanyInfo(string str, string companyName, ref int i,
			out string companyIndustry, out string companyFinancing, out float sizeMin, out float sizeMax)
		{
			if (str == companyName)
			{
				companyIndustry = string.Empty;
				companyFinancing = string.Empty ;
				sizeMin = 0 ;
				sizeMax = 0 ;
				return true;
			}

			str = str.Replace(" ", "");
			var list = str.Split("/");
			if (list.Length != 3)
			{
				companyIndustry = string.Empty;
				companyFinancing = string.Empty;
				sizeMin = 0;
				sizeMax = 0;
				return false;
			}

			companyIndustry = list[0];
			companyFinancing = list[1];

			i += 1;

			// 公司规模
			str = list[2];
			if (str.StartsWith("少于"))
			{
				sizeMin = 0;
				sizeMax = float.Parse(str.Substring(2, str.Length - 3));
				return true;
			}
			else
			{
				return ParseCompanySize(list[2], out sizeMin, out sizeMax);
			}
		}

		private bool ParseRequirementsWelfare(string str,
			out string jobRequirementsStr, out string welfareStr)
		{
			if (IsFirstData(str))
			{
				jobRequirementsStr = string.Empty;
				welfareStr = string.Empty;
				return true;
			}

			if (!str.Contains('“'))
			{
				jobRequirementsStr = str;
				welfareStr = string.Empty;
				return true;
			}

			var list = str.Replace("”", "").Split('“');
			if (list.Length != 2)
			{
				jobRequirementsStr = string.Empty;
				welfareStr = string.Empty;
				return false;
			}

			jobRequirementsStr = list[0];
			welfareStr = list[1];
			return true;
		}
	}
}