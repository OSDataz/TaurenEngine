/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/21 11:32:49
 *└────────────────────────┘*/

using System.Collections;
using System.Collections.Generic;

namespace TaurenEngine.Hotfix
{
	public class RefrenceMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TValue : IRefrenceObject
	{
		protected readonly Dictionary<TKey, TValue> refMap = new Dictionary<TKey, TValue>();

		#region 字典函数
		public TValue this[TKey key] => refMap[key];

		public int Count => refMap.Count;

		public Dictionary<TKey, TValue>.KeyCollection Keys => refMap.Keys;

		public Dictionary<TKey, TValue>.ValueCollection Values => refMap.Values;

		public bool ContainsKey(TKey key) => refMap.ContainsKey(key);

		public bool ContainsValue(TValue value) => refMap.ContainsValue(value);

		public bool TeyGetValue(TKey key, out TValue value) => refMap.TryGetValue(key, out value);
		#endregion

		#region 引用函数
		public virtual void Add(TKey key, TValue value)
		{
			if (refMap.TryGetValue(key, out var old))
			{
				if (old.Equals(value))
					return;

				old.DelRefCount();
			}

			refMap.Add(key, value);
			value.AddRefCount();
		}

		public virtual bool Remove(TKey key)
		{
			if (!refMap.TryGetValue(key, out var value))
				return false;

			refMap.Remove(key);
			value.DelRefCount();

			return true;
		}

		public virtual void Clear()
		{
			while (refMap.Count > 0)
			{
				var keys = refMap.Keys;
				foreach (var key in keys)
				{
					var value = refMap[key];
					refMap.Remove(key);
					value.DelRefCount();
				}
			}
		}
		#endregion

		#region 迭代
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return refMap.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}