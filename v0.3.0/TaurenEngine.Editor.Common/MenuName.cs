/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2022/1/15 19:26:15
 *└────────────────────────┘*/

namespace TaurenEngine.Editor.Common
{
	public static class MenuName
	{
		#region Root
		private const string Assets = "Assets/";

		private const string TaurenEngine = "TaurenEngine/";
		#endregion

		#region File
		private const string File = TaurenEngine + "File/";

		public const string CopyFile = File + "Copy File";
		public const string LinkFile = File + "Link File";
		#endregion

		#region UI
		/// <summary>
		/// 查找引用过指定图片的所有预制体
		/// </summary>
		public const string FindImageReferencesInUIPrefabs = Assets + "Find Image References In UI Prefabs";
		#endregion

		#region App
		private const string App = TaurenEngine + "App/";

		public const string OpenScriptProject = App + "Open Script Project";
		#endregion
	}
}