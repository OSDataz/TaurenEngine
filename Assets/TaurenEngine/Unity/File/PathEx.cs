/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2021/11/9 19:47:39
 *└────────────────────────┘*/

using System.IO;

namespace TaurenEngine.Unity
{
	public static class PathEx
	{
		/// <summary>
		/// 检测路径是否是指定的后缀
		/// </summary>
		/// <param name="path">路径</param>
		/// <param name="extension">后缀</param>
		/// <returns></returns>
		public static bool IsExtension(string path, string extension)
		{
			return EqualExtension(Path.GetExtension(path), extension);
		}

		/// <summary>
		/// 比较后缀名是否相等
		/// </summary>
		/// <param name="pathExt">路径后缀</param>
		/// <param name="extension">比较后缀</param>
		/// <returns></returns>
		public static bool EqualExtension(string pathExt, string extension)
		{
			return pathExt.ToLower() == extension.ToLower();
		}

		/// <summary>
		/// 格式化文件夹路径，开头是否需要斜杆
		/// </summary>
		/// <param name="path"></param>
		/// <param name="needSlash"></param>
		/// <returns></returns>
		public static string FormatPathStart(string path, bool needSlash)
		{
			if (string.IsNullOrEmpty(path))
				return path;

			var c = path[0];
			if (c == '/' || c == '\\')
			{
				if (!needSlash)
					return path.Substring(1);
			}
			else
			{
				if (needSlash)
					return path + "/";
			}

			return path;
		}

		/// <summary>
		/// 格式化文件夹路径，末尾是否需要斜杆
		/// </summary>
		/// <param name="path"></param>
		/// <param name="needSlash"></param>
		/// <returns></returns>
		public static string FormatPathEnd(string path, bool needSlash)
		{
			if (string.IsNullOrEmpty(path))
				return path;

			var c = path[path.Length - 1];
			if (c == '/' || c == '\\')
			{
				if (!needSlash)
					return path.Substring(0, path.Length - 1);
			}
			else
			{
				if (needSlash)
					return path + "/";
			}

			return path;
		}
	}
}