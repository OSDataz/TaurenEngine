/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/23 16:30:03
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Core
{
	public enum FingerTouchType
	{
		Began,
		Ended,
		MoveLeft,
		MoveRight,
		MoveUp,
		MoveDown,
		LongPress
	}

	/// <summary>
	/// 触摸手势输入
	/// </summary>
	public sealed class TouchInput : Singleton<TouchInput>
	{
		#region 事件管理
		private readonly EventDispatcher<FingerTouchType> _touchDispatcher = new EventDispatcher<FingerTouchType>();

		public void AddEvent(FingerTouchType type, Action<IEventData> callback, bool isOnce = false)
		{
			_touchDispatcher.AddEvent(type, callback, isOnce);
			CheckUpdate();
		}

		public void RemoveEvent(FingerTouchType type, Action<IEventData> callback)
		{
			_touchDispatcher.RemoveEvent(type, callback);
			CheckUpdate();
		}

		public void RemoveEvent(FingerTouchType type)
		{
			_touchDispatcher.RemoveEvent(type);
			CheckUpdate();
		}
		#endregion

		#region 触摸按键
		private Dictionary<FingerTouchType, List<Touch>> _touchs = new Dictionary<FingerTouchType, List<Touch>>()
		{
			{FingerTouchType.Began, new List<Touch>()},
			{FingerTouchType.Ended, new List<Touch>()},
			{FingerTouchType.MoveLeft, new List<Touch>()},
			{FingerTouchType.MoveRight, new List<Touch>()},
			{FingerTouchType.MoveUp, new List<Touch>()},
			{FingerTouchType.MoveDown, new List<Touch>()},
			{FingerTouchType.LongPress, new List<Touch>()}
		};

		private readonly TouchEvent _touchEvent = new TouchEvent();
		private IFrameUpdate _frameUpdate;

		private void CheckUpdate()
		{
			if (_touchDispatcher.HasEvent)
			{
				_frameUpdate ??= FrameManager.Instance.GetUpdate(OnUpdate);
				_frameUpdate.Start();
			}
			else
			{
				_frameUpdate?.Stop();
			}
		}

		private void OnUpdate()
		{
			if (Input.touchCount > 0)
			{
				foreach (var touch in Input.touches)
				{
					switch (touch.phase)
					{
						case TouchPhase.Moved:// 触摸移动
							var value = touch.position.x - touch.rawPosition.x;
							if (value > 0) _touchs[FingerTouchType.MoveRight].Add(touch);
							else if (value < 0) _touchs[FingerTouchType.MoveLeft].Add(touch);

							value = touch.position.y - touch.rawPosition.y;
							if (value > 0) _touchs[FingerTouchType.MoveUp].Add(touch);
							else if (value < 0) _touchs[FingerTouchType.MoveDown].Add(touch);
							break;

						case TouchPhase.Began:
							_touchs[FingerTouchType.Began].Add(touch);
							break;

						case TouchPhase.Ended:
							_touchs[FingerTouchType.Ended].Add(touch);
							break;

						case TouchPhase.Stationary:// 滞留不动
							_touchs[FingerTouchType.LongPress].Add(touch);
							break;
					}
				}

				foreach (var touchs in _touchs)
				{
					if (touchs.Value.Count > 0)
					{
						_touchEvent.touchs = touchs.Value;
						_touchDispatcher.Send(touchs.Key, _touchEvent, false);
						touchs.Value.Clear();
					}
				}
			}
		}
		#endregion
	}
}