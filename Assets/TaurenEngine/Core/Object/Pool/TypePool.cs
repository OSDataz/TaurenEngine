/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.4.0
 *│　Time    ：2022/1/22 10:25:25
 *└────────────────────────┘*/

using System;

namespace TaurenEngine
{
	/// <summary>
	/// Type 类型的对象池
	/// <para>框架限制：Type类型必须继承IRecycle</para>
	/// </summary>
	public class TypePool
	{
		private Type _type;
		private IRecycle[] _caches;
		private int _count;

		public TypePool(Type type, int maximum = 30)
		{
			_type = type;
			_caches = new IRecycle[maximum];
		}

		/// <summary>
		/// 对象池中是否有该对象
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(IRecycle item)
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
		public bool Add(IRecycle item)
		{
			if (item == null)
				return false;

			if (item.IsDestroyed)
				return false;

			if (Contains(item))
				return true;

			if (_count >= Maximum)
			{
				item.Dispose();
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
		public object Get()
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

			return Activator.CreateInstance(_type);
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

		private void DestroyList(int startIdx)
		{
			for (; startIdx < _count; ++startIdx)
			{
				_caches[startIdx].Dispose();
				_caches[startIdx] = null;
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

		/// <summary>
		/// 当前缓存对象数
		/// </summary>
		public int Count => _count;
	}
}