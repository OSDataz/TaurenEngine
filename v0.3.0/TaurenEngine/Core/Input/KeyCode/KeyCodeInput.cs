/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/23 16:29:52
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine.Core
{
	public enum KeyCodeType
	{
		Key,
		KeyDown,
		KeyUp
	}

	/// <summary>
	/// 键盘按键输入
	/// </summary>
	public sealed class KeyCodeInput : Singleton<KeyCodeInput>
	{
		#region 事件管理
		private readonly EventDispatcher<KeyCode> _keyEvent = new EventDispatcher<KeyCode>();
		private readonly EventDispatcher<KeyCode> _keyDownEvent = new EventDispatcher<KeyCode>();
		private readonly EventDispatcher<KeyCode> _keyUpEvent = new EventDispatcher<KeyCode>();

		private EventDispatcher<KeyCode> GetEventDispatcher(KeyCodeType type)
		{
			if (type == KeyCodeType.KeyDown)
				return _keyDownEvent;

			if (type == KeyCodeType.KeyUp)
				return _keyUpEvent;

			return _keyEvent;
		}

		public void AddEvent(KeyCodeType type, KeyCode key, Action<IEventData> callback, bool isOnce = false)
		{
			GetEventDispatcher(type).AddEvent(key, callback, isOnce);
			CheckUpdate();
		}

		public void RemoveEvent(KeyCodeType type, KeyCode key, Action<IEventData> callback)
		{
			GetEventDispatcher(type).RemoveEvent(key, callback);
			CheckUpdate();
		}

		public void RemoveEvent(KeyCodeType type, KeyCode key)
		{
			GetEventDispatcher(type).RemoveEvent(key);
			CheckUpdate();
		}
		#endregion

		#region 键盘按键
		private readonly KeyCodeEvent _keyCodeEvent = new KeyCodeEvent();
		private IFrameUpdate _frameUpdate;

		private void CheckUpdate()
		{
			if (_keyEvent.HasEvent || _keyDownEvent.HasEvent || _keyUpEvent.HasEvent)
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
			_keyCodeEvent.time += Time.deltaTime;

			foreach (var key in _keyEvent.events)
			{
				if (Input.GetKey(key.Key))
				{
					_keyCodeEvent.keyCode = key.Key;
					_keyEvent.Send(key.Key, _keyCodeEvent, false);
				}
			}

			foreach (var key in _keyDownEvent.events)
			{
				if (Input.GetKeyDown(key.Key))
				{
					_keyCodeEvent.keyCode = key.Key;
					_keyDownEvent.Send(key.Key, _keyCodeEvent, false);
				}
			}

			foreach (var key in _keyUpEvent.events)
			{
				if (Input.GetKeyUp(key.Key))
				{
					_keyCodeEvent.keyCode = key.Key;
					_keyUpEvent.Send(key.Key, _keyCodeEvent, false);
				}
			}
		}
		#endregion
	}
}