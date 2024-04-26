/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/9 21:06:03
 *└────────────────────────┘*/

using System.IO;
using System.Text;
using Tauren.Core.Runtime;
using UnityEngine;

namespace Tauren.Framework.Editor
{
	public enum LineEndingsType
	{
		/// <summary>
		///  (Carriage Return, 回车) 的ASCII码是13，十六进制表示为\r
		///  适用设备：早期的设备
		/// </summary>
		CR,
		/// <summary>
		///  (Line Feed, 换行) 的ASCII码是10，十六进制表示为\n
		///  适用设备：Unix、Linux、Mac
		/// </summary>
		LF,
		/// <summary>
		/// 适用设备：Windows
		/// </summary>
		CRLF
	}

	public enum EncodingType
	{
		ASCII,
		Unicode,
		BigEndianUnicode,
		UTF8,
		UTF8_BOM,
		UTF7,
		UTF32
	}

	/// <summary>
	/// 文件行尾结束符转换
	/// </summary>
	public static class FileFormatHelper
	{
		public static void FormatPath(string path, LineEndingsType lineEnding, EncodingType encoding)
		{
			if (string.IsNullOrEmpty(path))
			{
				Debug.LogError("格式化失败，文件/文件夹地址为空");
				return;
			}

			if (!FileUtils.Exists(path, out var isFile))
			{
				Debug.LogError($"格式化失败，文件/文件夹地址未找到：{path}");
				return;
			}

			if (isFile)
				FormatFile(path, lineEnding, encoding);
			else
				FormatFolder(path, lineEnding, encoding);

			Debug.Log($"格式化完成：{path}");
		}

		/// <summary>
		/// 格式化文件行尾和编码
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="lineEnding"></param>
		/// <param name="encoding"></param>
		public static void FormatFile(string filePath, LineEndingsType lineEnding, EncodingType encoding)
		{
			string fileContent = File.ReadAllText(filePath);

			fileContent = fileContent.Replace("\r\n", "\n");
			if (lineEnding == LineEndingsType.CRLF)
				fileContent = fileContent.Replace("\r", "\n").Replace("\n", "\r\n");
			else if (lineEnding == LineEndingsType.LF)
				fileContent = fileContent.Replace("\r", "\n");
			else if (lineEnding == LineEndingsType.CR)
				fileContent = fileContent.Replace("\n", "\r");

			File.WriteAllText(filePath, fileContent, ToEncoding(encoding));
		}

		/// <summary>
		/// 格式化文件夹下文件行尾和编码
		/// </summary>
		/// <param name="folderPath"></param>
		/// <param name="encoding"></param>
		public static void FormatFolder(string folderPath, LineEndingsType lineEnding, EncodingType encoding)
		{
			string[] filePaths = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

			foreach (string filePath in filePaths)
			{
				FormatFile(filePath, lineEnding, encoding);
			}
		}

		private static Encoding ToEncoding(EncodingType encoding)
		{
			if (encoding == EncodingType.ASCII) return Encoding.ASCII;
			if (encoding == EncodingType.Unicode) return Encoding.Unicode;
			if (encoding == EncodingType.BigEndianUnicode) return Encoding.BigEndianUnicode;
			if (encoding == EncodingType.UTF8) return new UTF8Encoding(false);
			if (encoding == EncodingType.UTF8_BOM) return new UTF8Encoding(true);
			if (encoding == EncodingType.UTF7) return Encoding.UTF7;
			if (encoding == EncodingType.UTF32) return Encoding.UTF32;

			return Encoding.Default;
		}
	}
}