/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/9/8 10:11:44
 *└────────────────────────┘*/

namespace Tauren.Core.Runtime
{
	/// <summary>
	/// 对象池
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	public sealed class ObjectPool<TItem> : IPool where TItem : IRecycle, new()
	{
		private TItem[] _caches;
		private int _count;

		public ObjectPool(int maximum = 30)
		{
			_caches = new TItem[maximum];
		}

		public bool Contains(TItem item)
		{
			for (int i = 0; i < _count; ++i)
			{
				if (item.Equals(_caches[i]))
					return true;
			}

			return false;
		}

		public bool Recycle(IRecycle item) => Recycle((TItem)item);

		public bool Recycle(TItem item)
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

		public IRecycle Get() => GetItem();

		public TItem GetItem()
		{
			while (_count > 0)
			{
				_count -= 1;
				var item = _caches[_count];
				_caches[_count] = default(TItem);
				if (item.IsDestroyed)
					continue;

				return item;
			}

			return new TItem();
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
				_caches[startIdx] = default(TItem);
			}
		}

		public int Maximum
		{
			get => _caches.Length;
			set
			{
				if (Maximum == value)
					return;

				var copyList = new TItem[value];

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