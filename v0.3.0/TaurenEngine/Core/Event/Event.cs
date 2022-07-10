/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/5 22:34:56
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Core
{
	internal sealed class Event : IRecycle
	{
		/// <summary>
		/// 对象池
		/// </summary>
		public static readonly ObjectPool<Event> Pool = new ObjectPool<Event>(50);

		public Action callAction;
		public Action<IEventData> callActionWithParam;
		public IEventData param;

		/// <summary> 只执行一次 </summary>
		public bool isOnce;
		/// <summary> 异步执行锁 </summary>
		public bool asyncLock;
		public EventState state;

		public bool Contains(Action action)
		{
			return callAction?.Equals(action) ?? false;
		}

		public bool Contains(Action<IEventData> action)
		{
			return callActionWithParam?.Equals(action) ?? false;
		}

		public void Execute()
		{
			if (state != EventState.Normal)
				return;

			if (callAction != null)
				callAction.Invoke();
			else if (callActionWithParam != null)
				callActionWithParam.Invoke(param);
		}

		public void Execute(IEventData data)
		{
			if (state != EventState.Normal)
				return;

			callActionWithParam?.Invoke(data);
		}

		public void Clear()
		{
			callAction = null;
			callActionWithParam = null;
			param = null;
			state = EventState.Normal;
			isOnce = false;
			asyncLock = false;
		}

		public void Destroy()
		{
			callAction = null;
			callActionWithParam = null;
			param = null;
		}
	}

	internal enum EventState
	{
		Normal,
		Add,
		Remove
	}
}