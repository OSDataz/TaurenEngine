/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/7/30 23:52:44
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 【注意】切勿自行创建，请使用FrameManager
	/// </summary>
	internal class Timer : FrameLoop
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
		public Action<object> callActionParam;
		public object param;
		#endregion

		public override void Start()
		{
			if (Running)
				return;

			Running = false;
			triggerTime = Time.time + interval;

			FrameManager.Instance.AddFrame(this);
		}

		public void Execute()
		{
			if (callAction != null)
				callAction.Invoke();
			else if (callActionParam != null)
				callActionParam.Invoke(param);

			if (isLoop)
				triggerTime = Time.time + interval;
			else
				CheckPoolDestroy();
		}

		public override void Clear()
		{
			callActionParam = null;
			param = null;

			base.Clear();
		}

		public override void Destroy()
		{
			callActionParam = null;
			param = null;

			base.Destroy();
		}

		public override void CheckPoolDestroy()
		{
			if (usePool)
				FrameManager.Instance.timerPool.Add(this);
			else
				Destroy();
		}
	}
}