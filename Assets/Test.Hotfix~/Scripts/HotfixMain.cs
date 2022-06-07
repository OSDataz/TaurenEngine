/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/6 11:37:49
 *└────────────────────────┘*/

using TaurenEngine.Framework;
using UnityEngine;

namespace Test.Hotfix
{
	public class HotfixMain
	{
		public static void Start()
		{
			Debug.Log("热更运行成功 v0.5");

			new HotfixMain();
		}

		public HotfixMain()
		{
			TaurenFramework.Event.AddEvent("SendOK", OnEvent);

			var mainPanelGo = TaurenFramework.Resource.Load<GameObject>("UI/MainPanel", LoadType.Resources);
			if (mainPanelGo != null)
			{
				var go = GameObject.Instantiate(mainPanelGo);

				var mainPanel = new MainPanelUI();
				mainPanel.Init(go.transform);

				mainPanel.LogText.text = "显示面板成功";
				mainPanel.Button.onClick.AddListener(OnClick);
			}
			else
				Debug.LogError("加载MainPanel失败");
		}

		private void OnClick()
		{
			TaurenFramework.Event.Send("SendOK");

			TaurenFramework.Frame.AddTimer(1f, () => {
				Debug.Log("延迟执行成功");
			});
		}

		private void OnEvent()
		{
			Debug.Log("事件触发响应成功");
		}
	}
}