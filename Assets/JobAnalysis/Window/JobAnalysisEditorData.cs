/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 12:15:26
 *└────────────────────────┘*/

using TaurenEditor;

namespace Tools.JobAnalysis
{
	public class JobAnalysisEditorData : EditorData<JobAnalysisData>
	{
		protected override string SavePath => "Assets/Tools/JobAnalysis/Config/JobAnalysisConfig.asset";

		protected override void UpdateProperty()
		{
			JobFilePath = GetProperty(JobFilePath, nameof(Data.jobFilePath));
			CompanyFilePath = GetProperty(CompanyFilePath, nameof(Data.companyFilePath));
		}

		public PropertyString JobFilePath { get; private set; }
		public PropertyString CompanyFilePath { get; private set; }
	}
}