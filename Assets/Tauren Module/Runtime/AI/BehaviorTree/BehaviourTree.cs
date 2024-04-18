/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/29 8:04:54
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// AI行为树
	/// 在Awake中设置节点
	/// </summary>
	public abstract class BehaviourTree : MonoBehaviour, IAIData
	{
		protected AINode rootNode;

		/// <summary>
		/// 初始化数据
		/// </summary>
		protected abstract void InitData();

		/// <summary>
		/// 初始化节点（注意：务必赋值根节点_rootNode）
		/// </summary>
		protected abstract void InitNode();

		void Awake()
		{
			InitData();
			InitNode();

			rootNode?.Activate(this);
		}

		void Update()
		{
			if (rootNode?.Evaluate() ?? false)
				rootNode.Execute();
		}

		void OnDestroy()
		{
			if (rootNode != null)
			{
				rootNode.Destroy();
				rootNode = null;
			}

			if (data != null)
			{
				data.Clear();
				data = null;
			}
		}

		#region 数据中心

		protected Dictionary<string, object> data = new Dictionary<string, object>();

		/// <summary>
		/// 设置数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void SetData<T>(string key, T value)
		{
			if (data.ContainsKey(key))
				data[key] = value;
			else
				data.Add(key, value);
		}

		/// <summary>
		/// 读取数据
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public T GetData<T>(string key)
		{
			if (data.ContainsKey(key))
				return (T) data[key];
			else
				return default(T);
		}

		#endregion
	}
}