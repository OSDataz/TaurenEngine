/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 21:13:53
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace Tools.JobAnalysis
{
	/// <summary>
	/// 职位数据
	/// </summary>
	public class JobData
	{
		/// <summary> 职位名 </summary>
		public string name;

		/// <summary> 搜索词 </summary>
		public string searchKeys;
		/// <summary> 剔除词 </summary>
		public string removeKeys;
		public string[] RemoveKeyList { get; set; }

		/// <summary> 职位列表 </summary>
		public List<JobItem> jobItems = new List<JobItem>();
	}
}