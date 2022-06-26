/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/25 17:14:58
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 【注意】切勿自行创建，请使用 TaurenFramework.Frame
	/// </summary>
	internal class Timer : Frame
	{
		#region 计时参数
		/// <summary> 间隔时间(s) </summary>
		public float interval;
		/// <summary> 触发时间(s) </summary>
		public float triggerTime;
		/// <summary> 是否循环 </summary>
		public bool isLoop;
		#endregion

		#region 带参回调
		public Action<object> callParamAction;
		public object param;
		#endregion

		public override void Start()
		{
			if (Running)
				return;

			Running = true;
			triggerTime = Time.time + interval;

			TaurenFramework.Frame.AddFrame(this);
		}

		public override void Stop()
		{
			if (!Running)
				return;

			Running = false;

			TaurenFramework.Frame.RemoveFrame(this);
		}

		public void Execute()
		{
			if (callAction != null)
				callAction.Invoke();
			else if (callParamAction != null)
				callParamAction.Invoke(param);

			if (isLoop)
				triggerTime = Time.time + interval;
			else
				RecoveryOrDestroy();
		}

		public override void Clear()
		{
			callParamAction = null;
			param = null;

			base.Clear();
		}

		public override void Destroy()
		{
			callParamAction = null;
			param = null;

			base.Destroy();
		}

		public override void RecoveryOrDestroy()
		{
			if (usePool)
				TaurenFramework.Frame.timerPool.Add(this);
			else
				Destroy();
		}
	}
}