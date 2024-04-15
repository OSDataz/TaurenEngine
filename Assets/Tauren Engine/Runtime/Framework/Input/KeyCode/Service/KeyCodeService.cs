/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/12 16:28:04
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	public class KeyCodeService : IKeyCodeService
	{
		public KeyCodeService()
		{
			this.InitInterface();
		}

		#region 事件管理
		private readonly EventDispatcher<KeyCode> _keyEvent = new EventDispatcher<KeyCode>();
		private readonly EventDispatcher<KeyCode> _keyDownEvent = new EventDispatcher<KeyCode>();
		private readonly EventDispatcher<KeyCode> _keyUpEvent = new EventDispatcher<KeyCode>();
		private readonly EventDispatcher<KeyCode> _keyDoubleEvent = new EventDispatcher<KeyCode>();

		private EventDispatcher<KeyCode> GetEventDispatcher(KeyCodeType type)
		{
			if (type == KeyCodeType.KeyDown) return _keyDownEvent;
			if (type == KeyCodeType.KeyUp) return _keyUpEvent;
			if (type == KeyCodeType.KeyDouble) return _keyDoubleEvent;
			return _keyEvent;
		}

		public void AddEvent(KeyCode key, KeyCodeType type, Action callback, bool isOnce = false)
		{
			GetEventDispatcher(type).ListenEvent(key, callback, isOnce);
			CheckUpdate();
		}

		public void RemoveEvent(KeyCode key, KeyCodeType type, Action callback)
		{
			GetEventDispatcher(type).RemoveEvent(key, callback);
			CheckUpdate();
		}

		public void RemoveEvent(KeyCode key, KeyCodeType type)
		{
			GetEventDispatcher(type).RemoveEvent(key);
			CheckUpdate();
		}
		#endregion

		#region 键盘按键
		private ITimer _timerUpdate;

		private void CheckUpdate()
		{
			if (_keyEvent.HasEvent || _keyDownEvent.HasEvent || _keyUpEvent.HasEvent || _keyDoubleEvent.HasEvent)
			{
				_timerUpdate ??= ITimerService.Instance.Create(OnUpdate, true, false);
				_timerUpdate.Start();
			}
			else
			{
				_timerUpdate?.Stop();
			}
		}

		private KeyCode _lastKeyCode;
		private float _lastTime;

		private void OnUpdate()
		{
			foreach (var kv in _keyEvent.events)
			{
				if (Input.GetKey(kv.Key))
				{
					_keyEvent.TriggerEvent(kv.Key, null, false);
				}
			}

			foreach (var kv in _keyDownEvent.events)
			{
				if (Input.GetKeyDown(kv.Key))
				{
					_keyDownEvent.TriggerEvent(kv.Key, null, false);
				}
			}

			foreach (var kv in _keyUpEvent.events)
			{
				if (Input.GetKeyUp(kv.Key))
				{
					_keyUpEvent.TriggerEvent(kv.Key, null, false);
				}
			}

			foreach (var kv in _keyDoubleEvent.events)
			{
				if (Input.GetKey(kv.Key))
				{
					if (_lastKeyCode == kv.Key && Time.time - _lastTime < 0.2f)
						_keyDoubleEvent.TriggerEvent(kv.Key, null, false);

					_lastKeyCode = kv.Key;
					_lastTime = Time.time;

					break;// 同时点多个按键不能触发
				}
			}
		}
		#endregion
	}
}