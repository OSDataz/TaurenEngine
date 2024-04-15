/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/12 14:50:18
 *└────────────────────────┘*/

using System;

namespace Tauren.Core.Runtime
{
	public class DoublyLinkedList<T>
	{
		/// <summary>
		/// 头节点
		/// </summary>
		public DoublyLinkedNode<T> First { get; internal set; }
		/// <summary>
		/// 尾节点
		/// </summary>
		public DoublyLinkedNode<T> Last { get; internal set; }

		internal void SetRoot(DoublyLinkedNode<T> node)
		{
			First = node;
			Last = node;

			node.LinkedList = this;
		}

		/// <summary>
		/// 添加到头节点
		/// </summary>
		/// <param name="node"></param>
		public void AddFirst(DoublyLinkedNode<T> node)
		{
			if (First == null)
			{
				SetRoot(node);
			}
			else
			{
				First.AddPrev(node);
			}
		}

		/// <summary>
		/// 添加到尾节点
		/// </summary>
		/// <param name="node"></param>
		public void AddLast(DoublyLinkedNode<T> node)
		{
			if (Last == null)
			{
				SetRoot(node);
			}
			else
			{
				Last.AddNext(node);
			}
		}

		/// <summary>
		/// 查找节点
		/// </summary>
		/// <param name="match"></param>
		/// <returns></returns>
		public DoublyLinkedNode<T> Find(Predicate<DoublyLinkedNode<T>> match)
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

		/// <summary>
		/// 清理链表
		/// </summary>
		public void Clear()
		{
			var node = First;
			DoublyLinkedNode<T> next;
			while (node != null)
			{
				next = node.Next;
				node.ClearNode();
				node = next;
			}

			First = null;
			Last = null;
		}
	}
}