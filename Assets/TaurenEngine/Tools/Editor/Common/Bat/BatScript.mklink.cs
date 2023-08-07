/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/1 21:17:42
 *└────────────────────────┘*/

using System.IO;
using TaurenEngine.Runtime.Core;

namespace TaurenEditor.Tools
{
	public partial class BatScript
	{
		#region mklink
		public void MKLink(string fromPath, string toPath)
		{
			fromPath = PathUtils.FormatEnd(fromPath, false);
			toPath = PathUtils.FormatEnd(toPath, false); ;

			if (string.IsNullOrEmpty(fromPath) || string.IsNullOrEmpty(toPath))
				return;

			if (!FileUtils.Exists(fromPath, out var fromIsFile))
				return;// 原文件不存在

			var toIsExist = FileUtils.Exists(toPath, out var toIsFile);

			if (fromIsFile)
			{
				if (!toIsExist)
				{
					if (toIsFile)
						FileUtils.CreateDirectoryByFilePath(toPath);
					else
						FileUtils.CreateDirectoryByDirectoryPath(toPath);
				}

				if (!toIsFile)
					toPath = $"{PathUtils.FormatEnd(toPath, true)}{Path.GetFileName(fromPath)}";

				// 文件链接文件
				AddCmd($"mklink /h \"{toPath}\" \"{fromPath}\"");
			}
			else
			{
				if (toIsExist)// 目标文件已存在，暂不处理
					return;

				var path = Path.GetDirectoryName(toPath);
				FileUtils.CreateDirectoryByDirectoryPath(path);

				// 文件夹链接文件夹
				AddCmd($"mklink /j \"{toPath}\" \"{fromPath}\"");
			}
		}
		#endregion
	}
}