/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 16:34:19
 *└────────────────────────┘*/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tauren.Framework.Runtime
{
	public class MD5Utils
	{
		public static string ToMD5(string content)
		{
			return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(UTF8Encoding.Default.GetBytes(content))).Replace("-", "").ToLower();
		}

		public static string ToMD5(Stream file)
		{
			return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(file)).Replace("-", "").ToLower();
		}
	}
}