/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 20:40:09
 *└────────────────────────┘*/

namespace Tools.JobAnalysis
{
	public class CompanyItem
	{
		#region 读取数据
		/// <summary> 公司规模 </summary>
		public string name;
		/// <summary> 公司规模 </summary>
		public NumRange size;
		/// <summary> 公司行业 </summary>
		public string industry;
		/// <summary> 融资情况 </summary>
		public string financing;
		#endregion

		#region 记录数据
		/// <summary> 意向状态 </summary>
		public string wishStatus = WishStatus.None;

		/// <summary> 备注 </summary>
		public string notes;
		#endregion

		public void SetData(string industry, string financing, float sizeMin, float sizeMax)
		{
			this.industry = industry;
			this.financing = financing;

			if (sizeMin > size.min || sizeMin == -1)
				size.min = sizeMin;

			if (sizeMax < size.max || size.max == 0)
				size.max = sizeMax;
		}

		public string ToText()
		{
			return $"{name} {size.min}-{size.max} {industry} {financing}";
		}
	}
}