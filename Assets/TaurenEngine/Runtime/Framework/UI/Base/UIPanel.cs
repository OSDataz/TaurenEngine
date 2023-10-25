/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/24 12:14:38
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Runtime
{
	public class UIPanel
	{
		#region 逻辑数据
		/// <summary> 面板是否打开 </summary>
		public bool IsOpen { get; internal set; }
		#endregion

		#region UI显示对象
		protected GameObject uiObject;
		#endregion
	}
}