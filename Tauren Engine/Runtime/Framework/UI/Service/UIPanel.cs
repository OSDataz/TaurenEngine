/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/24 12:14:38
 *└────────────────────────┘*/

using Tauren.Core.Runtime;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	internal class UIPanel : RecycleObject<UIPanel>
	{
		#region 配置数据
		public UIPanelData data;
		#endregion

		#region 动态逻辑数据
		/// <summary> 面板是否打开 </summary>
		public bool isOpen;

		/// <summary> 面板是否显示 </summary>
		public bool isShow;
		#endregion

		#region UI显示对象
		/// <summary> 未加载 </summary>
		public const int LoadNone = 0;
		/// <summary> 加载中 </summary>
		public const int Loading = 1;
		/// <summary> 加载完成 </summary>
		public const int LoadCompleted = 2;

		/// <summary> 加载状态 </summary>
		public int loadStatus;

		/// <summary> UI对象 </summary>
		public GameObject uiObject;
		#endregion

		public override void Clear()
		{
			data = null;

			isOpen = false;
			isShow = false;

			loadStatus = LoadNone;
			uiObject = null;
		}

		public void Show()
		{

		}

		public void Hide()
		{

		}
	}
}