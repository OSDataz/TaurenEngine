/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:07:27
 *└────────────────────────┘*/

namespace TaurenEngine.AI
{
	/// <summary>
	/// 条件节点（注意：1.该节点只能用于前提条件节点；2.Execute只能写判断逻辑，禁止写执行逻辑）
	/// </summary>
	public abstract class AICondition : AINode
	{
		internal override bool Evaluate()
		{
			if (!HasChildren)
				return false;

			foreach (var child in Children)
			{
				if (!child.Evaluate() || !child.Execute())
					return false;
			}

			return true;
		}
	}
}