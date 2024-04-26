/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/10 9:52:20
 *└────────────────────────┘*/

namespace Tauren.Framework.Runtime
{
	public interface IExecuteItem
	{
		/// <summary> 优先级，数值越大越先执行 </summary>
		public int Priority { get; }
	}
}