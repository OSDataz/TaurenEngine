/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/9/8 10:11:44
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 对象池
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class ObjectPool<T> where T : IRecycle, new()
	{
		private T[] _caches;
		private int _count;

		public ObjectPool(int maximum = 30)
		{
			_caches = new T[maximum];
		}

		/// <summary>
		/// 对象池中是否有该对象
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(T item)
		{
			for (int i = 0; i < _count; ++i)
			{
				if (item.Equals(_caches[i]))
					return true;
			}

			return false;
		}

		/// <summary>
		/// 向对象池放入一个对象
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
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

		/// <summary>
		/// 从对象池取出一个对象
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// 清理对象池
		/// </summary>
		public void Clear()
		{
			DestroyList(0);

			_count = 0;
		}

		/// <summary>
		/// 销毁对象池
		/// </summary>
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

		/// <summary>
		/// 最大缓存对象数
		/// </summary>
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

		/// <summary>
		/// 当前缓存对象数
		/// </summary>
		public int Count => _count;
	}
}