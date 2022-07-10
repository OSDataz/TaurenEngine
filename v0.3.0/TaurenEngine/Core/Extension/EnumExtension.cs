/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/24 13:55:33
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Core
{
    public static class EnumExtension
    {
		/// <summary>
		/// 字符串转化为指定枚举类型
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="object"></param>
		/// <returns></returns>
		public static T ToEnum<T>(this string @object) where T : Enum
		{
			return (T)Enum.Parse(typeof(T), @object);
		}
	}
}