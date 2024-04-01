/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:59:15
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace Tauren.Core.Runtime
{
	public static class ListExtension
	{
		/// <summary>
		/// 从数组中移除第一个并返回
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="object"></param>
		/// <returns></returns>
		public static T Shift<T>(this List<T> @object)
		{
			if (@object.Count == 0)
				return default(T);

			var item = @object[0];
			@object.RemoveAt(0);

			return item;
		}

		/// <summary>
		/// 从数组中移除最后一个并返回
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="object"></param>
		/// <returns></returns>
		public static T Pop<T>(this List<T> @object)
		{
			var index = @object.Count;
			if (index == 0)
				return default(T);

			index -= 1;
			var item = @object[index];
			@object.RemoveAt(index);
			return item;
		}

		/// <summary>
		/// 将一个对象移动到数组指定位置
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="object"></param>
		/// <param name="item"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static bool Move<T>(this List<T> @object, T item, int index)
		{
			if (index < 0 || index >= @object.Count)
				return false;

			var ci = @object.IndexOf(item);
			if (ci == -1)
				return false;

			if (ci == index)
				return true;

			@object.RemoveAt(ci);
			@object.Insert(index, item);
			return true;
		}

		/// <summary>
		/// 给数组添加对象，并且对象不能重复
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="object"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static bool AddUnique<T>(this List<T> @object, T item)
		{
			if (@object.Contains(item))
				return false;

			@object.Add(item);
			return true;
		}
	}
}