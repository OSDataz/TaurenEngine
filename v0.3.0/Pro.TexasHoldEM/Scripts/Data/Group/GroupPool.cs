/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/4/11 9:17:06
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace Pro.TexasHoldEM
{
	public partial class GroupPool : Singleton<GroupPool>
	{
		public void Init(bool force)
		{
			if (!force && HasData)
				return;

			InitGroup5();
			InitGroup2();
		}

		/// <summary>
		/// 是否有数据（已加载/已生成）
		/// </summary>
		public bool HasData
			=> Group5s != null && Group5s.Count > 0 && Group2s != null && Group2s.Count > 0;
	}
}