/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/9/4 20:38:51
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Launch
{
	/// <summary>
	/// 异步加载数据
	/// </summary>
	public class LoadAsyncData
	{
		/// <summary> 加载优先级 </summary>
		public int priority;

		/// <summary> 加载完成回调 </summary>
		public Action onComplete;

		public void Clear()
		{
			priority = 0;
			onComplete = null;
		}
	}
}