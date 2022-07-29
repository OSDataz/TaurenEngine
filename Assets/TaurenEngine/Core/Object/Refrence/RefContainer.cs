/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/24 11:58:33
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine
{
	/// <summary>
	/// 指定类型的引用对象容器
	/// <para>备注：不使用接口因为泛型接口的扩展函数不能正常使用</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RefContainer<T> : RefObject, IRecycle where T : IRefObject
	{
		/// <summary>
		/// 引用对象列表
		/// </summary>
		public List<T> RefObjectList { get; } = new List<T>();

		/// <summary>
		/// 添加到引用计数对象到管理队列，并且引用计数+1；并会检测唯一性
		/// </summary>
		/// <param name="refObject"></param>
		/// <returns></returns>
		public bool AddRefObjectUnique(T refObject)
		{
			if (RefObjectList.Contains(refObject))
				return false;// 已有对象，不再缓存

			RefObjectList.Add(refObject);
			refObject.AddRefCount();
			return true;
		}

		/// <summary>
		/// 【谨慎使用】添加到引用计数对象到管理队列，并且引用计数+1；不会检测唯一性
		/// </summary>
		/// <param name="refObject"></param>
		public void AddRefObject(T refObject)
		{
			RefObjectList.Add(refObject);
			refObject.AddRefCount();
		}

		/// <summary>
		/// 从引用计数对象到管理队列中移除，并且引用计数-1
		/// </summary>
		/// <param name="refObject"></param>
		/// <returns></returns>
		public bool RemoveRefObject(T refObject)
		{
			if (!RefObjectList.Remove(refObject))
				return false;// 列表里没有该对象

			refObject.DelRefCount();
			return true;
		}

		/// <summary>
		/// 【谨慎使用】从引用计数对象到管理队列中移除，并且引用计数-1；不会检测下标合法性
		/// </summary>
		/// <param name="index"></param>
		public void RemoveRefObject(int index)
		{
			RefObjectList[index].DelRefCount();
			RefObjectList.RemoveAt(index);
		}

		/// <summary>
		/// 移除所有引用计数对象
		/// </summary>
		public void RemoveAllRefObjects()
		{
			for (int i = RefObjectList.Count - 1; i >= 0; --i)
			{
				RefObjectList[i]?.DelRefCount();
			}

			RefObjectList.Clear();
		}

		public virtual void Clear()
			=> RemoveAllRefObjects();
	}
}