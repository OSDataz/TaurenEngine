/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/19 10:30:17
 *└────────────────────────┘*/

using UnityEngine;

#if UNITY_IOS || UNITY_IPHONE
using System.Runtime.InteropServices;
#endif

namespace TaurenEngine.Core
{
	public static class SystemHelper
	{
		#region 获取设备机器码
#if UNITY_IOS || UNITY_IPHONE
        [DllImport("__Internal")]
        private static extern string DeviceUniqueId();
#endif
		/// <summary>
		/// 获取设备机器码
		///
		/// Android 资料来源 https://stackoverflow.com/questions/2785485/is-there-a-unique-android-device-id/5626208#5626208
		/// iOS  资料来源于 支付宝使用的 SSKeyChain 钥匙串存储
		/// </summary>
		/// <returns></returns>
		public static string GetDeviceUniqueIdentifier()
		{
			string key = "DeviceUniqueIdentifierID";
			string id = PlayerPrefsEx.GetString(key);
			if (string.IsNullOrEmpty(id) || id == "null" || id.Length < 4)
			{
#if UNITY_EDITOR || UNITY_STANDALONE
				id = SystemInfo.deviceUniqueIdentifier;
#elif UNITY_IOS || UNITY_IPHONE
				id = DeviceUniqueId();
#elif UNITY_ANDROID
                AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.alianhome.deviceuniqueidentifier.MainActivity");
                id = androidJavaObject.CallStatic<string>("DeviceUniqueIdentifier");
#else
				id = SystemInfo.deviceUniqueIdentifier;
#endif
				id = id.Replace("-", "");
				id = id.Substring(0, 32);
				PlayerPrefsEx.SaveString(key, id);
			}

			return id;
		}
		#endregion
	}
}