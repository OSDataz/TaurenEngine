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

		public IPool GetPool<T>() where T : IRecycle, new()
		{
			Type type = typeof(T);
			if (_map.TryGetValue(type, out var pool))
				return pool;

			var nPool = new ObjectPool<T>();
			_map.Add(type, nPool);
			return nPool;
		}

		public IPool GetPool(Type type)
		{
			if (_map.TryGetValue(type, out var pool))
				return pool;

			var nPool = new TypePool(type);
			_map.Add(type, nPool);
			return nPool;
		}

		public T Get<T>() where T : IRecycle, new()
		{
			return (T)GetPool(typeof(T)).Get();
		}

		public void Recycle<T>(T item) where T : IRecycle
		{
			GetPool(typeof(T)).Recycle(item);
		}

		public void SetPoolSize(Type type, int size)
		{
			GetPool(type).Maximum = size;
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