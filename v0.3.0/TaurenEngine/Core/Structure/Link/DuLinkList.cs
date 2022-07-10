/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/26 11:54:33
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 双向链表
	/// </summary>
	public class DuLinkList<T>
	{
		/// <summary>
		/// 头节点
		/// </summary>
		public DuLinkNode<T> First { get; internal set; }
		/// <summary>
		/// 尾节点
		/// </summary>
		public DuLinkNode<T> Last { get; internal set; }

		/// <summary>
		/// 链表设置首个根节点
		/// </summary>
		/// <param name="node"></param>
		public void SetRootNode(DuLinkNode<T> node)
		{
			Clear();

			node.SetRootList(this);
		}

		public void Clear()
		{
			var node = First;
			DuLinkNode<T> next;
			while (node != null)
			{
				next = node.Next;
				node.Clear();
				node = next;
			}

			First = null;
			Last = null;
		}

		public DuLinkNode<T> Find(Predicate<DuLinkNode<T>> match)
		{
			var node = First;
			while (node != null)
			{
				if (match(node))
					return node;

				node = node.Next;
			}

			return null;
		}
	}
}