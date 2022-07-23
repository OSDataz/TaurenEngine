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
		/// </summary>
		List<IRefObject> RefObjectList { get; set; }
	}

	/// <summary>
	/// 接口IRefObjectList的扩展方法
	/// </summary>
	public static class IRefContainerExtension
	{
		/// <summary>
		/// 添加到引用计数对象到管理队列，并且引用计数+1
		/// </summary>
		/// <param name="object"></param>
		/// <param name="refObject"></param>
		public static void AddRefObject(this IRefContainer @object, IRefObject refObject)
		{
			if (@object.RefObjectList == null)
				@object.RefObjectList = new List<IRefObject>();
			else if (@object.RefObjectList.Contains(refObject))
				return;// 已有对象，不再缓存

			refObject.AddRefCount();
			@object.RefObjectList.Add(refObject);
		}

		/// <summary>
		/// 从引用计数对象到管理队列中移除，并且引用计数-1
		/// </summary>
		/// <param name="object"></param>
		/// <param name="refObject"></param>
		public static void RemoveRefObject(this IRefContainer @object, IRefObject refObject)
		{
			if (@object.RefObjectList == null)
				return;

			if (@object.RefObjectList.Remove(refObject))
				refObject.DelRefCount();
		}

		/// <summary>
		/// 移除所有引用计数对象
		/// </summary>
		/// <param name="object"></param>
		public static void RemoveAllRefObjects(this IRefContainer @object)
		{
			if (@object.RefObjectList == null)
				return;

			for (int i = @object.RefObjectList.Count - 1; i >= 0; --i)
			{
				@object.RefObjectList[i]?.DelRefCount();
			}

			@object.RefObjectList.Clear();
		}
	}
}