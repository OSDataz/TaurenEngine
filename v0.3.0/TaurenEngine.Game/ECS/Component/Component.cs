/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 9:20:21
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Core;

namespace TaurenEngine.ECS
{
	/// <summary>
	/// 数据组件
	/// </summary>
	/// <typeparam name="TClass"></typeparam>
	/// <typeparam name="TData"></typeparam>
	public abstract class Component<TClass, TData> : Singleton<TClass> where TClass : class, new() where TData : class, new()
	{
		public Dictionary<uint, TData> Datas { get; private set; }

		public Component()
		{
			Datas = new Dictionary<uint, TData>();
		}

		public TData Bind(uint id)
		{
			if (Datas.ContainsKey(id))
				return Datas[id];

			var data = new TData();
			Datas.Add(id, data);
			return data;
		}

		public void Unbind(uint id)
		{
			Datas.Remove(id);
		}

		public bool IsContains(uint id)
		{
			return Datas.ContainsKey(id);
		}

		public TData TryGet(uint id)
		{
			return Datas.ContainsKey(id) ? Datas[id] : default(TData);
		}

		public TData Get(uint id)
		{
			return Datas[id];
		}
	}
}