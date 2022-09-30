/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/25 10:55:39
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine
{
	/// <summary>
	/// 支持循环中增删元素的列表，减少GC
	/// <para>注意：不能提供任何操作下标Index的接口</para>
	/// <para>最好只用于Event和Timer管理，确保Destroy不会有嵌套删除</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LoopList<T> : RefContainer<T>, IRecycle where T : IRefObject
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

		/// <summary> 列表对象对应的当前状态 </summary>
		protected readonly List<Status> status = new List<Status>();

		/// <summary> 【逻辑上】列表长度 </summary>
		public int LengthLogic { get; protected set; }
		/// <summary> 【实际上】列表长度 </summary>
		public int LengthReal => RefObjectList.Count;
		/// <summary> 【逻辑上】列表是否为空 </summary>
		public bool IsEmptyLogic => LengthLogic == 0;
		/// <summary> 【实际上】列表是否为空</summary>
		public bool IsEmptyReal => RefObjectList.Count == 0;

		protected bool isUpdate;
		protected int loopCount = 0;
		protected bool IsLoop => loopCount > 0;

		/// <summary>
		/// 添加列表对象
		/// </summary>
		/// <param name="item"></param>
		/// <param name="unique"></param>
		public void Add(T item, bool unique = false)
		{
			if (unique)
			{
				var index = RefObjectList.IndexOf(item);
				if (index != -1)
				{
					if (status[index] == Status.Remove)
					{
						status[index] = Status.Add;
						LengthLogic += 1;
					}
					return;
				}
			}

			AddRefObject(item);

			if (IsLoop)
			{
				status.Add(Status.Add);
				isUpdate = true;
			}
			else
			{
				status.Add(Status.None);
			}

			LengthLogic += 1;
		}

		/// <summary>
		/// 移除列表对象
		/// </summary>
		/// <param name="item"></param>
		public void Remove(T item)
		{
			var index = RefObjectList.IndexOf(item);
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
				RemoveRefObject(index);
				status.RemoveAt(index);
			}

			LengthLogic -= 1;
		}

		/// <summary>
		/// 移除范围内对象
		/// </summary>
		/// <param name="index">起始下标</param>
		/// <param name="count">长度</param>
		public void RemoveRange(int index, int count)
		{
			if (IsLoop)
			{
				var len = index + count;
				for (; index < len; ++index)
				{
					status[index] = Status.Remove;
				}

				isUpdate = true;
			}
			else
			{
				for (var i = index + count - 1; i >= index; --i)
				{
					RemoveRefObject(i);
				}

				status.RemoveRange(index, count);
			}

			LengthLogic -= count;
		}

		/// <summary>
		/// 【逻辑上】列表中是否有指定对象
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Contains(T item)
		{
			var index = RefObjectList.IndexOf(item);
			if (index == -1)
				return false;

			if (status[index] == Status.Remove)
				return false;

			return true;
		}

		/// <summary>
		/// 【逻辑上】查找指定对象
		/// </summary>
		/// <param name="match"></param>
		/// <returns></returns>
		public T Find(Predicate<T> match)
		{
			var len = RefObjectList.Count;
			for (int i = 0; i < len; ++i)
			{
				if (status[i] != Status.Remove && match.Invoke(RefObjectList[i]))
					return RefObjectList[i];
			}

			return default(T);
		}

		/// <summary>
		/// 【逻辑上】清理列表
		/// </summary>
		public override void Clear()
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
				RemoveAllRefObjects();
				status.Clear();
			}

			LengthLogic = 0;
		}

		public override void OnDestroy() => Clear();

		#region 循环执行
		/// <summary>
		/// 循环中 新添加的 和 被删除的 都不执行
		/// </summary>
		/// <param name="action"></param>
		/// <param name="resetLoop">强制循环次数，如果嵌套循环务必设置为 false</param>
		public void ForEach(Action<T> action, bool resetLoop = false)
		{
			var len = RefObjectList.Count;
			if (len == 0)
				return;

			if (resetLoop) loopCount = 1;
			else loopCount += 1;

			int i;
			for (i = 0; i < len; ++i)
			{
				if (status[i] == Status.None)
				{
#if TryCatch
					try
					{
#endif
						action.Invoke(RefObjectList[i]);// todo 需考虑使用try catch的必要性
#if TryCatch
					}
					catch (Exception exception) 
					{
						DebugEx.Error(exception.StackTrace);
					}
#endif
				}
			}

			loopCount -= 1;

			if (loopCount == 0 && isUpdate)
			{
				isUpdate = false;

				if (LengthLogic == 0)
				{
					RemoveAllRefObjects();
					status.Clear();
				}
				else
				{
					for (i = RefObjectList.Count - 1; i >= 0; --i)
					{
						if (status[i] == Status.Add)
						{
							status[i] = Status.None;
						}
						else if (status[i] == Status.Remove)
						{
							RemoveRefObject(i);
							status.RemoveAt(i);
						}
					}
				}
			}
		}
#endregion
	}
}