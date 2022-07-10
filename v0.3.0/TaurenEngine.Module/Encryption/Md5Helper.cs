/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 16:34:19
 *└────────────────────────┘*/

using System;
using System.Security.Cryptography;
using System.Text;

namespace TaurenEngine.EncryptionEx
{
	public class Md5Helper
	{
		public static string ToMd5(string content)
		{
			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			string result = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(content)), 4, 8);
			result = result.Replace("-", "");
			return result.ToLower();
		}
	}
}