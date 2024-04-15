/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/12 17:58:35
 *└────────────────────────┘*/

using System.Collections.Generic;
using System;
using Tauren.Core.Runtime;
using UnityEngine;

namespace Tauren.Framework.Runtime
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

	public interface ITouchService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static ITouchService Instance { get; internal set; }

		void AddEvent(FingerTouchType type, Action<List<Touch>> callback, bool isOnce = false);

		void RemoveEvent(FingerTouchType type);
	}

	public static class ITouchServiceExtension
	{
		public static void InitInterface(this ITouchService @object)
		{
			ITouchService.Instance = @object;
		}
	}
}