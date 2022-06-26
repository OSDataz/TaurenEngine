/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/25 10:55:39
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 支持循环中增删元素的列表
	/// <para>注意：不能提供任何操作下标Index的接口</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LoopList<T> : IRecycle
	{
		/// <summary>
		/// 循环状态
		/// </summary>
		protected enum Status
		{
			None,
			Add,
			Remove
		}

		protected List<T> list;
		protected List<Status> status;
		public int Length { get; protected set; }
		public bool IsEmpty => list.Count == 0;

		protected bool isUpdate;
		protected int loopCount = 0;
		protected bool IsLoop => loopCount > 0;

		public LoopList()
		{
			list = new List<T>();
			status = new List<Status>();
		}

		public void Add(T item, bool unique = false)
		{
			if (unique)
			{
				var index = list.IndexOf(item);
				if (index != -1)
				{
					if (status[index] == Status.Remove)
					{
						status[index] = Status.Add;
						Length += 1;
					}
					return;
				}
			}

			list.Add(item);

			if (IsLoop)
			{
				status.Add(Status.Add);
				isUpdate = true;
			}
			else
			{
				status.Add(Status.None);
			}

			Length += 1;
		}

		public void Remove(T item)
		{
			var index = list.IndexOf(item);
			if (index == -1)
				return;

			if (IsLoop)
			{
				if (status[index] == Status.Remove)
					return;

				status[index] = Status.Remove;
				isUpdate = true;
			}
			else
			{
				list.RemoveAt(index);
				status.RemoveAt(index);
			}

			Length -= 1;
		}

		public bool Contains(T item)
		{
			var index = list.IndexOf(item);
			if (index == -1)
				return false;

			if (status[index] == Status.Remove)
				return false;

			return true;
		}

		public T Find(Predicate<T> match)
		{
			var len = list.Count;
			for (int i = 0; i < len; ++i)
			{
				if (status[i] != Status.Remove && match.Invoke(list[i]))
					return list[i];
			}

			return default(T);
		}

		public void Clear()
		{
			if (IsLoop)
			{
				var len = status.Count;
				for (int i = 0; i < len; ++i)
				{
					status[i] = Status.Remove;
				}

				isUpdate = true;
			}
			else
			{
				list.Clear();
				status.Clear();
			}

			Length = 0;
		}

		public void Destroy() => Clear();

		#region 循环执行
		/// <summary>
		/// 循环中 新添加的 和 被删除的 都不执行
		/// </summary>
		/// <param name="action"></param>
		/// <param name="resetLoop">强制循环次数，如果嵌套循环务必设置为 false</param>
		public void ForEach(Action<T> action, bool resetLoop = false)
		{
			if (resetLoop) loopCount = 1;
			else loopCount += 1;

			int i;
			var len = list.Count;

			for (i = 0; i < len; ++i)
			{
				if (status[i] == Status.None)
					action.Invoke(list[i]);
			}

			loopCount -= 1;

			if (loopCount == 0 && isUpdate)
			{
				isUpdate = false;

				if (Length == 0)
				{
					list.Clear();
					status.Clear();
				}
				else
				{
					for (i = list.Count - 1; i >= 0; --i)
					{
						if (status[i] == Status.Add)
						{
							status[i] = Status.None;
						}
						else if (status[i] == Status.Remove)
						{
							list.RemoveAt(i);
							status.RemoveAt(i);
						}
					}
				}
			}
		}
		#endregion
	}
}