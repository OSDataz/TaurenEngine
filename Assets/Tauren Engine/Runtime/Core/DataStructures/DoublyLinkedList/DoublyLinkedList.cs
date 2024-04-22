/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/12 14:50:18
 *└────────────────────────┘*/

using System;

namespace Tauren.Core.Runtime
{
	public class DoublyLinkedList
	{
		/// <summary> 头节点 </summary>
		public DoublyLinkedNode First { get; internal set; }
		/// <summary> 尾节点 </summary>
		public DoublyLinkedNode Last { get; internal set; }

		/// <summary> 链表长度 </summary>
		public int Count { get; internal set; }

		internal void SetRoot(DoublyLinkedNode node)
		{
			First = node;
			Last = node;

			node.LinkedList = this;
		}

		/// <summary>
		/// 添加到头节点
		/// </summary>
		/// <param name="node"></param>
		public void AddFirst(DoublyLinkedNode node)
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

		public void AddFirst(IDoublyLinkedNode node) => AddFirst(node.Node);

		/// <summary>
		/// 添加到尾节点
		/// </summary>
		/// <param name="node"></param>
		public void AddLast(DoublyLinkedNode node)
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

		public void AddLast(IDoublyLinkedNode node) => AddLast(node.Node);

		/// <summary>
		/// 查找节点
		/// </summary>
		/// <param name="match"></param>
		/// <param name="node"></param>
		/// <returns></returns>
		public bool Find(Predicate<DoublyLinkedNode> match, out DoublyLinkedNode node)
		{
			node = First;
			while (node != null)
			{
				if (match(node))
					return true;

				node = node.Next;
			}

			return false;
		}

		/// <summary>
		/// 清理链表
		/// </summary>
		public void Clear()
		{
			var node = First;
			DoublyLinkedNode next;
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