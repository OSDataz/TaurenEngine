/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/7 0:25:16
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	/// <summary>
	/// UI组件
	/// </summary>
	public class UIComponent : FrameworkComponent
	{
		public UIGroup[] uiGroups;

		protected override void Awake()
		{
			base.Awake();

			if (TaurenFramework.UI == null)
			{
				var ui = new UIManager();
				ui.Init(uiGroups);
				TaurenFramework.UI = ui;
			}
		}
	}
}