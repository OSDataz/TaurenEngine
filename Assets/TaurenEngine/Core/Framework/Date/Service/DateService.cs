/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/20 20:37:59
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine
{
	/// <summary>
	/// 时间/日期管理服务
	/// </summary>
	public class DateService : IDateService
	{
		/// <summary>
		/// 启动时间（秒）
		/// </summary>
		private float _startupTime;

		public DateService()
		{
			// DateTime.UtcNow.Ticks 单位 100纳秒
			_startupTime = DateTime.UtcNow.Ticks * 0.0000001f - Time.realtimeSinceStartup;

			Debug.Log("启动时间开始记录");
		}

		public float RealtimeSinceStartup => DateTime.UtcNow.Ticks * 0.0000001f - _startupTime;
	}
}