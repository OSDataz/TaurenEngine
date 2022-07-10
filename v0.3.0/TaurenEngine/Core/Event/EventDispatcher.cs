/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/5 22:35:48
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine.Core
{
	public class EventDispatcher<T>
	{
		internal readonly Dictionary<T, List<Event>> events = new Dictionary<T, List<Event>>();

		public bool HasEvent => events.Count > 0;

		#region 添加事件
		/// <summary>
		/// 添加侦听事件
		/// </summary>
		/// <param name="key">事件键值</param>
		/// <param name="callAction">回调函数</param>
		/// <param name="isOnce">是否只执行一次</param>
		public void AddEvent(T key, Action callAction, bool isOnce = false)
		{
			if (callAction == null)
			{
				TDebug.Error($"添加事件，回调函数不能为Null。Key：{key}");
				return;
			}

			Event @event = null;
			if (events.TryGetValue(key, out var list))
			{
				@event = list.Find(item => item.Contains(callAction));
			}
			else
			{
				list = new List<Event>();
				events.Add(key, list);
			}

			if (@event == null)
			{
				@event = Event.Pool.Get();
				@event.callAction = callAction;
				list.Add(@event);

				if (_executingKey?.Equals(key) ?? false)
					@event.state = EventState.Add;
			}
			else
			{
				if (@event.state == EventState.Remove)
					@event.state = EventState.Normal;
			}

			@event.isOnce = isOnce;
		}

		/// <summary>
		/// 添加侦听事件
		/// </summary>
		/// <typeparam name="TValue">回调函数参数类型</typeparam>
		/// <param name="key">事件键值</param>
		/// <param name="callAction">回调函数</param>
		/// <param name="isOnce">是否只执行一次</param>
		public void AddEvent<TValue>(T key, Action<EventData<TValue>> callAction, bool isOnce = false)
		{
			AddEvent(key, callAction as Action<IEventData>, isOnce);
		}

		/// <summary>
		/// 添加侦听事件
		/// </summary>
		/// <param name="key">事件键值</param>
		/// <param name="callAction">回调函数</param>
		/// <param name="isOnce">是否只执行一次</param>
		public void AddEvent(T key, Action<IEventData> callAction, bool isOnce = false)
		{
			if (callAction == null)
			{
				TDebug.Error($"添加事件，回调函数不能为Null。Key：{key}");
				return;
			}

			Event @event = null;
			if (events.TryGetValue(key, out var list))
			{
				@event = list.Find(item => item.Contains(callAction));
			}
			else
			{
				list = new List<Event>();
				events.Add(key, list);
			}

			if (@event == null)
			{
				@event = Event.Pool.Get();
				@event.callActionWithParam = callAction;
				list.Add(@event);

				if (_executingKey?.Equals(key) ?? false)
					@event.state = EventState.Add;
			}
			else
			{
				if (@event.state == EventState.Remove)
					@event.state = EventState.Normal;
			}

			@event.isOnce = isOnce;
		}
		#endregion

		#region 删除事件
		/// <summary>
		/// 删除侦听事件
		/// </summary>
		/// <param name="key">事件键值</param>
		/// <param name="callAction">回调函数</param>
		public void RemoveEvent(T key, Action callAction)
		{
			if (!events.TryGetValue(key, out var list))
				return;

			Event @event = list.Find(item => item.Contains(callAction));
			if (@event == null)
				return;

			RemoveEvent(ref key, list, @event);
		}

		/// <summary>
		/// 删除侦听事件
		/// </summary>
		/// <typeparam name="TValue">回调函数参数类型</typeparam>
		/// <param name="key">事件键值</param>
		/// <param name="callAction">回调函数</param>
		public void RemoveEvent<TValue>(T key, Action<EventData<TValue>> callAction)
		{
			RemoveEvent(key, callAction as Action<IEventData>);
		}

		/// <summary>
		/// 删除侦听事件
		/// </summary>
		/// <param name="key">事件键值</param>
		/// <param name="callAction">回调函数</param>
		public void RemoveEvent(T key, Action<IEventData> callAction)
		{
			if (!events.TryGetValue(key, out var list))
				return;

			Event @event = list.Find(item => item.Contains(callAction));
			if (@event == null)
				return;

			RemoveEvent(ref key, list, @event);
		}

		/// <summary>
		/// 删除事件键值所有的注册事件
		/// </summary>
		/// <param name="key">事件键值</param>
		public void RemoveEvent(T key)
		{
			if (!events.TryGetValue(key, out var list))
				return;

			if (_executingKey?.Equals(key) ?? false)
			{
				foreach (var @event in list)
					@event.state = EventState.Remove;
			}
			else
			{
				foreach (var @event in list)
				{
					if (@event.asyncLock)
						@event.state = EventState.Remove;
					else
						Event.Pool.Add(@event);
				}

				list.Clear();
				events.Remove(key);
			}
		}

		private void RemoveEvent(ref T key, List<Event> list, Event @event)
		{
			if (_executingKey?.Equals(key) ?? false)
			{
				@event.state = EventState.Remove;
			}
			else
			{
				if (list.Count == 1)
					events.Remove(key);
				else
					list.Remove(@event);

				if (@event.asyncLock)
					@event.state = EventState.Remove;
				else
					Event.Pool.Add(@event);
			}
		}
		#endregion

		#region 派发事件
		/// <summary> 当前执行的key </summary>
		private T _executingKey;

		/// <summary>
		/// 派发无参事件，默认异步执行。
		/// </summary>
		/// <param name="key">事件键值</param>
		public void Send(T key) => Send(key, null, true);
		/// <summary>
		/// 派发带参事件，默认异步执行。
		/// </summary>
		/// <param name="key">事件键值</param>
		/// <param name="data">事件回调参数</param>
		public void Send(T key, IEventData data) => Send(key, data, true);
		/// <summary>
		/// 派发无参事件。
		/// </summary>
		/// <param name="key"></param>
		/// <param name="async">是否异步执行</param>
		public void Send(T key, bool async) => Send(key, null, async);
		/// <summary>
		/// 派发带参事件。
		/// </summary>
		/// <param name="key">事件键值</param>
		/// <param name="data">事件回调参数</param>
		/// <param name="async">是否异步执行</param>
		public void Send(T key, IEventData data, bool async)
		{
			if (!events.TryGetValue(key, out var list))
				return;

			_executingKey = key;

			var length = list.Count;
			if (async)
			{
				for (int i = 0; i < length; ++i)
				{
					var @event = list[i];
					@event.param = data;

					AddAsyncEvent(@event);

					if (@event.isOnce)
						@event.state = EventState.Remove;
				}
			}
			else
			{
				for (int i = 0; i < length; ++i)
				{
					var @event = list[i];
					@event.Execute(data);

					if (@event.isOnce)
						@event.state = EventState.Remove;
				}
			}

			_executingKey = default(T);

			for (int i = list.Count - 1; i >= 0; --i)
			{
				var @event = list[i];
				if (@event.state == EventState.Add)
					@event.state = EventState.Normal;
				else if (@event.state == EventState.Remove)
				{
					if (list.Count == 1)
						events.Remove(key);
					else
						list.Remove(@event);

					if (!@event.asyncLock)
						Event.Pool.Add(@event);
				}
			}
		}
		#endregion

		#region 异步事件
		private bool _isPermanentlyOpenAsync;
		private readonly List<Event> _asyncEvents = new List<Event>();
		private IFrameUpdate _frameUpdate;

		/// <summary>
		/// 是否永久开启异步事件处理。默认不开启。
		/// <para>是否开启主要从性能方面考虑。如果经常有异步事件触发，建议考虑永久开启。</para>
		/// </summary>
		public bool IsPermanentlyOpenAsync
		{
			get => _isPermanentlyOpenAsync;
			set
			{
				if (_isPermanentlyOpenAsync == value)
					return;

				_isPermanentlyOpenAsync = value;

				if (_isPermanentlyOpenAsync)
				{
					OpenAsyncUpdate();
				}
				else
				{
					if (_asyncEvents.Count == 0)
						CloseAsyncUpdate();
				}
			}
		}

		private void AddAsyncEvent(Event @event)
		{
			@event.asyncLock = true;
			_asyncEvents.Add(@event);

			if (!_isPermanentlyOpenAsync && _asyncEvents.Count == 1)
				OpenAsyncUpdate();
		}

		private void OpenAsyncUpdate()
		{
			_frameUpdate ??= FrameManager.Instance.GetUpdate(OnUpdate);
			_frameUpdate.Start();
		}

		private void CloseAsyncUpdate()
		{
			_frameUpdate?.Stop();
		}

		private void OnUpdate()
		{
			var length = _asyncEvents.Count;
			if (length == 0)
				return;

			for (int i = 0; i < length; ++i)
			{
				var @event = _asyncEvents[i];
				@event.Execute();

				if (@event.state == EventState.Remove)
					Event.Pool.Add(@event);
				else
					@event.asyncLock = false;
			}

			if (_asyncEvents.Count == length)
				_asyncEvents.Clear();
			else
				_asyncEvents.RemoveRange(0, length);
		}
		#endregion

		public void Destroy()
		{
			foreach (var kv in events)
			{
				kv.Value.ForEach(item => Event.Pool.Add(item));
			}
			events.Clear();

			CloseAsyncUpdate();
		}
	}
}