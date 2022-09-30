/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/31 11:33:41
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine
{
	/// <summary>
	/// 单例管理器
	/// </summary>
	public class InstanceManager : Singleton<InstanceManager>
	{
		/// <summary>
		/// 实例字典
		/// </summary>
		private readonly Dictionary<Type, InstanceBase> _instanceMap = new Dictionary<Type, InstanceBase>();

		/// <summary>
		/// 获取单例实例对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T Get<T>() where T : InstanceBase, new()
		{
			var type = typeof(T);
			if (!_instanceMap.TryGetValue(type, out var value))
			{
				value = new T();
				_instanceMap.Add(type, value);
			}

			return value as T;
		}
	}
}