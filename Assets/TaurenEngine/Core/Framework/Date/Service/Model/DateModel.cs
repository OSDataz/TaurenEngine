/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/22 15:42:09
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine
{
	/// <summary>
	/// 存贮和获取时间有关的数据
	/// </summary>
	public class DateModel : ModelBase
	{
		private readonly DateTime StartDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		#region 设置服务器时间
		/// <summary> 服务器相对UTC0时区偏移(秒)，比如东八区偏移28800秒 </summary>
		private double _serverTimezoneOffset;
		/// <summary> 服务器UTC时间戳 </summary>
		private double _serverTimestampUtc;
		/// <summary> 客户端UTC时间戳 </summary>
		private double _clientTimestampUtc;
		/// <summary> 运行时间 </summary>
		private double _startRealtime;

		/// <summary>
		/// 设置服务器时间戳
		/// </summary>
		/// <param name="utcTimestamp">秒</param>
		public void SetServerTimestamp(double utcTimestamp)
		{
			_serverTimestampUtc = utcTimestamp;
			_clientTimestampUtc = GetClientTimestampUtc();
			_startRealtime = Time.realtimeSinceStartup;
		}

		/// <summary>
		/// 设置服务器区域
		/// </summary>
		/// <param name="offset"></param>
		public void SetServerTimezoneOffset(double offset)
		{
			_serverTimezoneOffset = offset;
		}
		#endregion

		#region 通用时间
		/// <summary>
		/// 【精准】将一个时间戳，转成 DateTime 对像
		/// </summary>
		/// <param name="timestamp">时间戳 秒</param>
		/// <returns></returns>
		public DateTime TimestampToDateTime(double timestamp)
			=> StartDateTime.AddSeconds(timestamp);

		/// <summary>
		/// 【精准】将DateTime对像转成为时间戳
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public double DateTimeToTimestamp(DateTime dateTime)
			=> (dateTime - StartDateTime).TotalSeconds;
		#endregion

		#region 客户端时间
		/// <summary>
		/// 【本地时间限制】获取当前客户端UTC时间戳
		/// </summary>
		/// <returns>当前时间戳 秒</returns>
		public double GetClientTimestampUtc()
			=> (DateTime.UtcNow - StartDateTime).TotalSeconds;

		/// <summary>
		/// 【本地时间限制】获取当前客户端时间戳
		/// </summary>
		/// <returns></returns>
		public double GetClientTimestamp()
			=> (DateTime.Now - StartDateTime).TotalSeconds;
		#endregion

		#region 服务器时间
		/// <summary>
		/// 【精准】获取当期服务器UTC时间戳
		/// </summary>
		/// <returns></returns>
		public double GetServerTimestampUtc()
			=> _serverTimestampUtc + Time.realtimeSinceStartup - _startRealtime;

		/// <summary>
		/// 【精准】获取当前服务器本地时间戳
		/// </summary>
		/// <returns></returns>
		public double GetServerTimestamp()
			=> GetServerTimestampUtc() + _serverTimezoneOffset;

		/// <summary>
		/// 【精准】获取当期服务器UTC时间
		/// </summary>
		/// <returns></returns>
		public DateTime GetServerDateTimeUtc()
			=> TimestampToDateTime(GetServerTimestampUtc());

		/// <summary>
		/// 【精准】获取当前服务器本地时间
		/// </summary>
		/// <returns></returns>
		public DateTime GetServerDateTime()
			=> TimestampToDateTime(GetServerTimestamp());

		/// <summary>
		/// 【精准】是否到达指定的服务器时间
		/// </summary>
		/// <param name="serverUtcTime"></param>
		/// <returns></returns>
		public bool IsReachedServerTime(long serverUtcTime)
			=> GetServerTimestampUtc() >= serverUtcTime;
		#endregion

		#region 服务器、客户端互转
		/// <summary>
		/// 【精准】客户端时间转服务器时间
		/// </summary>
		/// <param name="clientUtcDate"></param>
		/// <returns></returns>
		public DateTime ClientToServerUtcDate(DateTime clientUtcDate)
			=> TimestampToDateTime(DateTimeToTimestamp(clientUtcDate) + _serverTimestampUtc - _clientTimestampUtc);

		/// <summary>
		/// 【精准】服务器时间戳转客户端时间
		/// </summary>
		/// <param name="serverUtcTime">服务器时间戳</param>
		/// <returns></returns>
		public DateTime ServerToClientUtcDate(long serverUtcTime)
			=> TimestampToDateTime(serverUtcTime - _serverTimestampUtc + _clientTimestampUtc);

		/// <summary>
		/// 【精准】服务器时间戳 转 UTC时间
		/// </summary>
		/// <param name="serverTimestampUtc"></param>
		/// <returns></returns>
		public DateTime ToServerDateTimeUtc(long serverTimestampUtc)
			=> TimestampToDateTime(serverTimestampUtc);

		/// <summary>
		/// 【精准】服务器时间戳 转 本地时间
		/// </summary>
		/// <param name="serverTimestampUtc"></param>
		/// <returns></returns>
		public DateTime ToServerDateTime(long serverTimestampUtc)
			=> TimestampToDateTime(serverTimestampUtc + _serverTimezoneOffset);

		/// <summary>
		/// 【精准】服务器时间戳 转 客户端本地时间
		/// </summary>
		/// <param name="serverTimestampUtc"></param>
		/// <returns></returns>
		public DateTime ToClientDateTime(long serverTimestampUtc)
			=> ServerToClientUtcDate(serverTimestampUtc).ToLocalTime();

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