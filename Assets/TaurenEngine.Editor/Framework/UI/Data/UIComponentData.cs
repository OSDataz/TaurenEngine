/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/2 14:42:28
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public class UIComponentData : ScriptableObject
	{
		/// <summary>
		/// UI预制体路径
		/// </summary>
		public string uiPrefabPath;

		/// <summary>
		/// 生成UI代码保存路径
		/// </summary>
		public string generateSavePath;

		/// <summary>
		/// 生成UI代码命名空间
		/// </summary>
		public string codeNamespace;
	}
}