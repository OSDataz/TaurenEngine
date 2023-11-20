/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/9/8 10:13:51
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using TaurenEngine.Core;

namespace TaurenEngine.ModPool
{
	/// <summary>
	/// 超级对象池，支持各种类型的对象添加和取出
	/// </summary>
	public sealed class PoolService : IPoolService
	{
		private readonly Dictionary<Type, object> _map = new Dictionary<Type, object>();

		public PoolService()
		{
			this.InitInterface();
		}

		public IObjectPool<T> GetOrCreatePool<T>() where T : IRecycle, new()
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

		public ITypePool GetOrCreatePool(Type type)
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

		public T Get<T>() where T : IRecycle, new()
		{
			Type type = typeof(T);
			if (_map.TryGetValue(type, out var pool))
			{
				if (pool is TypePool tPool)
					return (T)tPool.Get();

				if (pool is ObjectPool<T> oPool)
					return oPool.Get();

				Log.Error($"{type}对象池对象异常，类型：{pool.GetType()}");
			}

			var nPool = new TypePool(type);
			_map[type] = nPool;

			return (T)nPool.Get();
		}

		public void Add<T>(T item) where T : IRecycle, new()
		{
			Type type = typeof(T);
			if (_map.TryGetValue(type, out var pool))
			{
				if (pool is TypePool tPool)
				{
					tPool.Add(item);
					return;
				}

				if (pool is ObjectPool<T> oPool)
				{
					oPool.Add(item);
					return;
				}

				Log.Error($"{type}对象池对象异常，类型：{pool.GetType()}");
			}

			var nPool = new TypePool(type);
			_map[type] = nPool;

			nPool.Add(item);
		}

		public void ClearPool<T>() where T : IRecycle, new()
		{
			Type type = typeof(T);
			if (!_map.TryGetValue(type, out var pool))
				return;

			if (pool is TypePool tPool)
			{
				tPool.Clear();
				return;
			}

			if (pool is ObjectPool<T> oPool)
			{
				oPool.Clear();
				return;
			}

			Log.Error($"{type}对象池对象异常，类型：{pool.GetType()}");
		}

		public void DestroyPool<T>() where T : IRecycle, new()
		{
			Type type = typeof(T);
			if (!_map.TryGetValue(type, out var pool))
				return;

			if (pool is TypePool tPool)
			{
				tPool.Destroy();
			}
			else if (pool is ObjectPool<T> oPool)
			{
				oPool.Destroy();
			}
			else
			{
				Log.Error($"{type}对象池对象异常，类型：{pool.GetType()}");
			}

			_map.Remove(type);
		}
	}
}