/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2022/1/3 12:24:07
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 循环执行列表
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LoopList<T>
	{
		#region 列表单元状态
		private enum Status
		{
			Ready,
			Add,
			Remove
		}
		#endregion

		#region 列表单元
		private class LoopItem : IRecycle
		{
			public Status status;
			public T item;

			public void Clear()
			{
				item = default(T);
			}

			public void Destroy() => Clear();
		}

		private static ObjectPool<LoopItem> _itemPool = new ObjectPool<LoopItem>();
		#endregion

		private List<LoopItem> _list = new List<LoopItem>();
		private int _length;

		public T this[int index] => _list[index].item;
		public int Length => _length;

		#region 增删
		public bool Add(T item) => Add(item, true);

		public bool Add(T item, bool unique)
		{
			if (item == null)
				return false;

			if (unique)
			{
				var index = IndexOf(item);
				if (index != -1)
				{
					if (_isLoop && _list[index].status == Status.Remove)
					{
						_list[index].status = Status.Ready;
					}
					return false;
				}
			}

			var cell = _itemPool.Get();
			cell.item = item;

			_list.Add(cell);
			_length += 1;

			if (_isLoop)
			{
				cell.status = Status.Add;
				_isUpdate = true;
			}
			else
				cell.status = Status.Ready;

			return true;
		}

		public bool Remove(T item)
		{
			var index = IndexOf(item);
			if (index == -1)
				return false;

			RemoveAtAux(index);
			return true;
		}

		public bool RemoveAt(int index)
		{
			if (index < 0 || index >= _length)
				return false;

			RemoveAtAux(index);
			return true;
		}

		private void RemoveAtAux(int index)
		{
			if (_isLoop)
			{
				_list[index].status = Status.Remove;
				_isUpdate = true;
			}
			else
				RemoveAtReal(index);
		}

		private void RemoveAtReal(int index)
		{
			_itemPool.Add(_list[index]);
			_list.RemoveAt(index);
		}
		#endregion

		#region 查询
		public int IndexOf(T item)
		{
			for (int i = 0; i < _length; ++i)
			{
				if (_list[i].item.Equals(item))
					return i;
			}

			return -1;
		}

		public bool Contains(T item) => IndexOf(item) != -1;

		public T Find(Predicate<T> match)
		{
			for (int i = 0; i < _length; ++i)
			{
				if (match(_list[i].item))
					return _list[i].item;
			}

			return default(T);
		}
		#endregion

		#region 清理
		public void Clear()
		{
			for (_index = 0; _index < _length; ++_index)
			{
				_itemPool.Add(_list[_index]);
			}

			_list.Clear();
			_length = 0;
			_isUpdate = false;
		}
		#endregion

		#region 循环执行
		private int _index;

		private bool _isUpdate;
		private bool _isLoop = false;

		public void ForEach(Action<T> action)
		{
			if (_isLoop)
			{
				TDebug.Error("LoopList.ForEach 循环执行嵌套");
				return;
			}

			_isLoop = true;

			for (_index = 0; _index < _length; ++_index)
			{
				if (_list[_index].status == Status.Ready)
					action.Invoke(_list[_index].item);
			}

			_isLoop = false;

			if (_isUpdate)
			{
				_isUpdate = false;

				for (_index = _length - 1; _index >= 0; --_index)
				{
					if (_list[_index].status == Status.Add)
						_list[_index].status = Status.Ready;
					else if (_list[_index].status == Status.Remove)
						RemoveAtReal(_index);
				}
			}
		}
		#endregion
	}
}