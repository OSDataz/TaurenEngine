/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/7 0:25:21
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	/// <summary>
	/// UI管理器
	/// </summary>
	public class UIManager
	{
		private UIGroup[] _uiGroups;

		internal void Init(UIGroup[] uiGroups)
		{
			_uiGroups = uiGroups;
		}

		#region UI组
		internal UIGroup FindUIGroup(string uiGroupName)
		{
			if (_uiGroups == null)
				return null;

			var len = _uiGroups.Length;
			for (int i = 0; i < len; ++i)
			{
				if (_uiGroups[i].Name == uiGroupName)
					return _uiGroups[i];
			}

			return null;
		}
		#endregion

		#region 显示面板
		public T Open<T>() where T : UIPanel
		{
			return null;
		}
		#endregion

		#region 关闭面板
		public void Close<T>(T panel) where T :UIPanel
		{
			if (panel == null || !panel.IsOpen)
				return;


		}
		#endregion
	}
}