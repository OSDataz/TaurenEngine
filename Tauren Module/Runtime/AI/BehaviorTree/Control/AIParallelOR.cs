/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/29 8:07:43
 *└────────────────────────┘*/

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 控制节点-并行节点（能执行的都执行）
	/// </summary>
	public sealed class AIParallelOR : AIAction
	{
		public override bool Execute()
		{
			if (!HasChildren)
				return false;

			bool result = false;
			foreach (var child in Children)
			{
				if (child.Evaluate())
				{
					child.Execute();
					result = true;
				}
			}

			return result;
		}
	}
}