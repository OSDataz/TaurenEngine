/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/9/8 10:13:51
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine
{
	/// <summary>
	/// 超级对象池，支持各种类型的对象添加和取出
	/// </summary>
	public sealed class PoolService : IPoolService
	{
		private readonly Dictionary<Type, object> _map = new Dictionary<Type, object>();

		public PoolService()
		{
			this.InitInterface(this);
		}

		public ObjectPool<T> GetPool<T>() where T : IRecycle, new()
		{
			Type type = typeof(T);
			if (_map.TryGetValue(type, out var value))
			{
				if (value is ObjectPool<T> tValue)
					return tValue;

				Debug.LogError($"获取ObjectPool<{type}>对象池类型错误，已有实例：{value}");
				return null;
			}

			var pool = new ObjectPool<T>();
			_map.Add(type, pool);
			return pool;
		}

		public TypePool GetPool(Type type)
		{
			if (_map.TryGetValue(type, out var value))
			{
				if (value is TypePool tValue)
					return tValue;

				Debug.LogError($"获取TypePool.{type}对象池类型错误，已有实例：{value}");
				return null;
			}

			var pool = new TypePool(type);
			_map.Add(type, pool);
			return pool;
		}

		private bool TryGetPool<T>(out ObjectPool<T> pool) where T : IRecycle, new()
		{
			Type type = typeof(T);
			if (_map.TryGetValue(type, out var value) && value is ObjectPool<T>)
			{
				pool = (ObjectPool<T>)value;
				return true;
			}

			pool = null;
			return false;
		}

		public bool Add<T>(T item) where T : IRecycle, new()
		{
			return GetPool<T>().Add(item);
		}

		public T Get<T>() where T : IRecycle, new()
		{
			return GetPool<T>().Get();
		}

		public bool Contains<T>(T item) where T : IRecycle, new()
		{
			return GetPool<T>().Contains(item);
		}

		public void SetMaximum<T>(int maximum) where T : IRecycle, new()
		{
			GetPool<T>().Maximum = maximum;
		}

		public void ClearPool<T>() where T : IRecycle, new()
		{
			if (TryGetPool<T>(out var pool))
				pool.Clear();
		}

		public void DestroyPool<T>() where T : IRecycle, new()
		{
			if (TryGetPool<T>(out var pool))
			{
				pool.Destroy();
				_map.Remove(typeof(T));
			}
		}
	}
}