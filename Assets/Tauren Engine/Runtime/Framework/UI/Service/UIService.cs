/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/24 12:10:49
 *└────────────────────────┘*/

using UnityEngine;

namespace Tauren.Framework.Runtime
{
	public class UIService : IUIService
	{
		public UIData UIData { get; private set; }

		public UIService()
		{
			this.InitInterface();

			UIData = new UIData();
		}

		#region 初始化
		/// <summary>
		/// 初始化UI层级
		/// </summary>
		/// <param name="layers">层级</param>
		/// <param name="names">层级命名</param>
		public void InitUILayer(int[] layers, string[] names)
		{

		}
		#endregion

		#region 显示面板
		public void Open<T>() where T : MonoBehaviour, IUIPanel
		{
			if (!UIData.TryGetPanelData(typeof(T), out var uiPanelData))
				return;

			if (uiPanelData.unique)
			{
				if (UIData.TryGetUIPanel(uiPanelData, out var uiPanel))
				{
					Open(uiPanel);// 面板已打开显示，尝试把隐藏的面板打开
					return;
				}
			}

			var newUIPanel = UIData.CreateUIPanel(uiPanelData);
			Open(newUIPanel);// 打开新的面板
		}

		private void Open(UIPanel uiPanel)
		{
			if (uiPanel.isOpen)
			{
				Show(uiPanel);// 面板已经打开，检测显示
			}
			else
			{
				if (!CheckLoadUI(uiPanel))
					return;

				Show(uiPanel);
			}
		}

		private bool CheckLoadUI(UIPanel uiPanel)
		{
			if (uiPanel.loadStatus == UIPanel.LoadNone)
			{

			}
			else if (uiPanel.loadStatus == UIPanel.LoadCompleted)
			{
				return uiPanel.uiObject != null;
			}

			return false;
		}
		#endregion
		
		#region 关闭面板
		public void Close<T>(T panel = null) where T : MonoBehaviour, IUIPanel
		{
			

		}

		private void Close(UIPanel uiPanel)
		{

		}
		#endregion

		#region 显示面板
		private void Show(UIPanel uiPanel)
		{
			//if (!uiPanel.isOpen)
			//	return;

			//if (!uiPanel.isHide)
			//	return;

			//uiPanel.isHide = true;
			//uiPanel.Show();
		}
		#endregion

		#region 隐藏面板
		private void Hide(UIPanel uiPanel)
		{
			//if (!uiPanel.isOpen)
			//	return;

			//if (uiPanel.isHide)
			//	return;

			//uiPanel.isHide = false;
			//uiPanel.Hide();
		}
		#endregion
	}
}