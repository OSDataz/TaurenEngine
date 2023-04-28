/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/26 23:20:15
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Unity
{
	public enum TimerType
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

	public partial class Timer : PoolObject<Timer>, IRefrenceObject
	{
		public int RefCount { get; set; }

		/// <summary> 计时器类型 </summary>
		public TimerType Type { get; private set; }
		/// <summary> 间隔时间(s) </summary>
		public float Interval { get; private set; }
		/// <summary> 触发时间(s) </summary>
		internal float TriggerTime { get; private set; }
		/// <summary> 是否间隔时间执行（用于提升效率） summary>
		internal bool IsInterval { get; set; }
		/// <summary> 是否循环 </summary>
		public bool IsLoop { get; private set; }

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


			if (Type == TimerType.Update || Type == TimerType.UpdateInterval)
			{
				TriggerTime = Time.time + Interval;
				UpdateList.Add(this);
			}
			else if (Type == TimerType.LateUpdate || Type == TimerType.LateUpdateInterval)
			{
				TriggerTime = Time.time + Interval;
				LateUpdateList.Add(this);
			}
			else if (Type == TimerType.FixedUpdate || Type == TimerType.FixedUpdateInterval)
			{
				TriggerTime = Time.fixedTime + Interval;
				FixedUpdateList.Add(this);
			}
			else
				Log.Error($"Timer类型异常：{Type}");
		}

		/// <summary>
		/// 停止运行（切勿直接调用 Destroy）
		/// </summary>
		public void Stop()
		{
			if (!IsRunning)
				return;

			IsRunning = false;

			if (Type == TimerType.Update || Type == TimerType.UpdateInterval)
				UpdateList.Remove(this);
			else if (Type == TimerType.LateUpdate || Type == TimerType.LateUpdateInterval)
				LateUpdateList.Remove(this);
			else if (Type == TimerType.FixedUpdate || Type == TimerType.FixedUpdateInterval)
				FixedUpdateList.Remove(this);
			else
				Log.Error($"Timer类型异常：{Type}");
		}

		/// <summary>
		/// 每帧执行
		/// </summary>
		internal void Execute()
		{
			if (callAction != null)
				callAction.Invoke();
			else if (callParamAction != null)
				callParamAction.Invoke(param);

			if (!IsLoop)
				Stop();
		}

		/// <summary>
		/// 间隔时间执行
		/// </summary>
		internal void ExecuteInterval()
		{
			if (callAction != null)
				callAction.Invoke();
			else if (callParamAction != null)
				callParamAction.Invoke(param);

			if (IsLoop)
			{
				if (Type == TimerType.FixedUpdate || Type == TimerType.FixedUpdateInterval)
					TriggerTime = Time.fixedTime + Interval;
				else
					TriggerTime = Time.time + Interval;
			}
			else
				Stop();
		}
	}
}