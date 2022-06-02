/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/2 18:07:17
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Editor
{
	public class EditorHelper
	{
		private static string projectPath;
		/// <summary> 项目路径 </summary>
		public static string ProjectPath
		{
			get
			{
				if (string.IsNullOrEmpty(projectPath))
					projectPath = Application.dataPath.Substring(0, Application.dataPath.Length - 6);

				return projectPath;
			}
		}
	}
}