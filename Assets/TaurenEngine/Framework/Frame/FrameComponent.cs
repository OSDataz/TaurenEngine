/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/18 0:03:01
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 帧循环/计时器组件
	/// </summary>
	public sealed class FrameComponent : FrameworkComponent
	{
		protected override void Awake()
		{
			base.Awake();

			TaurenFramework.Frame = this;
		}

		#region 更新循环
		private void Update()
		{
			_timerList.ForEach(RunTimer, true);
			_updateList.ForEach(RunUpdateFrame, true);
		}

		private void FixedUpdate()
		{
			_fixedList.ForEach(RunFixedFrame, true);
		}

		private void RunTimer(Timer timer)
		{
			if (Time.time >= timer.triggerTime)
				timer.Execute();
		}

		private void RunUpdateFrame(UpdateFrame updateFrame)
		{
			updateFrame.callAction.Invoke();
		}

		private void RunFixedFrame(FixedFrame fixedFrame)
		{
			fixedFrame.callAction.Invoke();
		}
		#endregion

		#region 帧列表管理
		internal readonly ObjectPool<FixedFrame> fixedPool = new ObjectPool<FixedFrame>();
		internal readonly ObjectPool<UpdateFrame> updatePool = new ObjectPool<UpdateFrame>();
		internal readonly ObjectPool<Timer> timerPool = new ObjectPool<Timer>();

		private readonly LoopList<FixedFrame> _fixedList = new LoopList<FixedFrame>();
		private readonly LoopList<UpdateFrame> _updateList = new LoopList<UpdateFrame>();
		private readonly LoopList<Timer> _timerList = new LoopList<Timer>();

		internal void AddFrame(FixedFrame frame) => _fixedList.Add(frame);
		internal void AddFrame(UpdateFrame frame) => _updateList.Add(frame);
		internal void AddFrame(Timer frame) => _timerList.Add(frame);

		internal void RemoveFrame(FixedFrame frame) => _fixedList.Remove(frame);
		internal void RemoveFrame(UpdateFrame frame) => _updateList.Remove(frame);
		internal void RemoveFrame(Timer frame) => _timerList.Remove(frame);
		#endregion

		#region 固定物理帧接口
		/// <summary>
		/// 获取物理帧循环对象，需要手动 <c>Start()</c> 开启
		/// <para>间隔时间 <c>Time.fixedDeltaTime</c></para>
		/// </summary>
		/// <param name="updateAction"></param>
		/// <returns></returns>
		public IFrame GetFixedFrame(Action updateAction)
		{
			if (updateAction == null)
			{
				Debug.LogError("FrameComponent.GetFixedFrame updateAction = null");
				return null;
			}

			return _fixedList.Find(item => item.callAction == updateAction) ?? new FixedFrame()
			{
				usePool = false,
				callAction = updateAction
			};
		}

		/// <summary>
		/// 添加物理帧循环
		/// <para>间隔时间 <c>Time.fixedDeltaTime</c></para>
		/// </summary>
		/// <param name="updateAction"></param>
		/// <returns></returns>
		public uint AddFixedFrame(Action updateAction)
		{
			if (updateAction == null)
			{
				Debug.LogError("FrameComponent.AddFixedFrame updateAction = null");
				return 0;
			}

			var frame = _fixedList.Find(item => item.callAction == updateAction);
			frame ??= fixedPool.Get();
			frame.usePool = true;
			frame.callAction = updateAction;
			frame.Start();

			return frame.Id;
		}
		#endregion

		#region 帧循环接口
		/// <summary>
		/// 获取帧循环对象，需要手动 <c>Start()</c> 开启
		/// <para>间隔时间 <c>Time.deltaTime</c></para>
		/// </summary>
		/// <param name="updateAction"></param>
		/// <returns></returns>
		public IFrame GetUpdateFrame(Action updateAction)
		{
			if (updateAction == null)
			{
				Debug.LogError("FrameComponent.GetUpdateFrame updateAction = null");
				return null;
			}

			return _updateList.Find(item => item.callAction == updateAction) ?? new UpdateFrame()
			{
				usePool = false,
				callAction = updateAction
			};
		}

		/// <summary>
		/// 添加帧循环
		/// <para>间隔时间 <c>Time.deltaTime</c></para>
		/// </summary>
		/// <param name="updateAction"></param>
		/// <returns></returns>
		public uint AddUpdateFrame(Action updateAction)
		{
			if (updateAction == null)
			{
				Debug.LogError("FrameComponent.AddUpdateFrame updateAction = null");
				return 0;
			}

			var frame = _updateList.Find(item => item.callAction == updateAction);
			frame ??= updatePool.Get();
			frame.usePool = true;
			frame.callAction = updateAction;
			frame.Start();

			return frame.Id;
		}
		#endregion

		#region 延迟调用接口
		/// <summary>
		/// 获取帧循环对象，需要手动 <c>Start()</c> 开启，默认时循环执行
		/// </summary>
		/// <param name="seconds"></param>
		/// <param name="callAction"></param>
		/// <returns></returns>
		public IFrame GetTimer(float seconds, Action callAction)
		{
			if (callAction == null)
			{
				Debug.LogError("FrameComponent.GetTimer updateAction = null");
				return null;
			}

			return new Timer()
			{
				usePool = false,
				interval = seconds,
				isLoop = true,
				callAction = callAction
			};
		}

		/// <summary>
		/// 获取帧循环对象，需要手动 <c>Start()</c> 开启，默认时循环执行
		/// </summary>
		/// <param name="seconds"></param>
		/// <param name="callAction"></param>
		/// <returns></returns>
		public IFrame GetTimer(float seconds, Action<object> callAction, object param)
		{
			if (callAction == null)
			{
				Debug.LogError("FrameComponent.GetTimer updateAction = null");
				return null;
			}

			return new Timer()
			{
				usePool = false,
				interval = seconds,
				isLoop = true,
				callParamAction = callAction,
				param = param
			};
		}

		/// <summary>
		/// 添加计时器
		/// </summary>
		/// <param name="seconds"></param>
		/// <param name="callAction"></param>
		/// <param name="isLoop"></param>
		/// <returns></returns>
		public uint AddTimer(float seconds, Action callAction, bool isLoop = false)
		{
			if (callAction == null)
			{
				Debug.LogError("FrameComponent.AddTimer updateAction = null");
				return 0;
			}

			var timer = timerPool.Get();
			timer.usePool = true;
			timer.interval = seconds;
			timer.isLoop = isLoop;
			timer.callAction = callAction;
			timer.Start();
			return timer.Id;
		}

		/// <summary>
		/// 添加计时器
		/// </summary>
		/// <param name="seconds"></param>
		/// <param name="callAction"></param>
		/// <param name="param"></param>
		/// <param name="isLoop"></param>
		/// <returns></returns>
		public uint AddTimer(float seconds, Action<object> callAction, object param, bool isLoop = false)
		{
			if (callAction == null)
			{
				Debug.LogError("FrameComponent.AddTimer updateAction = null");
				return 0;
			}

			var timer = timerPool.Get();
			timer.usePool = true;
			timer.interval = seconds;
			timer.isLoop = isLoop;
			timer.callParamAction = callAction;
			timer.param = param;
			timer.Start();
			return timer.Id;
		}
		#endregion

		#region 移除接口
		/// <summary>
		/// 删除帧循环/计时器
		/// </summary>
		/// <param name="id"></param>
		public void Remove(uint id)
		{
			var timer = _timerList.Find(item => item.Id == id);
			if (timer != null)
			{
				timer.RecoveryOrDestroy();
				return;
			}

			var updateFrame = _updateList.Find(item => item.Id == id);
			if (updateFrame != null)
			{
				updateFrame.RecoveryOrDestroy();
				return;
			}

			_fixedList.Find(item => item.Id == id)?.RecoveryOrDestroy();
		}

		/// <summary>
		/// 删除帧循环/计时器
		/// </summary>
		/// <param name="updateAction"></param>
		public void Remove(Action updateAction)
		{
			_updateList.Find(item => item.callAction == updateAction)?.RecoveryOrDestroy();
			_fixedList.Find(item => item.callAction == updateAction)?.RecoveryOrDestroy();
		}
		#endregion
	}
}