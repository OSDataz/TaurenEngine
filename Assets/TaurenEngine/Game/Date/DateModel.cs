/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/22 15:42:09
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Game
{
	/// <summary>
	/// 存贮和获取时间有关的数据
	/// </summary>
	public class DateModel : ModelBase
	{
		/// <summary> 服务器相对UTC0时区偏移(秒)，比如东八区偏移28800秒 </summary>
		public double serverTimezoneOffset;
		/// <summary> 服务器UTC时间戳 </summary>
		public double serverTimestampUtc;
		/// <summary> 客户端UTC时间戳 </summary>
		public double clientTimestampUtc;
		/// <summary> 运行时间 </summary>
		public double startRealtime;

		#region 服务器时间
		/// <summary>
		/// 【精准】获取当期服务器UTC时间戳
		/// </summary>
		/// <returns></returns>
		public double GetServerUtcTimestamp()
			=> serverTimestampUtc + Time.realtimeSinceStartup - startRealtime;

		/// <summary>
		/// 【精准】获取当前服务器本地时间戳
		/// </summary>
		/// <returns></returns>
		public double GetServerTimestamp()
			=> GetServerUtcTimestamp() + serverTimezoneOffset;

		/// <summary>
		/// 【精准】获取当期服务器UTC时间
		/// </summary>
		/// <returns></returns>
		public DateTime GetServerUtcDateTime()
			=> DateUtils.TimestampToDateTime(GetServerUtcTimestamp());

		/// <summary>
		/// 【精准】获取当前服务器本地时间
		/// </summary>
		/// <returns></returns>
		public DateTime GetServerDateTime()
			=> DateUtils.TimestampToDateTime(GetServerTimestamp());

		/// <summary>
		/// 【精准】是否到达指定的服务器时间
		/// </summary>
		/// <param name="serverUtcTimestamp"></param>
		/// <returns></returns>
		public bool IsReachedServerTime(long serverUtcTimestamp)
			=> GetServerUtcTimestamp() >= serverUtcTimestamp;
		#endregion

		#region 服务器、客户端互转
		/// <summary>
		/// 【精准】客户端时间转服务器时间
		/// </summary>
		/// <param name="clientUtcDate"></param>
		/// <returns></returns>
		public DateTime ClientToServerUtcDateTime(DateTime clientUtcDate)
			=> DateUtils.TimestampToDateTime(DateUtils.DateTimeToTimestamp(clientUtcDate) + serverTimestampUtc - clientTimestampUtc);

		/// <summary>
		/// 【精准】服务器时间戳转客户端时间
		/// </summary>
		/// <param name="serverUtcTimestamp">服务器时间戳</param>
		/// <returns></returns>
		public DateTime ServerToClientUtcDateTime(long serverUtcTimestamp)
			=> DateUtils.TimestampToDateTime(serverUtcTimestamp - serverTimestampUtc + clientTimestampUtc);

		/// <summary>
		/// 【精准】服务器时间戳 转 UTC时间
		/// </summary>
		/// <param name="serverTimestampUtc"></param>
		/// <returns></returns>
		public DateTime ToServerUtcDateTime(long serverTimestampUtc)
			=> DateUtils.TimestampToDateTime(serverTimestampUtc);

		/// <summary>
		/// 【精准】服务器时间戳 转 本地时间
		/// </summary>
		/// <param name="serverTimestampUtc"></param>
		/// <returns></returns>
		public DateTime ToServerDateTime(long serverTimestampUtc)
			=> DateUtils.TimestampToDateTime(serverTimestampUtc + serverTimezoneOffset);

		/// <summary>
		/// 【精准】服务器时间戳 转 客户端本地时间
		/// </summary>
		/// <param name="serverTimestampUtc"></param>
		/// <returns></returns>
		public DateTime ToClientDateTime(long serverTimestampUtc)
			=> ServerToClientUtcDateTime(serverTimestampUtc).ToLocalTime();

		/// <summary>
		/// 【精准】获取服务器本地距离零点的剩余时间(秒)
		/// </summary>
		/// <returns></returns>
		public double GetServerDurationToZero()
		{
			var local = GetServerDateTime();
			var zero = new DateTime(local.Year, local.Month, local.Day).AddDays(1);// 第二天0点
			return (zero - local).TotalSeconds;
		}
		#endregion
	}
}