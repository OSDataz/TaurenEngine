/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/12 14:50:45
 *└────────────────────────┘*/

namespace Tauren.Core.Runtime
{
	public class DoublyLinkedNode<T>
	{
		public T Value { get; set; }

		/// <summary>
		/// 前一个节点
		/// </summary>
		public DoublyLinkedNode<T> Prev { get; internal set; }
		/// <summary>
		/// 下一个节点
		/// </summary>
		public DoublyLinkedNode<T> Next { get; internal set; }

		/// <summary>
		/// 添加到前一个节点
		/// </summary>
		/// <param name="node"></param>
		public void AddPrev(DoublyLinkedNode<T> node)
		{
			node.RemoveSelf();

			CreateLinkedList();

			if (Prev != null)
			{
				Prev.Next = node;
				node.Prev = Prev;
			}
			else
			{
				// 添加到头节点
				LinkedList.First = node;

				node.Prev = null;
			}

			node.LinkedList = LinkedList;
			Prev = node;
			node.Next = this;
		}

		/// <summary>
		/// 添加到后一个节点
		/// </summary>
		/// <param name="node"></param>
		public void AddNext(DoublyLinkedNode<T> node)
		{
			node.RemoveSelf();

			CreateLinkedList();

			if (Next != null)
			{
				Next.Prev = node;
				node.Next = Next;
			}
			else
			{
				// 添加到尾节点
				LinkedList.Last = node;

				node.Next = null;
			}

			node.LinkedList = LinkedList;
			Next = node;
			node.Prev = this;
		}

		/// <summary>
		/// 从列表中删除自身
		/// </summary>
		public void RemoveSelf()
		{
			if (Prev == null)
			{
				if (Next != null)
				{
					// 链表头节点
					LinkedList.First = Next;

					Next.Prev = null;
				}
			}
			else
			{
				if (Next != null)
				{
					Prev.Next = Next;
					Next.Prev = Prev;
				}
				else
				{
					// 链表尾节点
					LinkedList.Last = Prev;

					Prev.Next = null;
				}
			}

			ClearNode();
		}

		internal void ClearNode()
		{
			LinkedList = null;
			Prev = null;
			Next = null;
		}

		public void Clear()
		{
			RemoveSelf();

			Value = default(T);
		}

		#region 链表
		public DoublyLinkedList<T> LinkedList { get; internal set; }

		private void CreateLinkedList()
		{
			if (LinkedList != null)
				return;

			LinkedList = new DoublyLinkedList<T>();
			LinkedList.SetRoot(this);
		}
		#endregion
	}
}