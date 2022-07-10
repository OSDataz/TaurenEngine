/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:07:55
 *└────────────────────────┘*/

namespace TaurenEngine.AI
{
	/// <summary>
	/// 控制节点-顺序节点（依次执行，前一个执行完成再执行下一个）
	/// </summary>
	public sealed class AISequence : AIAction
	{
		public override bool Execute()
		{
			if (!HasChildren)
				return false;

			bool result = false;
			foreach (var child in Children)
			{
				if (child.Evaluate() && child.Execute())
					result = true;
				else
					break;
			}

			return result;
		}
	}
}