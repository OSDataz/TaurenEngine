/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/10/18 14:04:09
 *└────────────────────────┘*/

using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Android;

namespace TaurenEngine.Core
{
	public static class FileEx
	{
		#region 读写权限
		/// <summary>
		/// 请求文件读写权限
		/// </summary>
		public static void RequestPermission()
		{
			Permission.RequestUserPermission("android.permission.WRITE_EXTERNAL_STORAGE");
		}
		#endregion

		#region 路径
		/// <summary>
		/// Android系统根路径
		/// </summary>
		public const string AndroidRootPath = "/sdcard/";
		#endregion

		#region 文件通用接口
		/// <summary>
		/// 当前路径是文件还是文件夹
		/// </summary>
		/// <param name="path">路径</param>
		/// <returns>True 文件；False 文件夹</returns>
		public static bool IsFile(ref string path)
		{
			return Path.HasExtension(path) || File.Exists(path);
		}

		/// <summary>
		/// 指定路径是否存在
		/// </summary>
		/// <param name="path">路径</param>
		/// <param name="isFile">True 文件；False 文件夹</param>
		/// <returns>该路径是否存在</returns>
		public static bool Exists(ref string path, out bool isFile)
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

		/// <summary>
		/// 创建存储路径
		/// </summary>
		/// <param name="path">路径</param>
		/// <param name="isFile">True 文件；False 文件夹</param>
		public static bool CheckCreatePath(ref string fromPath, ref string toPath, out bool fromIsFile, out bool toIsFile)
		{
			if (!CheckPath(ref fromPath, ref toPath, out fromIsFile, out toIsFile, out var toIsExist))
				return false;

			if (!toIsExist)
			{
				if (toIsFile)
					CreateSaveFilePath(ref toPath);
				else
					CreateSaveDirectoryPath(ref toPath);
			}
			return true;
		}

		public static bool CheckPath(ref string fromPath, ref string toPath, out bool fromIsFile, out bool toIsFile, out bool toIsExist)
		{
			if (string.IsNullOrEmpty(fromPath) || string.IsNullOrEmpty(toPath))
			{
				fromIsFile = false;
				toIsFile = false;
				toIsExist = false;
				return false;
			}

			if (!Exists(ref fromPath, out fromIsFile))
			{
				toIsFile = false;
				toIsExist = false;
				TDebug.Error($"原地址未找到：{fromPath}");
				return false;
			}

			toIsExist = Exists(ref toPath, out toIsFile);
			if (toIsFile && !fromIsFile)
				toIsFile = false;

			return true;
		}

		/// <summary>
		/// 创建存储路径
		/// </summary>
		/// <param name="path">路径</param>
		public static void CreateSaveFilePath(ref string filePath)
		{
			var directoryPath = Path.GetDirectoryName(filePath);
			CreateSaveDirectoryPath(ref directoryPath);
		}

		public static void CreateSaveDirectoryPath(ref string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);
		}

		/// <summary>
		/// 删除文件夹下所有文件
		/// </summary>
		/// <param name="path"></param>
		public static void ClearDirectory(ref string path)
		{
			var rootInfo = new DirectoryInfo(path);

			var fileInfos = rootInfo.GetFiles();
			var len = fileInfos.Length;
			for (int i = 0; i < len; ++i)
			{
				fileInfos[i].Delete();
			}

			var direInfos = rootInfo.GetDirectories();
			len = direInfos.Length;
			for (int i = 0; i < len; ++i)
			{
				direInfos[i].Delete(true);
			}
		}

		/// <summary>
		/// 格式化文件夹路径
		/// </summary>
		/// <param name="path"></param>
		public static string FormatDirectoryPath(string path)
		{
			if (string.IsNullOrEmpty(path))
				return path;

			var c = path[path.Length - 1];
			if (c == '/' || c == '\\')
				return path.Substring(0, path.Length - 1);

			return path;
		}
		#endregion

		#region 存储/读取 文本
		public static void SaveText(string filePath, string content)
		{
			CreateSaveFilePath(ref filePath);

			StreamWriter writer = File.CreateText(filePath);
			writer.Write(content);
			writer.Close();
			writer.Dispose();
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
				CreateSaveFilePath(ref filePath);
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
			CreateSaveFilePath(ref filePath);

			FileStream stream = File.Open(filePath, FileMode.OpenOrCreate);
			BinaryFormatter format = new BinaryFormatter();
			format.Serialize(stream, content);
			stream.Close();
			stream.Dispose();
		}

		public static object LoadObject(string filePath)
		{
			if (File.Exists(filePath))
			{
				FileStream stream = File.Open(filePath, FileMode.Open);
				BinaryFormatter format = new BinaryFormatter();
				object content = format.Deserialize(stream);
				stream.Close();
				stream.Dispose();
				return content;
			}
			else
				return null;
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
			if (!CheckCreatePath(ref sourcePath, ref destPath, out var sourIsFile, out var destIsFile))
				return false;

			if (sourIsFile)
			{
				if (destIsFile)
				{
					File.Copy(sourcePath, destPath, true);
				}
				else
				{
					// 将文件复制到文件夹中
					File.Copy(sourcePath, $"{FormatDirectoryPath(destPath)}/{Path.GetFileName(sourcePath)}", true);
				}
			}
			else
			{
				if (destIsFile)
				{
					TDebug.Error($"不能将文件夹复制到一个文件中。源路径：{sourcePath} 目标路径：{destPath}");
					return false;
				}

				sourcePath = FormatDirectoryPath(sourcePath);
				destPath = FormatDirectoryPath(destPath);

				// 从一个文件夹复制到另一个文件夹
				if (isFull)
					ClearDirectory(ref destPath);

				CopyDirectory(sourcePath, destPath);
			}

			return true;
		}

		private static void CopyDirectory(string sourcePath, string destPath)
		{
			var sourDireInfo = new DirectoryInfo(sourcePath);

			// 复制文件
			var sourFiles = sourDireInfo.GetFiles();
			var len = sourFiles.Length;
			for (int i = 0; i < len; ++i)
			{
				var info = sourFiles[i];
				File.Copy(info.FullName, $"{destPath}/{info.Name}", true);
			}

			// 复制文件夹
			var sourDires = sourDireInfo.GetDirectories();
			len = sourDires.Length;
			for (int i = 0; i < len; ++i)
			{
				var info = sourDires[i];
				var destDircPath = $"{destPath}/{info.Name}";

				CreateSaveDirectoryPath(ref destDircPath);
				CopyDirectory(info.FullName, destDircPath);
			}
		}
		#endregion

		#region 存储/读取 图片
		public static void SaveImage(string filePath, Texture2D image)
		{
			byte[] bytes;
			var ext = Path.GetExtension(filePath);
			if (PathEx.EqualExtension(ext, ".jpg"))
				bytes = image.EncodeToJPG();
			else if (PathEx.EqualExtension(ext, ".png"))
				bytes = image.EncodeToPNG();
			else if (PathEx.EqualExtension(ext, ".tga"))
				bytes = image.EncodeToTGA();
			else if (PathEx.EqualExtension(ext, ".exr"))
				bytes = image.EncodeToEXR();
			else
			{
				Debug.LogError($"保存图片路径后缀未识别：{filePath}");
				return;
			}

			CreateSaveFilePath(ref filePath);

			FileStream stream = File.Open(filePath, FileMode.OpenOrCreate);
			stream.Write(bytes, 0, bytes.Length);
			stream.Flush();
			stream.Close();
			stream.Dispose();
		}
		#endregion
	}
}