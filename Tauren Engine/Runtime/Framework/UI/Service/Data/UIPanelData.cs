/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/7 11:53:23
 *└────────────────────────┘*/

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// UI面板配置数据
	/// </summary>
	public class UIPanelData
	{
		/// <summary> UI面板名 </summary>
		public string name;

		/// <summary> 是否全屏 </summary>
		public string fullScreen;

		/// <summary> 显示层级（自定义）</summary>
		public int layer;

		/// <summary> 是否是唯一面板 </summary>
		public bool unique;

		/// <summary> UI预制资源地址 </summary>
		public string uiAssetPath;
	}
}