﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/4 10:21:27
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public static class JsonHelper
	{
		/// <summary>
		/// 字符串转Json对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static T ToObject<T>(string value)
		{
			return IJsonService.Instance.ToObject<T>(value);
		}

		/// <summary>
		/// Json对象转字符串
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string ToJson(object value)
		{
			return IJsonService.Instance.ToJson(value);
		}
	}
}