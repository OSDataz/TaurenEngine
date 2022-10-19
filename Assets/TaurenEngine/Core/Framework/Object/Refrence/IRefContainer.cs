/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/20 0:10:49
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine
{
	/// <summary>
	/// 引用对象管理容器
	/// </summary>
	public interface IRefContainer
	{
		/// <summary>
		/// 引用对象列表
		/// <para>注意：需要手动赋值</para>
		/// </summary>
		List<IRefObject> RefObjectList { get; }
	}

	/// <summary>
	/// 接口IRefObjectList的扩展方法
	/// </summary>
	public static class IRefContainerExtension
	{
		/// <summary>
		/// 添加到引用计数对象到管理队列，并且引用计数+1；并会检测唯一性
		/// </summary>
		/// <param name="object"></param>
		/// <param name="refObject"></param>
		public static bool AddRefObjectUnique(this IRefContainer @object, IRefObject refObject)
		{
			if (@object.RefObjectList.Contains(refObject))
				return false;// 已有对象，不再缓存

			AddRefObject(@object, refObject);
			return true;
		}

		/// <summary>
		/// 【谨慎使用】添加到引用计数对象到管理队列，并且引用计数+1；不会检测唯一性
		/// </summary>
		/// <param name="object"></param>
		/// <param name="refObject"></param>
		public static void AddRefObject(this IRefContainer @object, IRefObject refObject)
		{
			@object.RefObjectList.Add(refObject);
			refObject.AddRefCount();
		}

		/// <summary>
		/// 从引用计数对象到管理队列中移除，并且引用计数-1
		/// </summary>
		/// <param name="object"></param>
		/// <param name="refObject"></param>
		public static bool RemoveRefObject(this IRefContainer @object, IRefObject refObject)
		{
			if (!@object.RefObjectList.Remove(refObject))
				return false;// 列表里没有该对象

			refObject.DelRefCount();
			return true;
		}

		/// <summary>
		/// 【谨慎使用】从引用计数对象到管理队列中移除，并且引用计数-1；不会检测下标合法性
		/// </summary>
		/// <param name="object"></param>
		/// <param name="index"></param>
		public static void RemoveRefObject(this IRefContainer @object, int index)
		{
			var refObject = @object.RefObjectList[index];
			@object.RefObjectList.RemoveAt(index);
			refObject.DelRefCount();
		}

		/// <summary>
		/// 移除所有引用计数对象
		/// </summary>
		/// <param name="object"></param>
		public static void RemoveAllRefObjects(this IRefContainer @object)
		{
			var index = @object.RefObjectList.Count - 1;
			while (index >= 0)
			{
				RemoveRefObject(@object, index);
				index = @object.RefObjectList.Count - 1;
			}
		}
	}
}