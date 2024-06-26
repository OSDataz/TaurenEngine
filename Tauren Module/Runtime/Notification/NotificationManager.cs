﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/29 8:21:54
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 手机系统消息通知管理
	/// </summary>
	public class NotificationManager : SingletonComponent<NotificationManager>
	{
		private NotificationBase _notification;

		void Awake()
		{
#if UNITY_ANDROID
			_notification = new NotificationAndroid();
#elif UNITY_IOS
            _notification = new NotificationIOS();
#endif

			_notification.ResetNotificationChannel();
		}

		void OnApplicationFocus(bool hasFocus)
		{
			if (hasFocus)
				_notification?.ReSendNotification();
		}

		public void SendNotification(string title, string text)
		{

		}
	}
}