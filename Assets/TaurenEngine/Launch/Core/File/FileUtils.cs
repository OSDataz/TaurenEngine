/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/7/28 21:24:18
 *└────────────────────────┘*/

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace TaurenEngine.Launch
{
	/// <summary>
	/// 请求文件读写权限
	/// Permission.RequestUserPermission("android.permission.WRITE_EXTERNAL_STORAGE");
	/// </summary>
	public static class FileUtils
	{
		/// <summary>
		/// 指定路径是否存在
		/// </summary>
		/// <param name="path">路径</param>
		/// <param name="isFile">True 文件；False 文件夹</param>
		/// <returns>该路径是否存在</returns>
		public static bool Exists(string path, out bool isFile)
		{
			if (Directory.Exists(path))
			{
				isFile = false;
				return true;
			}
			else if (File.Exists(path))
			{
				isFile = true;
				return true;
			}

			isFile = Path.HasExtension(path);// 默认有后缀的是文件
			return false;
		}

		#region 文件夹处理
		/// <summary>
		/// 创建文件夹
		/// </summary>
		/// <param name="directoryPath"></param>
		public static void CreateDirectoryByDirectoryPath(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);
		}

		/// <summary>
		/// 创建文件夹
		/// </summary>
		/// <param name="filePath"></param>
		public static void CreateDirectoryByFilePath(string filePath)
		{
			CreateDirectoryByDirectoryPath(Path.GetDirectoryName(filePath));
		}

		/// <summary>
		/// 删除文件夹下的所有文件
		/// </summary>
		/// <param name="directoryPath"></param>
		public static void DeleteFiles(string directoryPath)
		{
			var info = new DirectoryInfo(directoryPath);

			var files = info.GetFiles();
			var len = files.Length;
			for (int i = 0; i < len; ++i)
			{
				files[i].Delete();
			}

			var dirs = info.GetDirectories();
			len = dirs.Length;
			for (int i = 0; i < len; ++i)
			{
				dirs[i].Delete(true);
			}
		}
		#endregion

		#region 存储/读取 文本
		public static void SaveText(string filePath, string content)
		{
			CreateDirectoryByFilePath(filePath);

			StreamWriter writer = File.CreateText(filePath);
			writer.Write(content);
			writer.Close();
			writer.Dispose();
		}

		public static void SaveText(string filePath, string content, Encoding encoding)
		{
			CreateDirectoryByFilePath(filePath);

			File.WriteAllText(filePath, content, encoding);
		}

		/// <summary>
		/// 默认UTF-8解析格式
		/// </summary>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static string LoadText(string filePath)
			=> File.ReadAllText(filePath);

		public static string LoadText(string filePath, Encoding encoding)
			=> File.ReadAllText(filePath, encoding);
		#endregion

		#region 存储/读取 文本行
		public static bool SaveLine(string filePath, string content)
		{
			FileInfo fileInfo = new FileInfo(filePath);
			StreamWriter writer;
			if (fileInfo.Exists)
			{
				writer = fileInfo.AppendText();
			}
			else
			{
				CreateDirectoryByFilePath(filePath);
				writer = fileInfo.CreateText();
			}
			writer.WriteLine(content);
			writer.Close();
			writer.Dispose();

			return true;
		}

		public static List<string> LoadLine(string filePath)
		{
			if (File.Exists(filePath))
			{
				StreamReader reader = File.OpenText(filePath);
				List<string> list = new List<string>();
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					list.Add(line);
				}

				reader.Close();
				reader.Dispose();
				return list;
			}
			else
				return null;
		}
		#endregion

		#region 序列化存储/读取
		public static void SaveObject(string filePath, object content)
		{
			CreateDirectoryByFilePath(filePath);

			FileStream stream = File.Open(filePath, FileMode.OpenOrCreate);
			BinaryFormatter format = new BinaryFormatter();
			format.Serialize(stream, content);
			stream.Close();
			stream.Dispose();
		}

		public static bool LoadObject(string filePath, out object data)
		{
			if (File.Exists(filePath))
			{
				FileStream stream = File.Open(filePath, FileMode.Open);
				BinaryFormatter format = new BinaryFormatter();
				data = format.Deserialize(stream);
				stream.Close();
				stream.Dispose();
				return true;
			}
			else
			{
				data = default;
				return false;
			}
		}
		#endregion

		#region 复制文件
		/// <summary>
		/// 复制文件或文件夹
		/// </summary>
		/// <param name="sourcePath">源路径</param>
		/// <param name="destPath">目标路径</param>
		/// <param name="isFull">是否全覆盖</param>
		/// <returns></returns>
		public static bool Copy(string sourcePath, string destPath, bool isFull = false)
		{
			// 地址检测
			if (string.IsNullOrWhiteSpace(sourcePath) || string.IsNullOrWhiteSpace(destPath))
				return false;// 路径为空

			// 来源地址检测
			if (!Exists(sourcePath, out var sourceIsFile))
				return false;// 原地址未找到

			// 开始复制
			if (sourceIsFile)
			{
				// 复制地址检测
				if (!Exists(destPath, out var destIsFile))
				{
					if (destIsFile)
						CreateDirectoryByFilePath(destPath);
					else
						CreateDirectoryByDirectoryPath(destPath);
				}

				if (destIsFile)
				{
					// 文件 到 文件
					File.Copy(sourcePath, destPath, true);
				}
				else
				{
					// 文件 到 文件夹
					File.Copy(sourcePath, $"{PathUtils.FormatEnd(destPath, true)}{Path.GetFileName(sourcePath)}", true);
				}
			}
			else
			{
				// 文件夹 到 文件夹
				sourcePath = PathUtils.FormatEnd(sourcePath, false);
				destPath = PathUtils.FormatEnd(destPath, false);

				// 从一个文件夹复制到另一个文件夹
				if (isFull)
					DeleteFiles(destPath);

				CopyDirectory(sourcePath, destPath);
			}

			return true;
		}

		private static void CopyDirectory(string sourcePath, string destPath)
		{
			CreateDirectoryByDirectoryPath(destPath);

			var info = new DirectoryInfo(sourcePath);

			// 复制文件
			var files = info.GetFiles();
			var len = files.Length;
			for (int i = 0; i < len; ++i)
			{
				var file = files[i];

				File.Copy(file.FullName, $"{destPath}/{file.Name}", true);
			}

			// 复制文件夹
			var dirs = info.GetDirectories();
			len = dirs.Length;
			for (int i = 0; i < len; ++i)
			{
				var dir = dirs[i];

				CopyDirectory(dir.FullName, $"{destPath}/{dir.Name}");
			}
		}
		#endregion

		#region Android
		/// <summary>
		/// Android系统根路径
		/// </summary>
		public const string AndroidRootPath = "/sdcard/";
		#endregion

		#region 图片文件
		public static void SaveImage(string filePath, Texture2D image)
		{
			byte[] bytes;
			var ext = Path.GetExtension(filePath);
			if (PathUtils.EqualExtension(ext, ".jpg"))
				bytes = image.EncodeToJPG();
			else if (PathUtils.EqualExtension(ext, ".png"))
				bytes = image.EncodeToPNG();
			else if (PathUtils.EqualExtension(ext, ".tga"))
				bytes = image.EncodeToTGA();
			else if (PathUtils.EqualExtension(ext, ".exr"))
				bytes = image.EncodeToEXR();
			else
			{
				Log.Error($"保存图片路径后缀未识别：{filePath}");
				return;
			}

			CreateDirectoryByFilePath(filePath);

			FileStream stream = File.Open(filePath, FileMode.OpenOrCreate);
			stream.Write(bytes, 0, bytes.Length);
			stream.Flush();
			stream.Close();
			stream.Dispose();
		}
		#endregion
	}
}