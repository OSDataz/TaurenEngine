/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:21:54
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.Notification
{
	/// <summary>
	/// 手机系统消息通知管理
	/// </summary>
	public class NotificationManager : SingletonBehaviour<NotificationManager>
	{
		private NotificationBase _notification;

		void Awake()
		{
#if UNITY_ANDROID
			_notification = new NotificationAndroid();
#else
            _notification = new NotificationIOS();
#endif

			_notification.ResetNotificationChannel();
		}

		void OnApplicationFocus(bool hasFocus)
		{
			if (hasFocus)
				_notification.ReSendNotification();
		}

		public void SendNotification(string title, string text)
		{

		}
	}
}