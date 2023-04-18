/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.9.0
 *│　Time    ：2022/11/24 21:32:20
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.Core
{
	public class RefrenceList<T> where T : IRefrenceObject
	{
		protected readonly List<T> refObjectList = new List<T>();

		#region 列表函数
		public T this[int index] => refObjectList[index];

		/// <summary>
		/// 列表元素个数
		/// </summary>
		public int Count => refObjectList.Count;

		public int IndexOf(T item) => refObjectList.IndexOf(item);
		#endregion

		#region 引用函数
		/// <summary>
		/// 添加到引用计数对象到管理队列，并且引用计数+1；并会检测唯一性
		/// </summary>
		/// <param name="refObject"></param>
		/// <returns></returns>
		public virtual bool AddUnique(T refObject)
		{
			if (refObjectList.Contains(refObject))
				return false;// 已有对象，不再缓存

			Add(refObject);
			return true;
		}

		/// <summary>
		/// 【谨慎使用】添加到引用计数对象到管理队列，并且引用计数+1；不会检测唯一性
		/// </summary>
		/// <param name="refObject"></param>
		public virtual void Add(T refObject)
		{
			refObjectList.Add(refObject);
			refObject.AddRefCount();
		}

		/// <summary>
		/// 从引用计数对象到管理队列中移除，并且引用计数-1
		/// </summary>
		/// <param name="refObject"></param>
		public virtual bool Remove(T refObject)
		{
			if (!refObjectList.Remove(refObject))
				return false;// 列表里没有该对象

			refObject.DelRefCount();
			return true;
		}

		/// <summary>
		/// 【谨慎使用】从引用计数对象到管理队列中移除，并且引用计数-1；不会检测下标合法性
		/// </summary>
		/// <param name="index"></param>
		public virtual void RemoveAt(int index)
		{
			var refObject = refObjectList[index];
			refObjectList.RemoveAt(index);
			refObject.DelRefCount();
		}

		/// <summary>
		/// 移除所有引用计数对象
		/// </summary>
		public virtual void RemoveAll()
		{
			var index = refObjectList.Count - 1;
			while (index >= 0)
			{
				RemoveAt(index);
				index = refObjectList.Count - 1;
			}
		}
		#endregion
	}
}