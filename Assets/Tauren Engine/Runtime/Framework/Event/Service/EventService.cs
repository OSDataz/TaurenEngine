/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/2 17:58:21
 *└────────────────────────┘*/

using System;

namespace Tauren.Framework.Runtime
{
	public class EventService : IEventService
	{
		private readonly EventDispatcher<string> _eventDispatcher = new EventDispatcher<string>();

		public EventService()
		{
			this.InitInterface();
		}

		public void ListenEvent(string key, Action callAction, bool isOnce = false)
		{
			_eventDispatcher.ListenEvent(key, callAction, isOnce);
		}

		public void ListenEvent(string key, Action<object> callAction, bool isOnce = false)
		{
			_eventDispatcher.ListenEvent(key, callAction, isOnce);
		}

		public void ListenEvent<TData>(string key, Action<TData> callAction, bool isOnce = false)
		{
			_eventDispatcher.ListenEvent(key, callAction, isOnce);
		}

		public void RemoveEvent(string key, Action callAction)
		{
			_eventDispatcher.RemoveEvent(key, callAction);
		}

		public void RemoveEvent(string key, Action<object> callAction)
		{
			_eventDispatcher.RemoveEvent(key, callAction);
		}

		public void RemoveEvent(string key)
		{
			_eventDispatcher.RemoveEvent(key);
		}

		public void TriggerEvent(string key, object data = null, bool async = false)
		{
			_eventDispatcher.TriggerEvent(key, data, async);
		}

		public bool IsAlwaysOpenAsync 
		{
			get => _eventDispatcher.IsAlwaysOpenAsync;
			set => _eventDispatcher.IsAlwaysOpenAsync = value;
		}
	}
}