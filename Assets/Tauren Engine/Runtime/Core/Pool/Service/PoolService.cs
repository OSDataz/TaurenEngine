/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/9/8 10:13:51
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace Tauren.Core.Runtime
{
	/// <summary>
	/// 超级对象池，支持各种类型的对象添加和取出
	/// </summary>
	public sealed class PoolService : IPoolService
	{
		private readonly Dictionary<Type, IPool> _map = new Dictionary<Type, IPool>();

		public PoolService()
		{
			this.InitInterface();
		}

		public ObjectPool<T> GetPool<T>() where T : IRecycle, new()
		{
			Type type = typeof(T);
			if (_map.TryGetValue(type, out var pool))
			{
				if (pool is ObjectPool<T> oPool)
					return oPool;

				Log.Error($"{typeof(T)}已有同类型对象池，类型：{pool.GetType()}");
				return null;
			}

			var nPool = new ObjectPool<T>();
			_map.Add(type, nPool);
			return nPool;
		}

		public TypePool GetPool(Type type)
		{
			if (_map.TryGetValue(type, out var pool))
			{
				if (pool is TypePool tPool)
					return tPool;

				Log.Error($"{type}已有同类型对象池，类型：{pool.GetType()}");
				return null;
			}

			var nPool = new TypePool(type);
			_map.Add(type, nPool);
			return nPool;
		}

		private IPool GetIPool(Type type)
		{
			if (!_map.TryGetValue(type, out var pool))
			{
				pool = new TypePool(type);
				_map[type] = pool;
			}

			return pool;
		}

		public T Get<T>() where T : IRecycle, new()
		{
			return (T)GetIPool(typeof(T)).GetItem();
		}

		public void Recycle<T>(T item) where T : IRecycle, new()
		{
			GetIPool(typeof(T)).Recycle(item);
		}

		public void SetPoolSize(Type type, int size)
		{
			GetIPool(type).Maximum = size;
		}

		public void ClearPool(Type type)
		{
			if (!_map.TryGetValue(type, out var pool))
				return;

			pool.Clear();
		}

		public void DestroyPool(Type type)
		{
			if (!_map.TryGetValue(type, out var pool))
				return;

			pool.Destroy();

			_map.Remove(type);
		}
	}
}