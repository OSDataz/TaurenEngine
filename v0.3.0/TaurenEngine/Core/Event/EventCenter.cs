/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/5 22:36:03
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 事件中心
	/// </summary>
	public static class EventCenter
	{
		public static readonly EventDispatcher<string> Dispatcher = new EventDispatcher<string>();
	}
}