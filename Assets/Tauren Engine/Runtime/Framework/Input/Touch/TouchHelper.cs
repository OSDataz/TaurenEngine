/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/12 17:58:22
 *└────────────────────────┘*/

using System.Collections.Generic;
using System;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	public static class TouchHelper
	{
		public static void AddEvent(FingerTouchType type, Action<List<Touch>> callback, bool isOnce = false)
		{
			ITouchService.Instance.AddEvent(type, callback, isOnce);
		}

		public static void RemoveEvent(FingerTouchType type)
		{
			ITouchService.Instance.RemoveEvent(type);
		}
	}
}