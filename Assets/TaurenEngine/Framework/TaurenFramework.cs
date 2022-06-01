/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/25 10:14:09
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Framework
{
	public static class TaurenFramework
	{
		/// <summary>
		/// 计时器/帧循环组件
		/// </summary>
		public static FrameComponent Frame { get; internal set; }
		/// <summary>
		/// 全局事件触发器
		/// </summary>
		public static EventDispatcher<string> Event { get; internal set; }
		/// <summary>
		/// 资源管理器
		/// </summary>
		public static ResourceManager Resource { get; internal set; }
		/// <summary>
		/// UI管理器
		/// </summary>
		public static UIManager UI { get; internal set; }

		static TaurenFramework()
		{
			_componentMap = new Dictionary<Type, FrameworkComponent>();
		}

		#region 框架组件
		private static Dictionary<Type, FrameworkComponent> _componentMap;

		internal static void AddComponent<T>(T component) where T : FrameworkComponent
		{
			var type = component.GetType();
			if (_componentMap.ContainsKey(type))
			{
				Debug.LogError($"重复添加框架组件：{component}");
				return;
			}

			_componentMap.Add(type, component);
		}

		internal static void RemoveComponent<T>(T component) where T : FrameworkComponent
		{
			_componentMap.Remove(component.GetType());
		}

		public static T GetComponent<T>() where T : FrameworkComponent
		{
			if (_componentMap.TryGetValue(typeof(T), out var value))
				return (T)value;

			return null;
		}
		#endregion
	}
}