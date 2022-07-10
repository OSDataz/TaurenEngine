/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/21 21:43:36
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine.Core
{
	public static partial class ListExtension
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

		/// <summary>
		/// 循环遍历，删减元素自动处理
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="object"></param>
		/// <param name="func"></param>
		public static void ForFunc<T>(this List<T> @object, Func<T, bool> func)
		{
			var len = @object.Count;
			for (int i = 0; i < len;)
			{
				if (func(@object[i])) i += 1;
				else len -= 1;
			}
		}
	}
}