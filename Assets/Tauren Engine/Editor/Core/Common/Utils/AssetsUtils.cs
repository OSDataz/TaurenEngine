/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/18 20:40:13
 *└────────────────────────┘*/

using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Tauren.Core.Editor
{
	public static class AssetsUtils
	{
		/// <summary>
		/// 获取忽略ignoreSuffixs后缀的所有文件
		/// </summary>
		/// <param name="dirPath"></param>
		/// <param name="ignoreSuffixs">规范使用小写后缀，如[".meta", ".cs"]</param>
		/// <returns></returns>
		public static List<string> GetAllFilesIgnoreExt(string dirPath, string[] ignoreSuffixs)
		{
			if (!Directory.Exists(dirPath))
			{
				Debug.LogError(@"未找到指定文件夹：{dirPath}");
				return new List<string>();
			}

			var files = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories);
			var res = new List<string>();
			foreach (var file in files)
			{
				var suffix = Path.GetExtension(file).ToLower();
				if (ignoreSuffixs.Contains(suffix))
					continue;

				res.Add(file);
			}

			return res;
		}
	}
}