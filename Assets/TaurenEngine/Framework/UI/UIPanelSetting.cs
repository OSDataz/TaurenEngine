/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/31 15:39:00
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Framework
{
	public class UIPanelSetting : MonoBehaviour
	{
		[SerializeField]
		private string _uiGroupName;

		private UIGroup _uiGroup;
		internal UIGroup UIGroup
		{
			get
			{
				if (_uiGroup == null)
					_uiGroup = TaurenFramework.UI.FindUIGroup(_uiGroupName);
				return _uiGroup;
			}
		}

		/// <summary>
		/// 使用UI摄像机
		/// </summary>
		public bool useUICamera;
		/// <summary>
		/// 适配屏幕
		/// </summary>
		public bool adaptScreen;
		/// <summary>
		/// 是否使用多实例
		/// </summary>
		public bool allowMultiInstance;
	}
}