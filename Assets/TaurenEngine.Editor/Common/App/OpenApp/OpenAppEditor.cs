/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.4.0
 *│　Time    ：2022/2/10 14:35:11
 *└────────────────────────┘*/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Common
{
	public static class OpenAppEditor
	{
		[MenuItem("TaurenEngine/App/Open Script Project")]
		private static void OpenScriptProject()
		{
			var path = Application.dataPath;
			path = path.Substring(0, path.Length - 7);
			path = $"{path}{path.Substring(path.LastIndexOf('/'))}.sln";

			if (!File.Exists(path))
			{
				Debug.LogWarning($"代码路径不存在：{path}");
				return;
			}

			Debug.Log($"打开项目：{path}");
			EditorUtility.OpenWithDefaultApp(path);
		}
	}
}