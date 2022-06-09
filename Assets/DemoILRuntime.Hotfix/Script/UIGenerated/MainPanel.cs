/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/9 11:52:42
 *│
 *│  工具自动生成，切勿自行修改。
 *└────────────────────────┘*/

using TaurenEngine.Framework;
using UnityEngine;

namespace DemoILRuntime.Hotfix
{
	public class MainPanel : UIPanel
	{
		public UnityEngine.UI.Text LogText { get; private set; }
		public UnityEngine.UI.Button Button { get; private set; }

		public override void Init(Transform root)
		{
			base.Init(root);
			
			LogText = root.Find("Bg/*LogText")?.GetComponent<UnityEngine.UI.Text>();
			Button = root.Find("Bg/*Button")?.GetComponent<UnityEngine.UI.Button>();
		}
	}
}