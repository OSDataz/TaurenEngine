/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:42:40
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine
{
	public static class DictionaryExtension
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