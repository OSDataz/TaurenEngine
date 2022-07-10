/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;
using TaurenEngine.LocationEx;
using UnityEngine;

namespace TaurenEngine.AR
{
    /// <summary>
    /// 计算太阳方位
    /// </summary>
    public class MathHelper
    {
        /// <summary>
        /// 获取太阳当前空间坐标
        /// https://blog.csdn.net/weixin_43108465/article/details/100660676
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetPositionSun()
        {
	        var gpsLocation = ARLocation.Instance.GpsLocation;

            if (!gpsLocation.IsReady)
                return Vector3.zero;

            var loc = GetLocationSun();
            var pos = gpsLocation.ToPositionRoot(loc);

            double latitude = gpsLocation.Location.latitude * Mathf.Deg2Rad;
            double degangle = loc.latitude * Mathf.Deg2Rad;
            double timeangle = (DateTime.Now.TimeOfDay.TotalHours - 12.0) * 15.0;// 时角

            double hangle = Math.Asin(Math.Sin(latitude) * Math.Sin(degangle) +
                                      Math.Cos(latitude) * Math.Cos(degangle) * Math.Cos(timeangle)) * Mathf.Rad2Deg;// 太阳高度角

            pos.y = (float)(Math.Abs(Math.Sin(hangle)) * Math.Sqrt(pos.x * pos.x + pos.z * pos.z));

            TDebug.Log("估算太阳照射坐标：" + pos);

            return pos;
        }

        /// <summary>
        /// 获取太阳当前时间直射点的经纬度
        /// https://www.doc88.com/p-376366008511.html
        /// </summary>
        /// <returns></returns>
        public static Location GetLocationSun()
        {
            var gpsLocation = ARLocation.Instance.GpsLocation;
            Location loc = new Location();
            if (!gpsLocation.IsReady)
                return loc;

            Location curLoc = gpsLocation.Location;
            DateTime date = DateTime.Now;
            double value;

            // 计算经度
            if (curLoc.longitude >= 0.0f)
            {
                value = curLoc.longitude + (12.0 - date.TimeOfDay.TotalHours) * 15.0;
                if (value > 180.0)
                    value -= 360.0;
            }
            else
            {
                value = curLoc.longitude - (date.TimeOfDay.TotalHours - 12.0) * 15.0;
                if (value < -180.0)
                    value += 360.0;
            }

            loc.longitude = value;

            // 计算纬度
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            int leap = (year % 4 == 0 && year % 100 != 0) || year % 400 == 0 ? 1 : 0;
            int[] mm = new int[12] { 0, 31, leap + 59, leap + 90, leap + 120, leap + 151, leap + 181, leap + 212, leap + 243, leap + 273, leap + 304, leap + 334 };// 积日

            double tick = mm[month - 1] + day - 79.6764 - 0.2422 * (year - 1985) + (int)((year - 1985) / 4);
            double sita = 2 * 3.14159265 * tick / 365.2422;// 日角
            loc.latitude = 0.3723 + 23.2567 * Math.Sin(sita)
                                           + 0.1149 * Math.Sin(2 * sita) - 0.1712 * Math.Sin(3 * sita)
                                           - 0.758 * Math.Cos(sita) + 0.3656 * Math.Cos(2 * sita)
                                           + 0.0201 * Math.Cos(3 * sita);

            TDebug.Log("估算太阳照射GPS定位：" + loc.ToString());

            return loc;
        }
    }
}