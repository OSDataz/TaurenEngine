/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/2/28 17:37:39
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEditor;

namespace TaurenEditor
{
	/// <summary>
	/// 引用依赖
	/// </summary>
	public static class DependenciesUtils
	{
		/// <summary>
		/// 获取直接依赖项
		/// </summary>
		/// <param name="pathName"></param>
		/// <returns></returns>
		public static List<string> GetDirectDependencies(string pathName)
		{
			var arr = AssetDatabase.GetDependencies(pathName, false);
			var paths = new List<string>();
			foreach (var path in arr)
			{
				paths.Add(path);
			}

			return paths;
		}
	}
}