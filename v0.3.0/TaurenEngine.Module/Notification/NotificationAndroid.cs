/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:21:15
 *└────────────────────────┘*/

using Unity.Notifications.Android;

namespace TaurenEngine.Notification
{
	internal class NotificationAndroid : NotificationBase
	{
		public override void ResetNotificationChannel()
		{
			base.ResetNotificationChannel();

			AndroidNotificationCenter.CancelAllNotifications();// 清除上次注册的通知
			RegisterChannel("Default");
		}

		private void RegisterChannel(string channelName)
		{
			var channel = new AndroidNotificationChannel()
			{
				Id = $"channel_{channelName}",
				Name = $"{channelName} Channel",
				Description = $"Generic notifications {channelName}",
				Importance = Importance.High,
				CanBypassDnd = false,
				CanShowBadge = true,
				EnableLights = true,
				EnableVibration = true,
				LockScreenVisibility = LockScreenVisibility.Public
			};

			AndroidNotificationCenter.RegisterNotificationChannel(channel);
		}

		public override void SendNotification(NotificationInfo info)
		{
			base.SendNotification(info);

			var notification = new AndroidNotification()
			{
				Number = notificationId,
				Title = info.title,
				Text = info.text,
				FireTime = info.dateTime,
				SmallIcon = info.smallIcon,
				LargeIcon = info.largeIcon
			};

			// 安排一个通知，该通知将在通知结构中指定的时间显示。
			// 返回的ID以后可以在触发通知之前用于更新通知，可以使用CheckScheduledNotificationStatus跟踪其当前状态。
			AndroidNotificationCenter.SendNotification(notification, info.channelId);
		}
	}
}