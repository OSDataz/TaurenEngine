/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/7 20:20:47
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
	public static class PlayerPrefsUtils
	{
		/// <summary>
		/// 保存Float数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SaveFloat(string key, float value)
		{
			PlayerPrefs.SetFloat(key, value);
			PlayerPrefs.Save();
		}

		/// <summary>
		/// 保存Int数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SaveInt(string key, int value)
		{
			PlayerPrefs.SetInt(key, value);
			PlayerPrefs.Save();
		}

		/// <summary>
		/// 保存String数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SaveString(string key, string value)
		{
			PlayerPrefs.SetString(key, value);
			PlayerPrefs.Save();
		}

		/// <summary>
		/// 获取Bool数据
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static bool GetBool(string key)
		{
			return PlayerPrefs.GetInt(key) != 0;
		}

		/// <summary>
		/// 获取Bool数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static bool GetBool(string key, bool defaultValue)
		{
			return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) != 0;
		}

		/// <summary>
		/// 设置Bool数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SetBool(string key, bool value)
		{
			PlayerPrefs.SetInt(key, value ? 1 : 0);
		}

		/// <summary>
		/// 保存Bool数据
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void SaveBool(string key, bool value)
		{
			SetBool(key, value);
			PlayerPrefs.Save();
		}
	}
}