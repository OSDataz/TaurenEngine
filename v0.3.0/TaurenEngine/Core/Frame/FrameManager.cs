/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/7/28 23:39:13
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine.Core
{
	public sealed class FrameManager : SingletonBehaviour<FrameManager>
	{
		#region 更新循环
		void Update()
		{
			_timers.ForEach(RunTimer);
			_loops.ForEach(RunFrameLoop);
		}

		void FixedUpdate()
		{
			_frames.ForEach(RumFrame);
		}

		private void RunTimer(Timer timer)
		{
			if (Time.time >= timer.triggerTime)
				timer.Execute();
		}

		private void RunFrameLoop(FrameLoop frameLoop)
		{
			frameLoop.callAction.Invoke();
		}

		private void RumFrame(Frame frame)
		{
			frame.callAction.Invoke();
		}
		#endregion

		#region 帧列表管理
		internal readonly ObjectPool<Frame> framePool = new ObjectPool<Frame>();
		internal readonly ObjectPool<FrameLoop> loopPool = new ObjectPool<FrameLoop>();
		internal readonly ObjectPool<Timer> timerPool = new ObjectPool<Timer>();

		private readonly LoopList<Timer> _timers = new LoopList<Timer>();
		private readonly LoopList<FrameLoop> _loops = new LoopList<FrameLoop>();
		private readonly LoopList<Frame> _frames = new LoopList<Frame>();

		internal void AddFrame(Frame frame)
		{
			if (frame is Timer timer) 
				_timers.Add(timer, true);
			else if (frame is FrameLoop frameLoop)
				_loops.Add(frameLoop, true);
			else
				_frames.Add(frame, true);
		}

		internal void RemoveFrame(Frame frame)
		{
			if (frame is Timer timer)
				_timers.Remove(timer);
			else if (frame is FrameLoop frameLoop)
				_loops.Remove(frameLoop);
			else
				_frames.Remove(frame);
		}
		#endregion

		#region 固定物理帧接口
		/// <summary>
		/// 获取物理帧循环对象，需要手动 <c>Start()</c> 开启
		/// <para>间隔时间 <c>Time.fixedDeltaTime</c></para>
		/// </summary>
		/// <param name="updateAction"></param>
		/// <returns></returns>
		public IFrameUpdate GetFixedUpdate(Action updateAction)
		{
			if (updateAction == null)
			{
				TDebug.Error("FrameManager.GetFixedUpdate updateAction = null");
				return null;
			}

			return _frames.Find(item => item.callAction == updateAction) ?? new Frame()
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
		public uint AddFixedUpdate(Action updateAction)
		{
			if (updateAction == null)
			{
				TDebug.Error("FrameManager.AddFixedUpdate updateAction = null");
				return 0;
			}

			var frame = _frames.Find(item => item.callAction == updateAction);
			frame ??= framePool.Get();
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
		public IFrameUpdate GetUpdate(Action updateAction)
		{
			if (updateAction == null)
			{
				TDebug.Error("FrameManager.GetUpdate updateAction = null");
				return null;
			}

			return _loops.Find(item => item.callAction == updateAction) ?? new FrameLoop()
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
		public uint AddUpdate(Action updateAction)
		{
			if (updateAction == null)
			{
				TDebug.Error("FrameManager.AddUpdate updateAction = null");
				return 0;
			}

			var frame = _loops.Find(item => item.callAction == updateAction);
			frame ??= loopPool.Get();
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
		public IFrameUpdate GetTimer(float seconds, Action callAction)
		{
			if (callAction == null)
			{
				TDebug.Error("FrameManager.GetTimer updateAction = null");
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
		public IFrameUpdate GetTimer(float seconds, Action<object> callAction, object param)
		{
			if (callAction == null)
			{
				TDebug.Error("FrameManager.GetTimer updateAction = null");
				return null;
			}

			return new Timer()
			{
				usePool = false,
				interval = seconds,
				isLoop = true,
				callActionParam = callAction,
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
				TDebug.Error("FrameManager.AddTimer updateAction = null");
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
				TDebug.Error("FrameManager.AddTimer updateAction = null");
				return 0;
			}

			var timer = timerPool.Get();
			timer.usePool = true;
			timer.interval = seconds;
			timer.isLoop = isLoop;
			timer.callActionParam = callAction;
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
			var timer = _timers.Find(item => item.Id == id);
			if (timer != null)
			{
				timer.CheckPoolDestroy();
				return;
			}

			var frameLoop = _loops.Find(item => item.Id == id);
			if (frameLoop != null)
			{
				frameLoop.CheckPoolDestroy();
				return;
			}

			_frames.Find(item => item.Id == id)?.CheckPoolDestroy();
		}

		/// <summary>
		/// 删除帧循环/计时器
		/// </summary>
		/// <param name="updateAction"></param>
		public void Remove(Action updateAction)
		{
			_loops.Find(item => item.callAction == updateAction)?.CheckPoolDestroy();
			_frames.Find(item => item.callAction == updateAction)?.CheckPoolDestroy();
		}
		#endregion
	}
}