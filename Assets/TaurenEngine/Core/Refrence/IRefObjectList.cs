/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/20 0:10:49
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 有引用对象管理列表的对象
	/// </summary>
	public interface IRefObjectList
	{
		/// <summary>
		/// 引用计数对象管理列表
		/// </summary>
		List<IRefObject> RefObjectList { get; set; }
	}

	/// <summary>
	/// 接口IRefObjectList的扩展方法
	/// </summary>
	public static class IRefObjectListExtension
	{
		/// <summary>
		/// 添加到引用计数对象到管理队列
		/// </summary>
		/// <param name="object"></param>
		/// <param name="refObject"></param>
		public static void AddRefObject(this IRefObjectList @object, IRefObject refObject)
		{
			if (@object.RefObjectList == null)
				@object.RefObjectList = new List<IRefObject>();

			@object.RefObjectList.Add(refObject);

			refObject.RefName = @object.ToString();
		}

		/// <summary>
		/// 添加到引用计数对象到管理队列，并且引用计数+1
		/// </summary>
		/// <param name="object"></param>
		/// <param name="refObject"></param>
		public static void AddRefObjectAndRetain(this IRefObjectList @object, IRefObject refObject)
		{
			refObject.Retain();
			AddRefObject(@object, refObject);
		}

		/// <summary>
		/// 从引用计数对象到管理队列中移除
		/// </summary>
		/// <param name="object"></param>
		/// <param name="refObject"></param>
		public static void RemoveRefObject(this IRefObjectList @object, IRefObject refObject)
		{
			@object.RefObjectList?.Remove(refObject);
		}

		/// <summary>
		/// 从引用计数对象到管理队列中移除，并且引用计数-1
		/// </summary>
		/// <param name="object"></param>
		/// <param name="refObject"></param>
		public static void RemoveRefObjectAndRelease(this IRefObjectList @object, IRefObject refObject)
		{
			refObject.Release();
			RemoveRefObject(@object, refObject);
		}

		/// <summary>
		/// 移除所有引用计数对象
		/// </summary>
		/// <param name="object"></param>
		public static void RemoveAllRefObjects(this IRefObjectList @object)
		{
			if (@object.RefObjectList == null)
				return;

			for (int i = @object.RefObjectList.Count - 1; i >= 0; --i)
			{
				@object.RefObjectList[i]?.Release();
			}

			@object.RefObjectList.Clear();
		}
	}
}