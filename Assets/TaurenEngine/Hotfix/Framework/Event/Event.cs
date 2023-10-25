/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/27 20:26:19
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Hotfix
{
	/// <summary>
	/// 事件对象
	/// </summary>
	internal sealed class Event : PoolObject<Event>, IRefrenceObject
	{
		public int RefCount { get; set; }

		/// <summary> 无参回调函数 </summary>
		public Action callAction;
		/// <summary> 带参回调函数 </summary>
		public Action<object> callParamAction;
		/// <summary> 回调函数参数 </summary>
		public object param;

		/// <summary> 只执行一次 </summary>
		public bool isOnce;

		public bool Contains(Action action)
		{
			if (callAction == null)
				return false;

			return callAction.Equals(action);
		}

		public bool Contains(Action<object> action)
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

		public void Execute(object data)
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