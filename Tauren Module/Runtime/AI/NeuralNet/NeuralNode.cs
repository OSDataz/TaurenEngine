/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/29 8:15:58
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 神经节点
	/// </summary>
	internal class NeuralNode<T> where T : class, INeuralInfo
	{
		private readonly Dictionary<string, NeuralBrige<T>> _briges = new Dictionary<string, NeuralBrige<T>>();

		public void AddNodeBrige(NeuralNode<T> node, float weight)
		{
			var brige = GetBrige(node);
			brige.Weight = weight;

			this.ExpectedValue += weight;
			node.ExpectedValue += weight;
		}

		/// <summary>
		/// 获取指定类型链接权重最高的神经节点（最多只查询三层）
		/// </summary>
		/// <param name="flag">特征标记</param>
		/// <returns></returns>
		public T GetInfoWeight(string flag)
		{
			NeuralBrige<T> brige = GetBrigeWeight(flag);

			if (brige == null)
			{
				foreach (var item in _briges)
				{
					FilterBrigeWeight(ref brige, item.Value.GetNode(this).GetBrigeWeight(flag));
				}

				if (brige == null)
				{
					foreach (var item in _briges)
					{
						var subNode = item.Value.GetNode(this);
						foreach (var subItem in subNode._briges)
						{
							FilterBrigeWeight(ref brige, subItem.Value.GetNode(subNode).GetBrigeWeight(flag));
						}
					}
				}
			}

			return brige?.GetNode(this).Info ?? null;
		}

		/// <summary>
		/// 获取指定类型正向期望最高的神经节点（最多只查询三层）
		/// </summary>
		/// <param name="flag">特征标记</param>
		/// <returns></returns>
		public T GetInfoPositive(string flag)
		{
			NeuralBrige<T> brige = GetBrigePositive(flag);

			if (brige == null)
			{
				foreach (var item in _briges)
				{
					FilterBrigePositive(ref brige, item.Value.GetNode(this).GetBrigePositive(flag));
				}

				if (brige == null)
				{
					foreach (var item in _briges)
					{
						var subNode = item.Value.GetNode(this);
						foreach (var subItem in subNode._briges)
						{
							FilterBrigePositive(ref brige, subItem.Value.GetNode(subNode).GetBrigePositive(flag));
						}
					}
				}
			}

			return brige?.GetNode(this).Info ?? null;
		}

		/// <summary>
		/// 获取指定类型负向期望的神经节点（最多只查询三层）
		/// </summary>
		/// <param name="flag">特征标记</param>
		/// <returns></returns>
		public T GetInfoNegative(string flag)
		{
			NeuralBrige<T> brige = GetBrigeNegative(flag);

			if (brige == null)
			{
				foreach (var item in _briges)
				{
					FilterBrigeNegative(ref brige, item.Value.GetNode(this).GetBrigeNegative(flag));
				}

				if (brige == null)
				{
					foreach (var item in _briges)
					{
						var subNode = item.Value.GetNode(this);
						foreach (var subItem in subNode._briges)
						{
							FilterBrigeNegative(ref brige, subItem.Value.GetNode(subNode).GetBrigeNegative(flag));
						}
					}
				}
			}

			return brige?.GetNode(this).Info ?? null;
		}

		/// <summary>
		/// 获取链接权重最高的神经节点（只查询一层）
		/// </summary>
		/// <returns></returns>
		public T GetInfoWeight()
		{
			return GetBrigeWeight()?.GetNode(this).Info ?? null;
		}

		/// <summary>
		/// 获取正向期望最高的神经节点（只查询一层）
		/// </summary>
		/// <returns></returns>
		public T GetInfoPositive()
		{
			return GetBrigePositive()?.GetNode(this).Info ?? null;
		}

		/// <summary>
		/// 获取负向期望最高的神经节点（只查询一层）
		/// </summary>
		/// <returns></returns>
		public T GetInfoNegative()
		{
			return GetBrigeNegative()?.GetNode(this).Info ?? null;
		}

		#region 神经链接

		private NeuralBrige<T> GetBrige(NeuralNode<T> node)
		{
			var brige = _briges[node.Type];
			if (brige == null)
			{
				brige = new NeuralBrige<T>();
				brige.NodeA = this;
				brige.NodeB = node;
				AddBrige(brige, node.Type);
				node.AddBrige(brige, Type);
			}

			return brige;
		}

		private void AddBrige(NeuralBrige<T> brige, string type)
		{
			_briges[type] = brige;
		}

		/// <summary>
		/// 筛选权重最高
		/// </summary>
		/// <param name="brige"></param>
		/// <param name="temp"></param>
		private void FilterBrigeWeight(ref NeuralBrige<T> brige, NeuralBrige<T> temp)
		{
			if (brige == null || temp.Weight > brige.Weight)
				brige = temp;
		}

		/// <summary>
		/// 筛选正向期望最高
		/// </summary>
		/// <param name="brige"></param>
		/// <param name="temp"></param>
		private void FilterBrigePositive(ref NeuralBrige<T> brige, NeuralBrige<T> temp)
		{
			if (brige == null || temp.ExpectedValue > brige.ExpectedValue)
				brige = temp;
		}

		/// <summary>
		/// 筛选负向期望最高
		/// </summary>
		/// <param name="brige"></param>
		/// <param name="temp"></param>
		private void FilterBrigeNegative(ref NeuralBrige<T> brige, NeuralBrige<T> temp)
		{
			if (brige == null || temp.ExpectedValue < brige.ExpectedValue)
				brige = temp;
		}

		private NeuralBrige<T> GetBrigeWeight()
		{
			NeuralBrige<T> brige = null;

			foreach (var item in _briges)
			{
				FilterBrigeWeight(ref brige, item.Value);
			}

			return brige;
		}

		private NeuralBrige<T> GetBrigeWeight(string flag)
		{
			NeuralBrige<T> brige = null;

			foreach (var item in _briges)
			{
				if (item.Key.Contains(flag))
				{
					FilterBrigeWeight(ref brige, item.Value);
				}
			}

			return brige;
		}

		private NeuralBrige<T> GetBrigePositive()
		{
			NeuralBrige<T> brige = null;

			foreach (var item in _briges)
			{
				FilterBrigePositive(ref brige, item.Value);
			}

			return brige;
		}

		private NeuralBrige<T> GetBrigePositive(string flag)
		{
			NeuralBrige<T> brige = null;

			foreach (var item in _briges)
			{
				if (item.Key.Contains(flag))
				{
					FilterBrigePositive(ref brige, item.Value);
				}
			}

			return brige;
		}

		private NeuralBrige<T> GetBrigeNegative()
		{
			NeuralBrige<T> brige = null;

			foreach (var item in _briges)
			{
				FilterBrigeNegative(ref brige, item.Value);
			}

			return brige;
		}

		private NeuralBrige<T> GetBrigeNegative(string flag)
		{
			NeuralBrige<T> brige = null;

			foreach (var item in _briges)
			{
				if (item.Key.Contains(flag))
				{
					FilterBrigeNegative(ref brige, item.Value);
				}
			}

			return brige;
		}
		#endregion

		#region 基础属性
		public T Info { get; set; }
		public string Type => Info.Type;

		/// <summary>
		/// 期望值
		/// </summary>
		public float ExpectedValue { get; private set; }
		#endregion
	}
}