﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/27 20:25:50
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public class EventDispatcher<T> : RefrenceObject, IRecycle
	{
		/// <summary>
		/// 事件字典
		/// </summary>
		internal readonly RefrenceMap<T, EventList> events;
		/// <summary>
		/// 是否有注册的事件
		/// </summary>
		public bool HasEvent => events.Count > 0;

		public EventDispatcher()
		{
			events = new RefrenceMap<T, EventList>();
		}

		#region 添加事件
		/// <summary>
		/// 添加侦听事件
		/// </summary>
		/// <param name="key">事件键值</param>
		/// <param name="callAction">回调函数</param>
		/// <param name="isOnce">是否只执行一次</param>
		public void ListenEvent(T key, Action callAction, bool isOnce = false)
		{
			if (callAction == null)
			{
				Log.Error($"添加事件，回调函数不能为Null。Key：{key}");
				return;
			}

			Event @event = null;
			if (events.TryGetValue(key, out var list))
			{
				@event = list.Find(item => item.Contains(callAction));
			}
			else
			{
				list = PoolHelper.Get<EventList>();
				events.Add(key, list);
			}

			if (@event == null)
			{
				@event = Event.GetFromPool();
				@event.callAction = callAction;
				list.Add(@event);
			}

			@event.isOnce = isOnce;
		}

		/// <summary>
		/// 添加侦听事件
		/// </summary>
		/// <param name="key">事件键值</param>
		/// <param name="callAction">回调函数</param>
		/// <param name="isOnce">是否只执行一次</param>
		public void ListenEvent(T key, Action<object> callAction, bool isOnce = false)
		{
			if (callAction == null)
			{
				Log.Error($"添加事件，回调函数不能为Null。Key：{key}");
				return;
			}

			Event @event = null;
			if (events.TryGetValue(key, out var list))
			{
				@event = list.Find(item => item.Contains(callAction));
			}
			else
			{
				list = PoolHelper.Get<EventList>();
				events.Add(key, list);
			}

			if (@event == null)
			{
				@event = Event.GetFromPool();
				@event.callParamAction = callAction;
				list.Add(@event);
			}

			@event.isOnce = isOnce;
		}

		/// <summary>
		/// 添加侦听事件（注意：该接口不支持删除指定回调）
		/// </summary>
		/// <typeparam name="TData">回调函数参数类型</typeparam>
		/// <param name="key">事件键值</param>
		/// <param name="callAction">回调函数</param>
		/// <param name="isOnce">是否只执行一次</param>
		public void ListenEvent<TData>(T key, Action<TData> callAction, bool isOnce = false)
		{
			if (callAction == null)
			{
				Log.Error($"添加事件，回调函数不能为Null。Key：{key}");
				return;
			}

			ListenEvent(key, data => 
			{
				if (data is TData tData)
					callAction.Invoke(tData);
				else
					callAction.Invoke(default(TData));
			}, isOnce);
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
		/// <param name="key">事件键值</param>
		/// <param name="callAction">回调函数</param>
		public void RemoveEvent(T key, Action<object> callAction)
		{
			if (!events.TryGetValue(key, out var list))
				return;

			Event @event = list.Find(item => item.Contains(callAction));
			if (@event == null)
				return;

			RemoveEvent(ref key, list, @event);
		}

		private void RemoveEvent(ref T key, EventList list, Event @event)
		{
			list.Remove(@event);

			if (list.IsEmptyReal)
			{
				events.Remove(key);
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

			list.Clear();

			if (list.IsEmptyReal)
			{
				events.Remove(key);
			}
		}
		#endregion

		#region 派发事件
		/// <summary>
		/// 派发带参事件。
		/// </summary>
		/// <param name="key">事件键值</param>
		/// <param name="data">事件回调参数</param>
		/// <param name="async">是否异步执行（下一帧执行）</param>
		public void TriggerEvent(T key, object data = null, bool async = false)
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
						list.Remove(@event);
				});
			}

			if (list.IsEmptyReal)
			{
				events.Remove(key);
			}
		}
		#endregion

		#region 异步事件
		private bool _isAlwaysOpenAsync = false;
		private readonly EventList _asyncEvents = new EventList();
		private ITimer _timer;

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
					if (_asyncEvents.IsEmptyLogic)
						CloseAsyncUpdate();
				}
			}
		}

		private void AddAsyncEvent(Event @event)
		{
			_asyncEvents.Add(@event);

			if (!_isAlwaysOpenAsync && _asyncEvents.LengthLogic == 1)
				OpenAsyncUpdate();
		}

		private void OpenAsyncUpdate()
		{
			if (_timer == null)
				_timer = TimerHelper.Create(OnUpdate, true);
			else
				_timer.Start();
		}

		private void CloseAsyncUpdate()
		{
			_timer?.Stop();
		}

		private void DestroyAsyncUpdate()
		{
			if (_timer == null)
				return;

			_timer.Stop();
		}

		private void OnUpdate()
		{
			var length = _asyncEvents.LengthReal;
			if (length == 0)
				return;

			_asyncEvents.ForEach(@event =>
			{
				@event.Execute();
			});

			if (_asyncEvents.LengthReal == length)
			{
				_asyncEvents.Clear();

				if (!_isAlwaysOpenAsync)
					CloseAsyncUpdate();
			}
			else
				_asyncEvents.RemoveRange(0, length);
		}
		#endregion

		public virtual void Clear()
		{
			events.Clear();

			_asyncEvents.Clear();

			if (!_isAlwaysOpenAsync)
				CloseAsyncUpdate();
		}

		protected override void OnDestroy()
		{
			Clear();

			DestroyAsyncUpdate();
		}
	}
}