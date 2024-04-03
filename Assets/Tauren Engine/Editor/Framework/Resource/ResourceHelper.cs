/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/2/28 17:28:20
 *└────────────────────────┘*/

using System.IO;
using Tauren.Core.Editor;
using UnityEngine;

namespace Tauren.Framework.Editor
{
	public static class ResourceHelper
	{
		public static void CheckExternalDependencies(string directoryPath)
		{
			directoryPath = directoryPath.Replace("/", "\\");
			Debug.Log($"选中资源文件夹：{directoryPath}");

			// 汇总文件夹资源
			var list = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
			foreach (var path in list)
			{
				Debug.Log($"检测文件：{path}");
				var useList = DependenciesUtils.GetDirectDependencies(path);
				foreach (var usePath in useList)
				{
					Debug.Log($"依赖：{usePath}");
				}
			}
		}
	}
}