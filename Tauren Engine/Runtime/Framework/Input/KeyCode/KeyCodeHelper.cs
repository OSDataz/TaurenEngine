/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/12 17:55:17
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	public static class KeyCodeHelper
	{
		public static void AddEvent(KeyCode key, KeyCodeType type, Action callback, bool isOnce = false)
		{
			IKeyCodeService.Instance.AddEvent(key, type, callback, isOnce);
		}

		public static void RemoveEvent(KeyCode key, KeyCodeType type, Action callback)
		{
			IKeyCodeService.Instance.RemoveEvent(key, type, callback);
		}

		public static void RemoveEvent(KeyCode key, KeyCodeType type)
		{
			IKeyCodeService.Instance.RemoveEvent(key, type);
		}
	}
}