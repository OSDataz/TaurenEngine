/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/23 16:32:10
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 注意：该数据对象是回收重复使用，不要缓存使用！可用Clone()克隆一个缓存
	/// </summary>
	public class TouchEvent : IEventData
	{
		public List<Touch> touchs;

		public TouchEvent Clone()
		{
			return new TouchEvent()
			{
				touchs = new List<Touch>(touchs)
			};
		}
	}
}