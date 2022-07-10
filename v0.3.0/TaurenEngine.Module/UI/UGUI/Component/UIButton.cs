/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/2/7 9:53:58
 *└────────────────────────┘*/

using UnityEngine.UI;

namespace TaurenEngine.UGUI
{
	public class UIButton
	{
		public Button UI { get; private set; }

		public UIButton(Button button)
		{
			UI = button;
		}
	}
}