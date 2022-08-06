/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/25 10:14:09
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine
{
	public static class TaurenFramework
	{
		public const string Version = "0.7.0";

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
				DebugEx.Error($"重复添加框架组件：{component}");
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