/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/27 20:26:19
 *└────────────────────────┘*/

using System;

namespace TaurenEngine
{
	/// <summary>
	/// 事件对象
	/// </summary>
	internal sealed class Event : PoolObject<Event>, IRefObject
	{
		public Action callAction;
		public Action<IEventData> callParamAction;
		public IEventData param;

		/// <summary> 只执行一次 </summary>
		public bool isOnce;

		public int RefCount { get; set; }

		public bool Contains(Action action)
		{
			if (callAction == null)
				return false;

			return callAction.Equals(action);
		}

		public bool Contains(Action<IEventData> action)
		{
			if (callParamAction == null)
				return false;

			return callParamAction.Equals(action);
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

		public override void Clear()
		{
			callAction = null;
			callParamAction = null;
			param = null;
			isOnce = false;
		}

		public override void OnDestroy()
		{
			callAction = null;
			callParamAction = null;
			param = null;
		}
	}
}