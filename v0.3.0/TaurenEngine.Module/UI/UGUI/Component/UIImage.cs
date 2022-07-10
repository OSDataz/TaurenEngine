/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/2/7 9:58:28
 *└────────────────────────┘*/

using UnityEngine.UI;

namespace TaurenEngine.UGUI
{
	public class UIImage
	{
		public Image UI { get; private set; }

		public UIImage(Image image)
		{
			UI = image;
		}
	}
}