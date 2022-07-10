/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 10:18:10
 *└────────────────────────┘*/

using System;
using System.Runtime.CompilerServices;
using TaurenEngine.Core;
using TaurenEngine.Mathematics;
using UnityEngine;

namespace TaurenEngine.LocationEx
{
    /*
     *        北 Z轴 纬度（手机启动时默认朝向）
     *            |
     *            |
     *            |
     *            |
     * 西————原点————东 X轴 经度（右侧）
     *            |
     *            |
     *            |
     *            |
     *            南
     *
     * Unity AR场景坐标系，1单位就是1米
     * 不使用float，避免经度丢失
     */
    public struct Location
    {
        /// <summary>
        /// 经度 X
        /// </summary>
        public double longitude;
        /// <summary>
        /// 纬度 Z
        /// </summary> 
        public double latitude;
        /// <summary>
        /// 海拔 Y
        /// </summary>
        public double altitude;

        /// <summary>
        /// 忽视海拔。海拔数据是相对高度值，不是绝对海拔值
        /// </summary>
        public bool ignoreAltitude;

        public Location(double longitude, double latitude, double altitude, bool ignoreAltitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
            this.altitude = altitude;
            this.ignoreAltitude = ignoreAltitude;
        }

        public Location Clone()
        {
            return new Location()
            {
                longitude = longitude,
                latitude = latitude,
                altitude = altitude,
                ignoreAltitude = ignoreAltitude
            };
        }

        public bool Equals(Location loc)
        {
            return this == loc;
        }

        public bool EqualIgnore(Location loc)
        {
            return (Math.Abs(longitude - loc.longitude)
                    + Math.Abs(latitude - loc.latitude)).Equals(0);
        }

        public override bool Equals(object loc)
        {
            return loc is Location loc1 && Equals(loc1);
        }

        public override int GetHashCode()
        {
            return longitude.GetHashCode() ^ latitude.GetHashCode() << 2 ^ altitude.GetHashCode() >> 2 ^
                   ignoreAltitude.GetHashCode() >> 1;
        }

