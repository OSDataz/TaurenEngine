/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/26 23:20:15
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine
{
	internal enum TimerType
	{
		/// <summary>
		/// 帧循环
		/// </summary>
		Update,
		/// <summary>
		/// 帧循环，指定间隔时间
		/// </summary>
		UpdateInterval,
		/// <summary>
		/// Late帧循环
		/// </summary>
		LateUpdate,
		/// <summary>
		/// Late帧循环，指定间隔时间
		/// </summary>
		LateUpdateInterval,
		/// <summary>
		/// 固定帧循环
		/// </summary>
		FixedUpdate,
		/// <summary>
		/// 固定帧循环，指定间隔时间
		/// </summary>
		FixedUpdateInterval
	}

	public partial class Timer : PoolObject<Timer>, IRefObject
	{
		public int RefCount { get; set; }

		/// <summary> 计时器类型 </summary>
		internal TimerType type;
		/// <summary> 间隔时间(s) </summary>
		internal float interval;
		/// <summary> 触发时间(s) </summary>
		public float TriggerTime { get; private set; }
		/// <summary> 是否间隔时间执行（用于提升效率） summary>
		public bool IsInterval { get; private set; }
		/// <summary> 是否循环 </summary>
		internal bool isLoop;

		/// <summary> 回调函数 </summary>
		internal Action callAction;
		/// <summary> 回调函数（带参） </summary>
		internal Action<object> callParamAction;
		/// <summary> 回调函数参数 </summary>
		internal object param;

		/// <summary> 是否运行中 </summary>
		public bool IsRunning { get; private set; }

		public override void Clear()
		{
			Stop();

			callAction = null;
			callParamAction = null;
			param = null;
		}

		/// <summary>
		/// 开始运行
		/// </summary>
		public void Start()
		{
			if (IsRunning)
				return;

			IsRunning = true;
			TriggerTime = Time.time + interval;

			if (type == TimerType.Update || type == TimerType.UpdateInterval)
				updateList.Add(this);
			else if (type == TimerType.LateUpdate || type == TimerType.LateUpdateInterval)
				lateUpdateList.Add(this);
			else if (type == TimerType.FixedUpdate || type == TimerType.FixedUpdateInterval)
				fixedUpdateList.Add(this);
			else
				DebugEx.Error($"Timer类型异常：{type}");
		}

		/// <summary>
		/// 停止运行
		/// </summary>
		public void Stop()
		{
			if (!IsRunning)
				return;

			IsRunning = false;

			if (type == TimerType.Update || type == TimerType.UpdateInterval)
				updateList.Remove(this);
			else if (type == TimerType.LateUpdate || type == TimerType.LateUpdateInterval)
				lateUpdateList.Remove(this);
			else if (type == TimerType.FixedUpdate || type == TimerType.FixedUpdateInterval)
				fixedUpdateList.Remove(this);
			else
				DebugEx.Error($"Timer类型异常：{type}");
		}

		/// <summary>
		/// 每帧执行
		/// </summary>
		public void Execute()
		{
			if (callAction != null)
				callAction.Invoke();
			else if (callParamAction != null)
				callParamAction.Invoke(param);

			if (!isLoop)
				Stop();
		}

		/// <summary>
		/// 间隔时间执行
		/// </summary>
		public void ExecuteInterval()
		{
			if (callAction != null)
				callAction.Invoke();
			else if (callParamAction != null)
				callParamAction.Invoke(param);

			if (isLoop)
				TriggerTime = Time.time + interval;
			else
				Stop();
		}
	}
}