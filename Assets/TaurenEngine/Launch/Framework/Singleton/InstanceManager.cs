/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/31 11:33:41
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace TaurenEngine.Launch
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

		/// <summary>
		/// 清理单例
		/// </summary>
		/// <param name="tag"></param>
		public void ClearInstances(int tag)
		{
			foreach (var kv in _instanceMap)
			{
				if (kv.Value.Tag == tag)
					kv.Value.Clear();
			}
		}

		/// <summary>
		/// 销毁单例
		/// </summary>
		/// <param name="tag"></param>
		public void DestroyInstances(int tag)
		{
			var keys = _instanceMap.Keys.ToArray();
			var len = keys.Length;
			for (int i = 0; i < len; ++i)
			{
				var key = keys[i];
				var instance = _instanceMap[key];
				if (instance.Tag == tag)
				{
					_instanceMap.Remove(key);
					instance.Destroy();
				}
			}
		}
	}
}