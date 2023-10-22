/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 21:26:51
 *└────────────────────────┘*/

namespace Tools.JobAnalysis
{
	public class Salary : NumRange
	{
		/// <summary> 加新奖金 </summary>
		public float bonus;

		public override string ToText()
		{
			return bonus > 0 ? $"{base.ToText()}K·{bonus}薪" : base.ToText() + "K";
		}
	}
}