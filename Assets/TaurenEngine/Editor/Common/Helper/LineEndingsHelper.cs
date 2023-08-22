/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/9 21:06:03
 *└────────────────────────┘*/

using System.IO;

namespace TaurenEngine.Editor
{
	/// <summary>
	/// 文件行尾结束符转换
	/// </summary>
	public static class LineEndingsHelper
	{
		/// <summary>
		/// 文件行尾转换为CRLF
		/// </summary>
		/// <param name="filePath"></param>
		public static void FileConvertToCRLF(string filePath)
		{
			string fileContent = File.ReadAllText(filePath);
			fileContent = fileContent.Replace("\r\n", "\n").Replace("\n", "\r\n");
			File.WriteAllText(filePath, fileContent);
		}

		/// <summary>
		/// 文件夹内所有文件行尾转换为CRLF
		/// </summary>
		/// <param name="folderPath"></param>
		public static void FolderConvertToCRLF(string folderPath)
		{
			string[] filePaths = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

			foreach (string filePath in filePaths)
			{
				FileConvertToCRLF(filePath);
			}
		}
	}
}