/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/17 20:19:01
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine.Hotfix
{
	/// <summary>
	/// 支持循环中增删元素的列表，循环嵌套，减少GC
	/// <para>注意：不能提供任何操作下标Index的接口</para>
	/// </summary>
	internal class EventList : LifecycleObject, IRecycle
	{
		/// <summary> 引用列表 </summary>
		protected readonly RefrenceList<Event> refrenceList = new RefrenceList<Event>();

		/// <summary> 列表对象对应的当前状态 </summary>
		protected readonly List<bool> status = new List<bool>();

		/// <summary> 【逻辑上】列表长度 </summary>
		public int LengthLogic { get; protected set; }
		/// <summary> 【实际上】列表长度 </summary>
		public int LengthReal => refrenceList.Count;

		/// <summary> 【逻辑上】列表是否为空 </summary>
		public bool IsEmptyLogic => LengthLogic == 0;
		/// <summary> 【实际上】列表是否为空</summary>
		public bool IsEmptyReal => refrenceList.Count == 0;

		#region 内部运行参数
		protected bool isUpdate;
		protected int loopCount = 0;
		protected bool IsLoop => loopCount > 0;
		#endregion

		/// <summary>
		/// 添加列表对象
		/// </summary>
		/// <param name="item"></param>
		/// <param name="unique"></param>
		public void Add(Event item, bool unique = false)
		{
			if (unique)
			{
				var index = refrenceList.IndexOf(item);
				if (index != -1)
				{
					if (!status[index])
					{
						// 将还未被删除的对象添加回来
						status[index] = true;
						LengthLogic += 1;
					}
					return;
				}
			}

			refrenceList.Add(item);
			status.Add(true);

			LengthLogic += 1;
		}

		/// <summary>
		/// 移除列表对象
		/// </summary>
		/// <param name="item"></param>
		public void Remove(Event item)
		{
			var index = refrenceList.IndexOf(item);
			if (index == -1)
				return;

			if (IsLoop)
			{
				if (!status[index])
					return;

				status[index] = false;
				isUpdate = true;
			}
			else
			{
				refrenceList.RemoveAt(index);
				status.RemoveAt(index);
			}

			LengthLogic -= 1;
		}

		/// <summary>
		/// 移除范围内对象（非严谨，未检测长度越界，后续有需求再处理）
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
					status[index] = false;
				}

				isUpdate = true;
			}
			else
			{
				for (var i = index + count - 1; i >= index; --i)
				{
					refrenceList.RemoveAt(i);
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
		public bool Contains(Event item)
		{
			var index = refrenceList.IndexOf(item);
			if (index == -1)
				return false;

			return status[index];
		}

		/// <summary>
		/// 【逻辑上】查找指定对象
		/// </summary>
		/// <param name="match"></param>
		/// <returns></returns>
		public Event Find(Predicate<Event> match)
		{
			var len = refrenceList.Count;
			for (int i = 0; i < len; ++i)
			{
				if (status[i] && match.Invoke(refrenceList[i]))
					return refrenceList[i];
			}

			return null;
		}

		/// <summary>
		/// 【逻辑上】清理列表
		/// </summary>
		public virtual void Clear()
		{
			if (IsLoop)
			{
				var len = status.Count;
				for (int i = 0; i < len; ++i)
				{
					status[i] = false;
				}

				isUpdate = true;
			}
			else
			{
				refrenceList.Clear();
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
		public void ForEach(Action<Event> action, bool resetLoop = false)
		{
			var len = refrenceList.Count;
			if (len == 0)
				return;

			if (resetLoop) loopCount = 1;
			else loopCount += 1;

			int i;
			for (i = 0; i < len; ++i)
			{
				if (status[i])
				{
#if BUILD_MODE_DEBUG
					try
					{
#endif
						action.Invoke(refrenceList[i]);// todo 需考虑使用try catch的必要性
#if BUILD_MODE_DEBUG
					}	
					catch (Exception exception) 
					{
						Log.Error(exception.StackTrace);
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
					refrenceList.Clear();
					status.Clear();
				}
				else
				{
					for (i = refrenceList.Count - 1; i >= 0; --i)
					{
						if (!status[i])
						{
							refrenceList.RemoveAt(i);
							status.RemoveAt(i);
						}
					}
				}
			}
		}
		#endregion
	}
}