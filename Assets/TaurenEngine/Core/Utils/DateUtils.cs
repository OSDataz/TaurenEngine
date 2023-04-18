/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/17 21:51:53
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Core
{
	public static class DateUtils
	{
		private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		/// <summary>
		/// DateTime转化为时间戳
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static double DateTimeToTimestamp(DateTime dateTime)
		{
			return (dateTime - UnixEpoch).TotalSeconds;
		}

		/// <summary>
		/// 时间戳转化为DateTime
		/// </summary>
		/// <param name="timestamp"></param>
		/// <returns></returns>
		public static DateTime TimestampToDateTime(double timestamp)
		{
			return UnixEpoch.AddSeconds(timestamp);
		}

		/// <summary>
		/// 本地UTC时间戳
		/// </summary>
		/// <returns></returns>
		public static double UtcNowTimestamp()
		{
			return (DateTime.UtcNow - UnixEpoch).TotalSeconds;
		}

		/// <summary>
		/// 本地时间戳
		/// </summary>
		/// <returns></returns>
		public static double NowTimestamp()
		{
			return (DateTime.Now - UnixEpoch).TotalSeconds;
		}
	}
}