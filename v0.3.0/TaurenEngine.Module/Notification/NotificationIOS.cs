/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:21:45
 *└────────────────────────┘*/

using System;
using Unity.Notifications.iOS;

namespace TaurenEngine.Notification
{
	internal class NotificationIOS : NotificationBase
	{
		public override void ResetNotificationChannel()
		{
			notificationId = 1;

			iOSNotificationCenter.ApplicationBadge = 0;
			iOSNotificationCenter.RemoveAllDeliveredNotifications();
			iOSNotificationCenter.RemoveAllScheduledNotifications();
		}

		public override void SendNotification(NotificationInfo info)
		{
			base.SendNotification(info);

			var timeTrigger = new iOSNotificationTimeIntervalTrigger()
			{
				TimeInterval = info.dateTime.Subtract(DateTime.Now),
				Repeats = false
			};

			var notification = new iOSNotification()
			{
				Badge = notificationId,
				Identifier = $"notification_{notificationId}",
				Title = info.title,
				Body = info.text,
				Trigger = timeTrigger,
				ShowInForeground = true,
				ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound | PresentationOption.Badge),
				CategoryIdentifier = "category_a",
				ThreadIdentifier = "thread1"
			};

			iOSNotificationCenter.ScheduleNotification(notification);
		}
	}
}