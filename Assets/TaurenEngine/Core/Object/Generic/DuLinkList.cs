/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/15 10:22:49
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 双向链表
	/// <para>注意：链表节点不能同时被多个链表使用</para>
	/// </summary>
	public class DuLinkList<T> where T : DuLinkNode<T>
	{
		/// <summary> 第一个节点 </summary>
		public T First { get; private set; }
		/// <summary> 最后一个节点 </summary>
		public T Last { get; private set; }

		public void Add(T node)
		{
			Remove(node);

			if (First == null)
			{
				First = Last = node;
				return;
			}

			AddToNext(node, Last);
			Last = node;
		}

		public void AddToFirst(T node)
		{
			Remove(node);

			if (First == null)
			{
				First = Last = node;
				return;
			}

			AddToPrior(node, First);
			First = node;
		}

		public void AddToPrior(T node, T toNode)
		{
			Remove(node);

			if (toNode.Prior != null) toNode.Prior.Next = node;
			node.Prior = toNode.Prior;
			toNode.Prior = node;
			node.Next = toNode;
		}

		public void AddToNext(T node, T toNode)
		{
			Remove(node);

			if (toNode.Next != null) toNode.Next.Prior = node;
			node.Next = toNode.Next;
			toNode.Next = node;
			node.Prior = toNode;
		}

		public void Remove(T node)
		{
			if (node == First)
			{
				if (node.Next != null)
				{
					First = node.Next;
					node.Next = null;
				}
				else
					First = Last = null;
			}
			else if (node == Last)
			{
				if (node.Prior != null)
				{
					Last = node.Prior;
					node.Prior = null;
				}
				else
					First = Last = null;
			}
			else
			{
				if (node.Prior != null) node.Prior.Next = node.Next;
				if (node.Next != null) node.Next.Prior = node.Prior;
			}
		}

		public T RemoveFirst()
		{
			if (First == null) return null;

			var node = First;
			Remove(node);
			return node;
		}

		public T RemoveLast()
		{
			if (Last == null) return null;

			var node = Last;
			Remove(node);
			return node;
		}
	}

	public abstract class DuLinkNode<T> where T : DuLinkNode<T>
	{
		/// <summary> 前一个节点 </summary>
		public T Prior { get; internal set; }
		/// <summary> 后一个节点 </summary>
		public T Next { get; internal set; }
	}
}