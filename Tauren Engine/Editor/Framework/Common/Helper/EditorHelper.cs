/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/2 18:07:17
 *└────────────────────────┘*/

using System.IO;
using UnityEngine;

namespace Tauren.Framework.Editor
{
	public static class EditorHelper
	{
		private static string projectPath;
		/// <summary> 项目路径 </summary>
		public static string ProjectPath
		{
			get
			{
				if (string.IsNullOrEmpty(projectPath))
					projectPath = Path.GetDirectoryName(Application.dataPath);

				return projectPath;
			}
		}

		/// <summary>
		/// 配置路径
		/// </summary>
		public static string ConfigPath = "Assets/TaurenEngine.Config";

		public static string ConfigFullPath => $"{ProjectPath}/{ConfigPath}";

		/// <summary>
		/// 运行存储文件路径
		/// </summary>
		public static string DebugFullPath => $"{ProjectPath}/Debug";

		public static string BuildFullPath => $"{ProjectPath}/Build";
	}
}