/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:15:42
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.AI
{
	/// <summary>
	/// 神经链接
	/// </summary>
	internal class NeuralBrige<T> where T : class, INeuralInfo
	{
		public NeuralNode<T> NodeA { private get; set; }
		public NeuralNode<T> NodeB { private get; set; }

		private float _weight = 1.0f;
		/// <summary>
		/// 链接权重
		/// </summary>
		public float Weight
		{
			get => _weight;
			set
			{
				_weight *= Mathf.Abs(value);
				ExpectedValue += value;
			}
		}

		/// <summary>
		/// 期望值
		/// </summary>
		public float ExpectedValue { get; private set; }
		/// <summary>
		/// 获取当前节点链接的另一个节点
		/// </summary>
		/// <param name="curNode"></param>
		/// <returns></returns>
		public NeuralNode<T> GetNode(NeuralNode<T> curNode)
		{
			return curNode.Type == NodeA.Type ? NodeB : NodeA;
		}
	}
}