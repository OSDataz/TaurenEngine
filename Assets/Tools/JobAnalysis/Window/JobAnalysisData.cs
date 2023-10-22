/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 12:15:17
 *└────────────────────────┘*/

using UnityEngine;

namespace Tools.JobAnalysis
{
	public class JobAnalysisData : ScriptableObject
	{
		/// <summary>
		/// 职位数据文件路径
		/// </summary>
		public string jobFilePath;
		/// <summary>
		/// 公司数据文件路径
		/// </summary>
		public string companyFilePath;
	}
}