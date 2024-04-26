/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/18 11:03:05
 *└────────────────────────┘*/

using UnityEngine;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 通用屏幕适配
	/// 横向边距默认留96像素，宽高比例越小，96像素留白区越小
	/// </summary>
	public class UniversalScreenAdapter : MonoBehaviour
	{
		#region 参数
		private const float margin = 96f;// 屏幕边距，UI已经预留刘海空间
		private const float minRatio = 1920f / 1080f;// 最小屏幕比例
		private const float maxRatio = 2340f / 1080f;// 最大屏幕比例
		#endregion

		protected void Awake()
		{
			AdaptScreen();
		}

		public void AdaptScreen()
		{
			RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
			float ratio = (float)Screen.width / Screen.height;
			float t = (maxRatio - Mathf.Clamp(ratio, minRatio, maxRatio)) / (maxRatio - minRatio);
			rectTransform.offsetMin = new Vector2(Mathf.Max(-margin * t, rectTransform.offsetMin.x - margin), 0);
			rectTransform.offsetMax = new Vector2(Mathf.Min(margin * t, rectTransform.offsetMax.x + margin), 0);
		}
	}
}