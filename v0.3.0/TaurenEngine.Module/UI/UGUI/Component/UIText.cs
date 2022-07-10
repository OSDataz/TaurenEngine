/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/2/7 9:54:03
 *└────────────────────────┘*/

using UnityEngine.UI;

namespace TaurenEngine.UGUI
{
	public class UIText
	{
		public Text UI { get; private set; }

		public UIText(Text text)
		{
			UI = text;
		}
	}
}