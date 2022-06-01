/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/7 0:01:27
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Unity
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