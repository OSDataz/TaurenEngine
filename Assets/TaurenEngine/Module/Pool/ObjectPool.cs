/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/9/8 10:11:44
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.ModPool
{
	/// <summary>
	/// 对象池
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class ObjectPool<T> : IObjectPool<T> where T : IRecycle, new()
	{
		private T[] _caches;
		private int _count;

		public ObjectPool(int maximum = 30)
		{
			_caches = new T[maximum];
		}

		public bool Contains(T item)
		{
			for (int i = 0; i < _count; ++i)
			{
				if (item.Equals(_caches[i]))
					return true;
			}

			return false;
		}

		public bool Add(T item)
		{
			if (item == null)
				return false;

			if (item.IsDestroyed)
				return false;

			if (Contains(item))
				return true;

			if (_count >= Maximum)
			{
				item.Dispose();// 避免重复进入对象池
				return false;
			}
			else
			{
				item.Clear();
				_caches[_count++] = item;
				return true;
			}
		}

		public T Get()
		{
			while (_count > 0)
			{
				_count -= 1;
				var item = _caches[_count];
				_caches[_count] = default(T);
				if (item.IsDestroyed)
					continue;

				return item;
			}

			return new T();
		}

		public void Clear()
		{
			DestroyList(0);

			_count = 0;
		}

		public void Destroy()
		{
			Clear();

			_caches = null;
		}

		/// <summary>
		/// 销毁指定列表
		/// </summary>
		/// <param name="startIdx"></param>
		private void DestroyList(int startIdx)
		{
			for (; startIdx < _count; ++startIdx)
			{
				_caches[startIdx].Dispose();// 避免重复进入对象池
				_caches[startIdx] = default(T);
			}
		}

		public int Maximum
		{
			get => _caches.Length;
			set
			{
				if (Maximum == value)
					return;

				var copyList = new T[value];

				if (_count > 0)
					_caches.CopyTo(copyList, 0);

				if (_count > value)
				{
					DestroyList(value);

					_count = value;
				}

				_caches = copyList;
			}
		}

		public int Count => _count;

		/// <summary>
		/// 是否被销毁
		/// </summary>
		public bool IsDestroyed => _caches == null;
	}
}