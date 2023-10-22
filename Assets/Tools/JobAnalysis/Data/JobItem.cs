/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 20:33:19
 *└────────────────────────┘*/

using System.Collections.Generic;
using System.Text;

namespace Tools.JobAnalysis
{
	/// <summary>
	/// 单个职位信息
	/// </summary>
	public class JobItem
	{
		#region 读取数据
		public string platform;
		public string recruitType;
		public string jobName;
		public Salary salary;
		public string workExperience;
		public string education;
		public string city;
		public string area;

		/// <summary> 工作要求 </summary>
		public string jobRequirementsStr;
		/// <summary> 福利待遇 </summary>
		public string welfareStr;

		public CompanyItem company;

		/// <summary> 招聘者 </summary>
		public string recruiter;

		/// <summary> 更新时间 </summary>
		public string updateDate;
		/// <summary> 发布日期 </summary>
		public string releaseDate;
		#endregion

		#region 记录数据
		/// <summary> 意向状态 </summary>
		public string wishStatus = WishStatus.None;

		/// <summary> 面试状态 </summary>
		public string interviewStatus = InterviewStatus.None;

		/// <summary> 备注 </summary>
		public string notes;
		#endregion

		public string ToText()
		{
			return $"{platform} {recruitType} {jobName} {salary.min}-{salary.max}-{salary.bonus} {workExperience} {education} {city} {area} {company.ToText()} {recruiter} {releaseDate} {updateDate} {wishStatus} {interviewStatus} {jobRequirementsStr} {welfareStr} {ToNotes()}";
		}

		private string ToArrayStr(IEnumerable<string> list, char interval)
		{
			var build = new StringBuilder();
			foreach (var str in list)
			{
				if (build.Length > 0)
					build.Append(interval);

				build.Append(str);
			}

			return build.ToString();
		}

		private string ToNotes()
		{
			if (string.IsNullOrWhiteSpace(notes))
				return string.Empty;

			return ToArrayStr(notes.Replace("\r\n", "\n").Replace("\r", "\n").Split("\n"), '。');
		}
	}
}