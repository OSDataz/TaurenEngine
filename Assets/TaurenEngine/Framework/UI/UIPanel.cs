/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/31 14:25:29
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Framework
{
	public abstract class UIPanel
	{
		public Transform Root { get; private set; }
		/// <summary> 面板是否打开 </summary>
		public bool IsOpen { get; internal set; }

		protected UIPanelSetting setting;

		public virtual void Init(Transform root)
		{
			Root = root;

			setting = root.GetComponent<UIPanelSetting>();
			if (setting == null)
				Debug.LogWarning($"UI面板【{root.name}】未添加并设置UIPanelSetting");
		}

		public void Close()
		{
			TaurenFramework.UI.Close(this);
		}

		public abstract void OnOpen();

		public abstract void OnClose();
	}
}