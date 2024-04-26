/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/29 8:21:33
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

namespace Tauren.Module.Runtime
{
    internal abstract class NotificationBase
    {
        private List<NotificationInfo> _infos = new List<NotificationInfo>();

        protected int notificationId = 0;

        public virtual void ResetNotificationChannel()
        {
            notificationId = 0;
        }

        public void ReSendNotification()
        {
            if (_infos.Count > 0)
            {
                ResetNotificationChannel();
                _infos.ForEach(info => SendNotification(info));
            }
        }

        public virtual void SendNotification(NotificationInfo info)
        {
            _infos.Add(info);

            notificationId += 1;
        }
    }

    internal class NotificationInfo
    {
        /// <summary> 通知渠道ID </summary>
        public string channelId;

        /// <summary> 标题 </summary>
        public string title;
        /// <summary> 文本 </summary>
        public string text;

        /// <summary> 提醒时间 </summary>
        public DateTime dateTime;

        /// <summary> 重复播放间隔 </summary>
        public TimeSpan? repeatInterval;

        /// <summary> 小图标 </summary>
        public string smallIcon = string.Empty;
        /// <summary> 大图标 </summary>
        public string largeIcon = string.Empty;

        /// <summary> 通知样式 </summary>
        public NotificationStyle style = NotificationStyle.None;

        /// <summary> 标准模板颜色 </summary>
        public Color? color = new Color(0, 0, 0, 0);

        /// <summary> 提示显示数量 </summary>
        public int number = -1;

        /// <summary> 当用户触摸此通知时，它将自动被取消。 </summary>
        public bool autoCancel = false;

        /// <summary> 将通知时间字段显示为秒表而不是时间戳。 </summary>
        public bool usesStopwatch = false;

        /// <summary>
        /// 将此属性设置为使通知成为共享同一密钥的一组通知的一部分。
        /// 分组的通知可能显示在支持此类呈现的设备上的群集或堆栈中。
        /// 仅在Android 7.0（API级别24）及更高版本上可用。
        /// </summary>
        public string group = String.Empty;

        /// <summary>
        /// 将此通知设置为一组通知的组摘要。 需要同时设置“组”属性。
        /// 分组的通知可能显示在支持此类呈现的设备上的群集或堆栈中。
        /// 仅在Android 7.0（API级别24）及更高版本上可用。
        /// </summary>
        public bool groupSummary = false;

        /// <summary>
        /// 设置此通知的组警报行为。 如果此通知组的警报应由其他通知处理，则将此属性设置为静音此通知。
        /// 这仅适用于属于组的通知。 必须在您要静音的所有通知上设置此项。
        /// 仅在Android 8.0（API级别26）及更高版本上可用。
        /// </summary>
        public GroupAlertBehaviours groupAlertBehaviour = GroupAlertBehaviours.GroupAlertAll;

        /// <summary>
        /// 排序键将用于在同一包中的其他通知中对该通知进行排序。
        /// 通知将使用此值按字典顺序排序。
        /// </summary>
        public string sortKey = string.Empty;

        /// <summary>
        /// 使用它来保存与通知有关的任意字符串数据。
        /// </summary>
        public string intentData = string.Empty;

        /// <summary>
        /// 启用它可以在传递通知时在通知上显示时间戳，除非将“ CustomTimestamp”属性设置为“ FireTime”。
        /// </summary>
        public bool showTimestamp = false;

        /// <summary>
        /// 将此设置为显示自定义日期，而不是将通知的“ FireTime”显示为通知的时间戳。
        /// </summary>
        public DateTime customTimestamp;
    }
}