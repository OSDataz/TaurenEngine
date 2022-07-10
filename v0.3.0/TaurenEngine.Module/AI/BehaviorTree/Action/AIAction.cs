/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:06:37
 *└────────────────────────┘*/

namespace TaurenEngine.AI
{
	/// <summary>
	/// 执行节点
	/// 
	/// 注意：
	/// 1.判断逻辑禁止写在Execute，使用前提条件节点precondition；
	/// 2.正常情况下执行节点不会有子节点，如有执行需自行调用ExecuteChildren
	/// </summary>
	public abstract class AIAction : AINode
	{
		public AINode Precondition { get; set; }

		internal override void Activate(IAIData data)
		{
			base.Activate(data);

			Precondition?.Activate(data);
		}

		internal override bool Evaluate()
		{
			return Precondition == null || (Precondition.Evaluate() && Precondition.Execute());
		}

		/// <summary>
		/// 执行子节点，正常情况下执行节点即为叶节点，不会再有子节点。如有特殊情况可使用
		/// </summary>
		public virtual void ExecuteChildren()
		{
			if (!HasChildren)
				return;

			foreach (var child in Children)
			{
				if (child.Evaluate())
					child.Execute();
			}
		}

		public override void Clear()
		{
			Precondition?.Clear();

			base.Clear();
		}

		public override void Destroy()
		{
			if (Precondition != null)
			{
				Precondition.Destroy();
				Precondition = null;
			}

			base.Destroy();
		}
	}
}