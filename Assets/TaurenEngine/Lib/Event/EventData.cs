/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/27 20:31:19
 *└────────────────────────┘*/

namespace TaurenEngine.Lib.Event
{
	/// <summary>
	/// 通用事件参数
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EventData<T> : IEventData
	{
		public T Value { get; set; }
	}

	public class EventObject : EventData<object> { }

	/// <summary>
	/// 消息事件参数必须继承该接口
	/// </summary>
	public interface IEventData { }
}