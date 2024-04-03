/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/18 20:40:13
 *└────────────────────────┘*/

using System.IO;
using System;
using UnityEditor;
using UnityEngine;

namespace Tauren.Core.Editor
{
	public static class AssetsUtils
	{
		/// <summary>
		/// 遍历指定路径下的所有预制体
		/// </summary>
		/// <param name="findCall"></param>
		/// <param name="path"></param>
		public static void ForeachPrefabs(Action<GameObject> findCall, string path = "Assets")
		{
			if (findCall == null)
			{
				Debug.LogError("ForeachPerfabs 回调函数不能为空");
				return;
			}

			var rootPath = Application.dataPath.Replace("/", "\\");
			rootPath = rootPath.Substring(0, rootPath.Length - 6);

			DirectoryInfo directory = new DirectoryInfo(path);
			FileInfo[] files = directory.GetFiles("*", SearchOption.AllDirectories);
			var len = files.Length;
			for (var i = 0; i < len; ++i)
			{
				if (files[i].Name.EndsWith(".prefab"))
				{
					var filePath = files[i].FullName.Replace(rootPath, "");
					GameObject prefab = PrefabUtility.LoadPrefabContents(filePath);
					if (prefab == null)
					{
						Debug.LogError($"预制体未加载成功：{path}");
						continue;
					}

					findCall.Invoke(prefab);

					PrefabUtility.UnloadPrefabContents(prefab);
				}
			}
		}
	}
}