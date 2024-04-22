/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/18 11:50:33
 *└────────────────────────┘*/

using UnityEngine;
using UnityEngine.UI;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 全屏背景图适配，根据CanvasScaler自动调节缩放，按高度适配
	/// </summary>
	public class BackgroundAdapter : MonoBehaviour
	{
		protected void Start()
		{
			AdaptScreen();
		}

		public void AdaptScreen()
		{
			//设置背景节点缩放
			var scaler = GetComponentInParent<CanvasScaler>(true);
			var scale = ((RectTransform)scaler.transform).rect.height / scaler.referenceResolution.y;
			transform.localScale = Vector3.one * scale;
		}
	}
}