        public override string ToString()
        {
            return $"({longitude}, {latitude}, {altitude}, {ignoreAltitude})";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double3(Location val) => new double3(val.longitude, val.altitude, val.latitude);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double2(Location val) => new double2(val.longitude, val.latitude);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(Location val) => new Vector3((float)val.longitude, (float)val.altitude, (float)val.latitude);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2(Location val) => new Vector2((float)val.longitude, (float)val.latitude);

        #region 静态函数
        public static bool operator ==(Location a, Location b)
        {
            return (Math.Abs(a.longitude - b.longitude)
                + Math.Abs(a.latitude - b.latitude)
                + Math.Abs(a.altitude - b.altitude)).Equals(0)
                   && (a.ignoreAltitude == b.ignoreAltitude);
        }

        public static bool operator !=(Location a, Location b)
        {
            return !(a == b);
        }

        public static double Distance(Location a, Location b)
        {
            double v1 = a.longitude - b.longitude;
            double v2 = a.latitude - b.latitude;
            double v3 = a.altitude - b.altitude;
            return Math.Sqrt(v1 * v1 + v2 * v2 + v3 * v3);
        }

        public static double Distance2(Location a, Location b)
        {
            double v1 = a.longitude - b.longitude;
            double v2 = a.latitude - b.latitude;
            return Math.Sqrt(v1 * v1 + v2 * v2);
        }
        #endregion

        #region 计算公式-简式
        /// <summary>
        /// 米转经度
        /// </summary>
        /// <param name="meter"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public static double MeterToLongitude(double meter, double latitude)
        {
            return meter * 0.000008983152841195214 / Math.Cos(MathHelper.DegreesToRadians(latitude));
        }

        /// <summary>
        /// 米转纬度
        /// </summary>
        /// <param name="meter"></param>
        /// <returns></returns>
        public static double MeterToLatitude(double meter)
        {
            return meter * 0.000008983152841195214;
        }

        /// <summary>
        /// 经度转为米
        /// </summary>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static double LongitudeToMeter(double longitude, double latitude)
        {
            return longitude * Math.Cos(MathHelper.DegreesToRadians(latitude)) * 111319.4907932736;
        }

        /// <summary>
        /// 纬度转为米
        /// </summary>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public static double LatitudeToMeter(double latitude)
        {
            return latitude * 111319.4907932736;
        }

        /// <summary>
        /// 将游戏对象的空间坐标转化为GPS经纬度位置（粗略计算，后续时间充裕再整理公式）
        /// https://blog.csdn.net/u012539364/article/details/74059679
        /// </summary>
        /// <param name="userLoc">相对根GPS坐标</param>
        /// <param name="moveVec">相对根GPS坐标的相对3D空间矢量</param>
        /// <returns></returns>
        public static Location GetLocationForPosition(Location userLoc, Vector3 moveVec)
        {
            var objectLoc = new Location()
            {
                latitude = userLoc.latitude + MeterToLatitude(moveVec.z),
                altitude = moveVec.y,
                ignoreAltitude = userLoc.ignoreAltitude
            };

            objectLoc.longitude = userLoc.longitude + MeterToLongitude(moveVec.x, (objectLoc.latitude + userLoc.latitude) * 0.5);
            return objectLoc;
        }

        /// <summary>
        /// 将游戏对象的空间坐标转化为GPS经纬度位置（粗略计算，后续时间充裕再整理公式）
        /// </summary>
        /// <param name="userLoc"></param>
        /// <param name="userPos"></param>
        /// <param name="objectPos"></param>
        /// <returns></returns>
        public static Location GetLocationForPosition(Location userLoc, Vector3 userPos, Vector3 objectPos)
        {
            return GetLocationForPosition(userLoc, objectPos - userPos);
        }

        public static Vector3 GetPositionForLocation(Location loc)
        {
            return new Vector3()
            {
                x = (float)LongitudeToMeter(loc.longitude, loc.latitude),
                z = (float)LatitudeToMeter(loc.latitude),
                y = loc.ignoreAltitude ? 0.0f : (float)loc.altitude
            };
        }

        public static Vector3 GetPositionForLocation(double longitude, double latitude)
        {
            return new Vector3()
            {
                x = (float)LongitudeToMeter(longitude, latitude),
                z = (float)LatitudeToMeter(latitude)
            };
        }
        #endregion

        #region 计算公式-Haversine
        /// <summary>
        /// 获取游戏对象的位置世界位置。
        /// </summary>
        /// <param name="userPos"></param>
        /// <param name="userLoc"></param>
        /// <param name="objectLoc"></param>
        /// <returns></returns>
        public static Vector3 GetPositionForLocation(Transform userPos, Location userLoc, Location objectLoc)
        {
            return GetPositionForLocation(userPos.position, userLoc, objectLoc);
        }

        /// <summary>
        /// 获取游戏对象的位置世界位置。
        /// </summary>
        /// <param name="userPos"></param>
        /// <param name="userLoc"></param>
        /// <param name="objectLoc"></param>
        /// <returns></returns>
        public static Vector3 GetPositionForLocation(Vector3 userPos, Location userLoc, Location objectLoc)
        {
            var dVec = VectorFromTo(userLoc, objectLoc);
            var objectPos = userPos;
            objectPos.x += (float)dVec.x;
            objectPos.z += (float)dVec.z;

            if (!objectLoc.ignoreAltitude && userLoc.ignoreAltitude)
                objectPos.y = (float)objectLoc.altitude;
            else
                objectPos.y += (float)dVec.y;

            return objectPos;
        }

        /// <summary>
        /// 考虑海拔高度，计算从l1到l2的矢量（以米为单位）。
        /// </summary>
        /// <returns>The from to.</returns>
        /// <param name="l1">L1.</param>
        /// <param name="l2">L2.</param>
        private static double3 VectorFromTo(Location l1, Location l2)
        {
            var horizontal = HorizontalVectorFromTo(l1, l2);
            var height = l2.altitude - l1.altitude;

            return new double3(
                horizontal.x,
                l1.ignoreAltitude == l2.ignoreAltitude ? l2.altitude - l1.altitude : 0,
                horizontal.y);
        }

        /// <summary>
        /// 计算从l1到l2的水平矢量，以米为单位。
        /// </summary>
        /// <returns>The vector from to.</returns>
        /// <param name="l1">L1.</param>
        /// <param name="l2">L2.</param>
        private static double2 HorizontalVectorFromTo(Location l1, Location l2)
        {
            var d = HaversineDistance(l1, l2);

            var direction = ((double2)l2 - (double2)l1).Normalized;
            direction.x = direction.x * d;
            direction.y = direction.y * d;

            return direction;
        }

        /// <summary>
        /// 使用Haversine公式的水平距离。
        /// https://stackoverflow.com/questions/41621957/a-more-efficient-haversine-function
        /// </summary>
        /// <returns>The distance, in meters.</returns>
        /// <param name="l1">L1.</param>
        /// <param name="l2">L2.</param>
        public static double HaversineDistance(Location l1, Location l2)
        {
            var r = 6372.8;// 地球半径 km
            var rad = Math.PI / 180;
            var dLat = (l2.latitude - l1.latitude) * rad;
            var dLon = (l2.longitude - l1.longitude) * rad;
            var lat1 = l1.latitude * rad;
            var lat2 = l2.latitude * rad;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);

            return r * 2 * Math.Asin(Math.Sqrt(a)) * 1000;
        }
        #endregion
    }
}