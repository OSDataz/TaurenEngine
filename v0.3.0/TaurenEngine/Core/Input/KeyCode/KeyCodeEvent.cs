/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/23 16:31:59
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 注意：该数据对象是回收重复使用，不要缓存使用！异步处理时，可用Clone()克隆一个缓存
	/// </summary>
	public class KeyCodeEvent : IEventData
	{
		/// <summary>
		/// 当前触发按键Code
		/// </summary>
		public KeyCode keyCode;
		/// <summary>
		/// 运行时间，可用于检测是否是同时按下多个按键
		/// </summary>
		public float time = 0.0f;

		/// <summary>
		/// 克隆
		/// </summary>
		/// <returns></returns>
		public KeyCodeEvent Clone()
		{
			return new KeyCodeEvent()
			{
				keyCode = keyCode,
				time = time
			};
		}
	}
}