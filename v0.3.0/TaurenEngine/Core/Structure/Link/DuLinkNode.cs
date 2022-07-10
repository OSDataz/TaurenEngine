/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/26 11:55:07
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 双向链表节点
	/// </summary>
	public class DuLinkNode<T>
	{
		public T data;

		/// <summary>
		/// 前一个节点
		/// </summary>
		public DuLinkNode<T> Prior { get; protected set; }
		/// <summary>
		/// 下一个节点
		/// </summary>
		public DuLinkNode<T> Next { get; protected set; }

		public DuLinkNode(T data)
		{
			this.data = data;
		}

		public void Clear()
		{
			data = default(T);

			RemoveSelf();
		}

		private void ClearNode()
		{
			_list = null;

			Prior = null;
			Next = null;
		}

		#region 链表
		private DuLinkList<T> _list;
		/// <summary>
		/// 链表
		/// </summary>
		public DuLinkList<T> List
		{
			get
			{
				if (_list == null)
				{
					_list = new DuLinkList<T>();
					_list.First = this;
					_list.Last = this;
				}
				return _list;
			}
			set => _list = value;
		}

		internal void SetRootList(DuLinkList<T> list)
		{
			RemoveSelf();

			_list = list;
			_list.First = this;
			_list.Last = this;
		}

		private void ClearList()
		{
			if (_list == null)
				return;

			_list.First = null;
			_list.Last = null;
		}
		#endregion

		public void AddPriorNode(DuLinkNode<T> node)
		{
			if (Prior != null)
			{
				Prior.Next = node;
				node.Prior = Prior;
			}
			else
			{
				// 添加到头节点
				List.First = node;

				node.Prior = null;
			}

			node.List = List;
			Prior = node;
			node.Next = this;
		}

		public void AddNextNode(DuLinkNode<T> node)
		{
			if (Next != null)
			{
				Next.Prior = node;
				node.Next = Next;
			}
			else
			{
				// 添加到尾节点
				List.Last = node;

				node.Next = null;
			}

			node.List = List;
			Next = node;
			node.Prior = this;
		}

		/// <summary>
		/// 移除前一个节点
		/// </summary>
		public void RemovePriorNode()
			=> Prior?.RemoveSelf();

		/// <summary>
		/// 移除后一个节点
		/// </summary>
		public void RemoveNextNode()
			=> Next?.RemoveSelf();

		public void RemoveSelf()
		{
			if (Prior == null)
			{
				if (Next != null)
				{
					// 链表头节点
					List.First = Next;

					Next.Prior = null;
				}
				else
				{
					// 链表根节点
					ClearList();
				}
			}
			else
			{
				if (Next != null)
				{
					Prior.Next = Next;
					Next.Prior = Prior;
				}
				else
				{
					// 链表尾节点
					List.Last = Prior;

					Prior.Next = null;
				}
			}

			ClearNode();
		}
	}
}