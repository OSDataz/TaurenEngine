/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/27 20:25:50
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Framework
{
	public class EventDispatcher<T>
	{
		#region 对象池
		internal readonly ObjectPool<LoopList<Event>> EventListPool = new ObjectPool<LoopList<Event>>();
		#endregion

		#region 事件数据
		internal readonly Dictionary<T, LoopList<Event>> events = new Dictionary<T, LoopList<Event>>();
		/// <summary>
		/// 是否有注册的事件
		/// </summary>
		public bool HasEvent => events.Count > 0;
		#endregion

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
				Debug.LogError($"添加事件，回调函数不能为Null。Key：{key}");
				return;
			}

			Event @event = null;
			if (events.TryGetValue(key, out var list))
			{
				@event = list.Find(item => item.Contains(callAction));
			}
			else
			{
				list = EventListPool.Get();
				events.Add(key, list);
			}

			if (@event == null)
			{
				@event = Event.Pool.Get();
				@event.callAction = callAction;
				list.Add(@event);
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
				Debug.LogError($"添加事件，回调函数不能为Null。Key：{key}");
				return;
			}

			Event @event = null;
			if (events.TryGetValue(key, out var list))
			{
				@event = list.Find(item => item.Contains(callAction));
			}
			else
			{
				list = EventListPool.Get();
				events.Add(key, list);
			}

			if (@event == null)
			{
				@event = Event.Pool.Get();
				@event.callParamAction = callAction;
				list.Add(@event);
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

		private void RemoveEvent(ref T key, LoopList<Event> list, Event @event)
		{
			list.Remove(@event);
			@event.Recycle();

			if (list.IsEmpty)
			{
				events.Remove(key);
				EventListPool.Add(list);
			}
		}

		/// <summary>
		/// 删除事件键值所有的注册事件
		/// </summary>
		/// <param name="key">事件键值</param>
		public void RemoveEvent(T key)
		{
			if (!events.TryGetValue(key, out var list))
				return;

			list.ForEach(RemoveEvent);
			list.Clear();

			if (list.IsEmpty)
			{
				events.Remove(key);
				EventListPool.Add(list);
			}
		}

		private void RemoveEvent(Event @event)
		{
			@event.Recycle();
		}
		#endregion

		#region 派发事件
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

			if (async)
			{
				list.ForEach(@event =>
				{
					@event.param = data;

					AddAsyncEvent(@event);

					if (@event.isOnce)
						list.Remove(@event);
				});
			}
			else
			{
				list.ForEach(@event =>
				{
					@event.Execute(data);

					if (@event.isOnce)
					{
						list.Remove(@event);
						@event.Recycle();
					}
				});
			}

			if (list.IsEmpty)
			{
				events.Remove(key);
				EventListPool.Add(list);
			}
		}
		#endregion

		#region 异步事件
		private bool _isAlwaysOpenAsync;
		private readonly List<Event> _asyncEvents = new List<Event>();
		private IFrame _frameUpdate;

		/// <summary>
		/// 是否永久开启异步事件处理。默认不开启。
		/// <para>是否开启主要从性能方面考虑。如果经常有异步事件触发，建议考虑永久开启。</para>
		/// </summary>
		public bool IsAlwaysOpenAsync
		{
			get => _isAlwaysOpenAsync;
			set
			{
				if (_isAlwaysOpenAsync == value)
					return;

				_isAlwaysOpenAsync = value;

				if (_isAlwaysOpenAsync)
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
			@event.isAsyncLock = true;
			_asyncEvents.Add(@event);

			if (!_isAlwaysOpenAsync && _asyncEvents.Count == 1)
				OpenAsyncUpdate();
		}

		private void OpenAsyncUpdate()
		{
			_frameUpdate ??= TaurenFramework.Frame.GetUpdateFrame(OnUpdate);
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

				if (@event.IsRecycle)
					Event.Pool.Add(@event);
				else
					@event.isAsyncLock = false;
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
				kv.Value.ForEach(RemoveEvent);
				EventListPool.Add(kv.Value);
			}
			events.Clear();

			CloseAsyncUpdate();
		}
	}
}