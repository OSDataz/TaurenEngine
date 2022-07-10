/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/23 9:57:39
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public class EventObject : EventData<object>
	{
	}

	/// <summary>
	/// 通用事件参数
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EventData<T> : IEventData
	{
		public T Value { get; set; }
	}

	/// <summary>
	/// 消息事件参数必须继承该接口
	/// </summary>
	public interface IEventData { }
}