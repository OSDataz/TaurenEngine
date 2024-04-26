/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/2 17:58:09
 *└────────────────────────┘*/

using System;

namespace Tauren.Framework.Runtime
{
	public static class EventHelper
	{
		public static void ListenEvent(string key, Action callAction, bool isOnce = false)
		{
			IEventService.Instance.ListenEvent(key, callAction, isOnce);
		}

		public static void ListenEvent(string key, Action<object> callAction, bool isOnce = false)
		{
			IEventService.Instance.ListenEvent(key, callAction, isOnce);
		}

		public static void ListenEvent<TData>(string key, Action<TData> callAction, bool isOnce = false)
		{
			IEventService.Instance.ListenEvent(key, callAction, isOnce);
		}

		public static void RemoveEvent(string key, Action callAction)
		{
			IEventService.Instance.RemoveEvent(key, callAction);
		}

		public static void RemoveEvent(string key, Action<object> callAction)
		{
			IEventService.Instance.RemoveEvent(key, callAction);
		}

		public static void RemoveEvent(string key)
		{
			IEventService.Instance.RemoveEvent(key);
		}

		public static void TriggerEvent(string key, object data = null, bool async = false)
		{
			IEventService.Instance.TriggerEvent(key, data, async);
		}
	}
}