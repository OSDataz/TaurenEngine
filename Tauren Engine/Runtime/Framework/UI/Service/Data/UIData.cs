/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/7 11:53:11
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public class UIData
	{
		/// <summary> UI面板数据 </summary>
		public readonly Dictionary<string, UIPanelData> UIPanelMap = new Dictionary<string, UIPanelData>();

		/// <summary> UI层级数据 </summary>
		internal readonly Dictionary<int, List<UIPanel>> UILayerMap = new Dictionary<int, List<UIPanel>>();

		public void Clear()
		{
			UIPanelMap.Clear();
			UILayerMap.Clear();
		}

		internal UIPanel CreateUIPanel(UIPanelData uiPanelData)
		{
			var uiPanel = UIPanel.GetFromPool();
			uiPanel.data = uiPanelData;
			UILayerMap[uiPanelData.layer].Add(uiPanel);

			return uiPanel;
		}

		internal bool TryGetPanelData(Type uiType, out UIPanelData uiPanelData)
		{
			var name = uiType.Name;

			if (!UIPanelMap.TryGetValue(name, out uiPanelData))
			{
				Log.Error($"获取未配置的UI面板：{name}");
				return false;
			}

			return true;
		}

		internal bool TryGetLayerList(UIPanelData uiPanelData, out List<UIPanel> uiList)
		{
			if (!UILayerMap.TryGetValue(uiPanelData.layer, out uiList))
			{
				Log.Error($"UI面板【{uiPanelData.name}】层级【{uiPanelData.layer}】未配置异常");
				return false;
			}

			return true;
		}

		internal bool TryGetUIPanel(UIPanelData uiPanelData, out UIPanel uiPanel)
		{
			uiPanel = null;

			if (!TryGetLayerList(uiPanelData, out var uiList))
				return false;

			foreach (var itemData in uiList)
			{
				if (itemData.data == uiPanelData)
					uiPanel = itemData;
			}

			return true;
		}
	}
}