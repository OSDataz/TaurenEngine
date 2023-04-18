/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/10/18 14:04:09
 *└────────────────────────┘*/

using System.IO;
using TaurenEngine.Core;
using UnityEngine;
using UnityEngine.Android;
using Filec = TaurenEngine.Core.Filex;

namespace TaurenEngine.Unity
{
	public static class Filex
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
			if (Pathx.EqualExtension(ext, ".jpg"))
				bytes = image.EncodeToJPG();
			else if (Pathx.EqualExtension(ext, ".png"))
				bytes = image.EncodeToPNG();
			else if (Pathx.EqualExtension(ext, ".tga"))
				bytes = image.EncodeToTGA();
			else if (Pathx.EqualExtension(ext, ".exr"))
				bytes = image.EncodeToEXR();
			else
			{
				Log.Error($"保存图片路径后缀未识别：{filePath}");
				return;
			}

			Filec.CreateDirectoryByFilePath(filePath);

			FileStream stream = File.Open(filePath, FileMode.OpenOrCreate);
			stream.Write(bytes, 0, bytes.Length);
			stream.Flush();
			stream.Close();
			stream.Dispose();
		}
		#endregion
	}
}