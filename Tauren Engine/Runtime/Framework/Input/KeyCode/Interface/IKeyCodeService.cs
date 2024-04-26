/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/12 16:28:11
 *└────────────────────────┘*/

using System;
using Tauren.Core.Runtime;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	public enum KeyCodeType
	{
		Key,
		KeyDown,
		KeyUp,
		KeyDouble
	}

	public interface IKeyCodeService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IKeyCodeService Instance { get; internal set; }

		void AddEvent(KeyCode key, KeyCodeType type, Action callback, bool isOnce = false);

		void RemoveEvent(KeyCode key, KeyCodeType type, Action callback);

		void RemoveEvent(KeyCode key, KeyCodeType type);
	}

	public static class IKeyCodeServiceExtension
	{
		public static void InitInterface(this IKeyCodeService @object)
		{
			IKeyCodeService.Instance = @object;
		}
	}
}