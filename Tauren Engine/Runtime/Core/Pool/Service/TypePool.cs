/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.4.0
 *│　Time    ：2022/1/22 10:25:25
 *└────────────────────────┘*/

using System;

namespace Tauren.Core.Runtime
{
	/// <summary>
	/// Type 类型的对象池
	/// <para>框架限制：Type类型必须继承IRecycle</para>
	/// </summary>
	public class TypePool : IPool
	{
		private Type _type;
		private IRecycle[] _caches;
		private int _count;

		public TypePool(Type type, int maximum = 30)
		{
			_type = type;
			_caches = new IRecycle[maximum];
		}

		public bool Contains(IRecycle item)
		{
			for (int i = 0; i < _count; ++i)
			{
				if (item.Equals(_caches[i]))
					return true;
			}

			return false;
		}

		public bool Recycle(IRecycle item)
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

		public IRecycle Get()
		{
			while (_count > 0)
			{
				_count -= 1;
				var item = _caches[_count];
				_caches[_count] = null;
				if (item.IsDestroyed)
					continue;

				return item;
			}

			return (IRecycle)Activator.CreateInstance(_type);
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

		private void DestroyList(int startIdx)
		{
			for (; startIdx < _count; ++startIdx)
			{
				_caches[startIdx].Dispose();// 避免重复进入对象池
				_caches[startIdx] = null;
			}
		}

		public int Maximum
		{
			get => _caches.Length;
			set
			{
				if (Maximum == value)
					return;

				var copyList = new IRecycle[value];

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