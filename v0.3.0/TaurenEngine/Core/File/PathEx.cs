/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/9 19:47:39
 *└────────────────────────┘*/

using System.IO;

namespace TaurenEngine.Core
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
	}
}