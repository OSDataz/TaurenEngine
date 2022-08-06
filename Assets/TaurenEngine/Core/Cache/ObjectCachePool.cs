/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/8/2 20:08:15
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine
{
	/// <summary>
	/// 对象缓冲池
	/// </summary>
	public class ObjectCachePool : IRefContainer
	{
		/// <summary>
		/// 自动释放队列(缓冲池)
		/// </summary>
		public List<IRefObject> RefObjectList { get; protected set; }

		/// <summary>
		/// 缓冲数量
		/// </summary>
		public int Capacity { get; protected set; }

		public ObjectCachePool(int capacity = 50)
		{
			Capacity = capacity;
			RefObjectList = new List<IRefObject>(Capacity);
		}

		/// <summary>
		/// 清理所有缓存
		/// </summary>
		public virtual void Clear()
		{

		}

		/// <summary>
		/// 添加到缓冲队列
		/// </summary>
		/// <param name="refObject"></param>
		public virtual void AddToCache(IRefObject refObject)
		{
			if (RefObjectList.Contains(refObject))
				return;

			CheckCapacity();
			this.AddRefObject(refObject);
		}

		/// <summary>
		/// 从缓冲队列移除
		/// </summary>
		/// <param name="refObject"></param>
		public virtual void RemoveToCache(IRefObject refObject)
		{
			this.RemoveRefObject(refObject);
		}

		/// <summary>
		/// 检查容量，缓冲溢出时将移除部分对象
		/// </summary>
		public virtual void CheckCapacity()
		{
			while (CheckCacheFull())
			{
				this.RemoveRefObject(FindOldestIndex());
			}
		}

		/// <summary>
		/// 检查缓存是否已满
		/// </summary>
		public virtual bool CheckCacheFull()
		{
			return RefObjectList.Count > Capacity;
		}

		/// <summary>
		/// 查找最久未被使用的索引
		/// </summary>
		/// <returns></returns>
		public virtual int FindOldestIndex()
		{
			return 0;
		}
	}
}