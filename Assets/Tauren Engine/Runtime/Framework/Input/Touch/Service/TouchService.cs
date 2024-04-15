/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/12 17:58:42
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	public class TouchService : ITouchService
	{
		public TouchService()
		{
			this.InitInterface();
		}

		#region 事件管理
		private readonly EventDispatcher<FingerTouchType> _touchDispatcher = new EventDispatcher<FingerTouchType>();

		public void AddEvent(FingerTouchType type, Action<List<Touch>> callback, bool isOnce = false)
		{
			_touchDispatcher.ListenEvent(type, callback, isOnce);
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

		private ITimer _timerUpdate;

		private void CheckUpdate()
		{
			if (_touchDispatcher.HasEvent)
			{
				_timerUpdate ??= ITimerService.Instance.Create(OnUpdate, true, false);
				_timerUpdate.Start();
			}
			else
			{
				_timerUpdate?.Stop();
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
						_touchDispatcher.TriggerEvent(touchs.Key, touchs.Value, false);
						touchs.Value.Clear();
					}
				}
			}
		}
		#endregion
	}
}