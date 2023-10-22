/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 21:26:16
 *└────────────────────────┘*/

namespace Tools.JobAnalysis
{
	/// <summary>
	/// min = -1 表示 100+ 格式
	/// </summary>
	public class NumRange
	{
		public float min;
		public float max;

		public virtual string ToText()
		{
			return min == -1 ? $"{max}+" : $"{min}-{max}";
		}
	}
}