/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/29 8:07:37
 *└────────────────────────┘*/

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 控制节点-并行节点（一起执行或都不执行）
	/// </summary>
	public sealed class AIParallelAnd : AIAction
	{
		public override bool Execute()
		{
			if (!HasChildren)
				return false;

			foreach (var child in Children)
			{
				if (!child.Evaluate())
					return false;
			}

			Children.ForEach(child => child.Execute());

			return true;
		}
	}
}