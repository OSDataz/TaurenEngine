/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:07:49
 *└────────────────────────┘*/

namespace TaurenEngine.AI
{
	/// <summary>
	/// 控制节点-选择节点（依次执行，有一个执行的即可）
	/// </summary>
	public sealed class AISelector : AIAction
	{
		public override bool Execute()
		{
			if (!HasChildren)
				return false;

			foreach (var child in Children)
			{
				if (child.Evaluate())
				{
					child.Execute();
					return true;
				}
			}

			return false;
		}
	}
}