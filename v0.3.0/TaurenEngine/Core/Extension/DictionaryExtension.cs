/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/21 22:05:24
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.Core
{
	public static partial class DictionaryExtension
	{
		public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> @object, TKey key, TValue value)
		{
			if (@object.ContainsKey(key))
				@object[key] = value;
			else
				@object.Add(key, value);
		}
	}
}