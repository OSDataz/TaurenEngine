/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/10/18 14:05:32
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
	public class PlayerPrefsEx : PlayerPrefs
	{
		/// <summary>
		/// 保存Float数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SaveFloat(string key, float value)
		{
			SetFloat(key, value);
			Save();
		}

		/// <summary>
		/// 保存Int数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SaveInt(string key, int value)
		{
			SetInt(key, value);
			Save();
		}

		/// <summary>
		/// 保存String数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SaveString(string key, string value)
		{
			SetString(key, value);
			Save();
		}
	}
}