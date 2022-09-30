/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/8/2 20:08:15
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 对象缓冲池
	/// </summary>
	public class ObjectCachePool<T> : RefContainer<T> where T : IRefObject
	{
		/// <summary>
		/// 缓冲数量
		/// </summary>
		public int capacity = 50;

		/// <summary>
		/// 添加到缓冲队列
		/// </summary>
		/// <param name="item"></param>
		public virtual void AddToCache(T item)
		{
			if (RefObjectList.Contains(item))
				return;

			CheckCapacity();
			AddRefObject(item);
		}

		/// <summary>
		/// 从缓冲队列移除
		/// </summary>
		/// <param name="item"></param>
		public virtual void RemoveToCache(T item)
		{
			RemoveRefObject(item);
		}

		/// <summary>
		/// 检查容量，缓冲溢出时将移除部分对象
		/// </summary>
		protected virtual void CheckCapacity()
		{
			while (CheckCacheFull())
			{
				RemoveRefObject(FindOldestIndex());
			}
		}

		/// <summary>
		/// 检查缓存是否已满
		/// </summary>
		protected virtual bool CheckCacheFull()
		{
			return RefObjectList.Count > capacity;
		}

		/// <summary>
		/// 查找最久未被使用的索引
		/// </summary>
		/// <returns></returns>
		protected virtual int FindOldestIndex()
		{
			return 0;
		}
	}
}