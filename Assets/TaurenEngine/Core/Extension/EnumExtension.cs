/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:37:06
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