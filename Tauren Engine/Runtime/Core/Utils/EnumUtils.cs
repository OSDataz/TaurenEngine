/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/9 17:36:51
 *└────────────────────────┘*/

using System.Collections.Generic;
using System;
using System.Linq;

namespace Tauren.Core.Runtime
{
	public static class EnumUtils
	{
		/// <summary>
		/// 获取枚举列表
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static List<T> GetEnumList<T>() where T : Enum
		{
			return Enum.GetValues(typeof(T)).OfType<T>().ToList();
		}
	}
}