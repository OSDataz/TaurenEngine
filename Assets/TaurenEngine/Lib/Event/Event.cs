/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/27 20:26:19
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;

namespace TaurenEngine.Lib.Event
{
	internal sealed class Event : IRecycle
	{
		/// <summary>
		/// 对象池
		/// </summary>
		public static readonly ObjectPool<Event> Pool = new ObjectPool<Event>(50);

		public Action callAction;
		public Action<IEventData> callParamAction;
		public IEventData param;

		/// <summary> 只执行一次 </summary>
		public bool isOnce;
		/// <summary> 异步执行锁 </summary>
		public bool isAsyncLock { private get; set; }
		/// <summary> 是否需要回收 </summary>
		public bool IsRecycle { get; private set; }

		public bool Contains(Action action)
		{
			return callAction?.Equals(action) ?? false;
		}

		public bool Contains(Action<IEventData> action)
		{
			return callParamAction?.Equals(action) ?? false;
		}

		public void Execute()
		{
			if (callAction != null)
				callAction.Invoke();
			else if (callParamAction != null)
				callParamAction.Invoke(param);
		}

		public void Execute(IEventData data)
		{
			callParamAction?.Invoke(data);
		}

		public void Recycle()
		{
			if (isAsyncLock)
				IsRecycle = true;
			else
				Pool.Add(this);
		}

		public void Clear()
		{
			callAction = null;
			callParamAction = null;
			param = null;
			isOnce = false;
			isAsyncLock = false;
			IsRecycle = false;
		}

		public void Destroy()
		{
			callAction = null;
			callParamAction = null;
			param = null;
		}
	}
}