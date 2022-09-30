/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/7 0:01:27
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine
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

		/// <summary>
		/// 获取Bool数据
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetBool(string key)
		{
			return GetInt(key) != 0;
		}

		/// <summary>
		/// 获取Bool数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static bool GetBool(string key, bool defaultValue)
		{
			return GetInt(key, defaultValue ? 1 : 0) != 0;
		}

		/// <summary>
		/// 设置Bool数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SetBool(string key, bool value)
		{
			SetInt(key, value ? 1 : 0);
		}

		/// <summary>
		/// 保存Bool数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SaveBool(string key, bool value)
		{
			SetBool(key, value);
			Save();
		}
	}
}