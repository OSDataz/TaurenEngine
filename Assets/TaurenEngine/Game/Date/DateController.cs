/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/16 22:31:42
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Game
{
	public class DateController : ControllerBase
	{
		#region 设置服务器时间
		/// <summary>
		/// 设置服务器时间戳
		/// </summary>
		/// <param name="utcTimestamp">秒</param>
		public void SetServerTimestamp(double utcTimestamp)
		{
			var model = GetModel<DateModel>();

			model.serverTimestampUtc = utcTimestamp;
			model.clientTimestampUtc = DateUtils.UtcNowTimestamp();
			model.startRealtime = Time.realtimeSinceStartup;
		}

		/// <summary>
		/// 设置服务器区域
		/// </summary>
		/// <param name="offset">秒</param>
		public void SetServerTimezoneOffset(double offset)
		{
			var model = GetModel<DateModel>();

			model.serverTimezoneOffset = offset;
		}
		#endregion
	}
}