/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/9/13 21:53:55
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEditor
{
	public static class ListExtension
	{
		/// <summary>
		/// 循环遍历，删减元素自动处理
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="object"></param>
		/// <param name="func"></param>
		public static void ForRemoveFunc<T>(this List<T> @object, Func<T, bool> func)
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