/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 20:27:32
 *└────────────────────────┘*/

using UnityEngine;

namespace Tools.JobAnalysis
{
	public class ParseBoss : ParseBase
	{
		public string Parse(string content, string flag)
		{
			content = content.Replace(" ", "");

			var list = content.Split("\r\n");
			if (list.Length == 0)
				return "无数据";

			var dataMgr = DataManager.Instance;

			var len = list.Length;
			for (int i = 0; i < len; ++i)
			{
				if (!GetStr(ref list, ref i, out var str))
					break;

				if (IsFirstData(str, flag))
				{
					// 招聘方式
					if (ParseRecruitType(str, out var recruitType))
					{
						if (!GetNextStr(ref list, ref i, out str))
							return $"第{i}行职位地区解析异常：{str}";
					}

					// 职位名、城市、地区
					if (!ParseNameArea(str, flag, out var jobName, out var city, out var area))
						return $"第{i}行职位地区解析异常：{str}";

					if (jobName.StartsWith('某'))
						recruitType = RecruitType.Headhunting;

					// 薪资
					if (!GetNextStr(ref list, ref i, out var salary))
						return $"第{i}行薪资解析异常";

					if (!ParseSalary(salary, out var salaryMin, out var salaryMax, out var bonus))
						return $"第{i}行薪资解析异常：{salary}";

					// 工作年限
					if (!GetNextStr(ref list, ref i, out var workExperience))
						return $"第{i}行工作年限解析异常";

					if (workExperience.Contains('天'))
					{
						if (!GetNextStr(ref list, ref i, out workExperience))
							return $"第{i}行工作年限解析异常";
					}

					// 学历
					if (!GetNextStr(ref list, ref i, out var education))
						return $"第{i}行学历解析异常";

					// 招聘者
					if (!GetNextStr(ref list, ref i, out var recruiter))
						return $"第{i}行招聘者解析异常";

					// 公司名
					if (GetNextStr(ref list, ref i, out var companyName))
					{
						if (companyName == "在线" && !GetNextStr(ref list, ref i, out companyName))
							return $"第{i}行公司名解析异常";
					}
					else
						return $"第{i}行公司名解析异常";

					// 公司行业
					if (!GetNextStr(ref list, ref i, out var companyIndustry))
						return $"第{i}行公司行业解析异常";

					// 融资情况
					if (GetNextStr(ref list, ref i, out var companyFinancing))// 可能没有融资数据
					{
						// 公司规模
						if (companyFinancing.IndexOf("人") == -1)
						{
							if (!GetNextStr(ref list, ref i, out var companySize))
								return $"第{i}行公司规模解析异常";

							str = companySize;
						}
						else
						{
							str = companyFinancing;

							companyFinancing = string.Empty;
						}
					}
					else
						return $"第{i}行融资情况解析异常";

					// 公司规模
					if (!ParseCompanySize(str, out var sizeMin, out var sizeMax))
						return $"第{i}行公司规模解析异常：{str}";

					// 工作要求和福利待遇
					if (!ParseRequirementsWelfare(ref list, ref i, flag, out var jobRequirementsStr, out var welfareStr))
						return $"第{i}行工作要求和福利待遇解析异常：{list[i]}";

					var jobItem = dataMgr.GetJobItem(JobPlatform.Boss, 
						jobName, companyName, recruiter, salaryMin, salaryMax, bonus);

					jobItem.recruitType = recruitType;
					jobItem.city = city;
					jobItem.area = area;
					jobItem.workExperience = workExperience;
					jobItem.education = education;
					jobItem.jobRequirementsStr = jobRequirementsStr;
					jobItem.welfareStr = welfareStr;
					jobItem.company.SetData(companyIndustry, companyFinancing, sizeMin, sizeMax);

					Debug.Log(jobItem.ToText());
				}
				else
				{
					return $"第{i}行未识别异常数据：{str}";
				}
			}

			return "解析成功";
		}

		private bool IsFirstData(string str, string flag)
		{
			return str == RecruitType.Proxy || str == RecruitType.Headhunting || str.IndexOf('·') != -1 || str.EndsWith(flag);
		}

		private bool ParseRecruitType(string str, out string recruitType)
		{
			if (str == RecruitType.Proxy || str == RecruitType.Headhunting)
			{
				recruitType = str;
				return true;
			}
			else
			{
				recruitType = RecruitType.Direct;
				return false;
			}
		}

		private bool ParseNameArea(string str, string flag,
			out string jobName, out string city, out string area)
		{
			var index = str.LastIndexOf(flag);
			if (index == -1)
			{
				jobName = string.Empty;
				city = string.Empty;
				area = string.Empty;
				return false;
			}

			jobName = str.Substring(0, index);

			str = str.Substring(index);
			index = str.IndexOf('·');
			if (index == -1)
			{
				city = str;
				area = string.Empty;
			}
			else
			{
				city = str.Substring(0, index);
				area = str.Substring(index + 1);
			}

			return true;
		}

		private bool ParseSalary(string str,
			out float min, out float max, out float bonus)
		{
			var index = str.IndexOf('·');
			if (index != -1)
			{
				bonus = float.Parse(str.Substring(index + 1, str.Length - index - 2));
				str = str.Substring(0, index);
			}
			else
			{
				bonus = 0;
			}

			index = str.IndexOf('-');
			if (index == -1)
			{
				min = 0;
				max = 0;
				return false;
			}

			min = float.Parse(str.Substring(0, index));
			str = str.Substring(index + 1);

			if (str.EndsWith("元/月"))
			{
				str = str.Substring(0, str.Length - 3);
				min /= 1000;
				max = float.Parse(str) / 1000;
			}
			else if (str.EndsWith("元/周"))
			{
				str = str.Substring(0, str.Length - 3);
				min = min * 4 / 1000;
				max = float.Parse(str) * 4 / 1000;
			}
			else if (str.EndsWith("元/天"))
			{
				str = str.Substring(0, str.Length - 3);
				min = min * 22 / 1000;
				max = float.Parse(str) * 22 / 1000;
			}
			else if (str.EndsWith("K"))
			{
				str = str.Substring(0, str.Length - 1);
				max = float.Parse(str);
			}
			else
			{
				max = 0;
			}

			return true;
		}

		private bool ParseRequirementsWelfare(ref string[] list, ref int i, string flag,
			out string jobRequirementsStr, out string welfareStr)
		{
			jobRequirementsStr = string.Empty;
			welfareStr = string.Empty;

			var j = i + 1;
			if (!GetStr(ref list, ref j, out var str))// 下一个当前的文本
				return false;

			var lastStr = string.Empty;

			while (!IsFirstData(str, flag))
			{
				if (!string.IsNullOrEmpty(lastStr))
				{
					// 工作要求
					if (string.IsNullOrEmpty(jobRequirementsStr))
						jobRequirementsStr = lastStr;
					else
						jobRequirementsStr += '，' + lastStr;
				}

				lastStr = str;

				i = j;// 当前文本索引
				j = i + 1;
				if (!GetStr(ref list, ref j, out str))
					break;
			}

			if (!string.IsNullOrEmpty(lastStr))
			{
				if (lastStr.IndexOf('，') == -1)
				{
					// 工作要求
					if (string.IsNullOrEmpty(jobRequirementsStr))
						jobRequirementsStr = lastStr;
					else
						jobRequirementsStr += '，' + lastStr;
				}
				else
				{
					// 福利待遇
					welfareStr = lastStr;
				}
			}

			return true;
		}
	}
}