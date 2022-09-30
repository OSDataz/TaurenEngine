/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/8/30 20:37:26
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine
{
	public static class DateHelper
	{
		/// <summary>
		/// 启动时间（秒）
		/// </summary>
		private static float startupTime;

		/// <summary>
		/// 程序启动时需调用
		/// </summary>
		public static void Startup()
		{
			// DateTime.UtcNow.Ticks 单位 100纳秒
			startupTime = DateTime.UtcNow.Ticks * 0.0000001f - Time.realtimeSinceStartup;

			DebugEx.Log("启动时间开始记录");
		}

		/// <summary>
		/// 自启动时间（支持子线程访问）
		/// </summary>
		public static float RealtimeSinceStartup => DateTime.UtcNow.Ticks * 0.0000001f - startupTime;
	}
}