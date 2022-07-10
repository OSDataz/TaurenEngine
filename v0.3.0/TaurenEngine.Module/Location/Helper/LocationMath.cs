/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 10:07:50
 *└────────────────────────┘*/

using System;
using TaurenEngine.Mathematics;

namespace TaurenEngine.LocationEx
{
	/// <summary>
	/// https://blog.csdn.net/qq_40120946/article/details/100654873
	/// </summary>
	public partial class LocationMath
	{
        public static double pi = 3.1415926535897932384626;
        public static double xPi = 3.14159265358979324 * 3000.0 / 180.0;
        public static double a = 6378245.0;
        public static double ee = 0.00669342162296594323;

        public static double2 WGS84_To_GCJ02(double longitude, double latitude)
        {
            if (OutOfChina(longitude, latitude))
                return new double2(longitude, latitude);

            double dLat = TransformLat(longitude - 105.0, latitude - 35.0);
            double dLon = TransformLon(longitude - 105.0, latitude - 35.0);
            double radLat = latitude / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);

            latitude = latitude + dLat;
            longitude = longitude + dLon;

            return new double2(longitude, latitude);
        }

        public static double2 GCJ02_To_WGS84(double longitude, double latitude)
        {
            double2 gps = WGS84_To_GCJ02(longitude, latitude);

            gps.x = longitude * 2 - gps.x;
            gps.y = latitude * 2 - gps.y;
            return gps;
        }

        public static double2 GCJ02_To_BD09(double longitude, double latitude)
        {
            double z = Math.Sqrt(longitude * longitude + latitude * latitude) + 0.00002 * Math.Sin(latitude * xPi);
            double theta = Math.Atan2(latitude, longitude) + 0.000003 * Math.Cos(longitude * xPi);

            longitude = z * Math.Cos(theta) + 0.0065;
            latitude = z * Math.Sin(theta) + 0.006;

            return new double2(longitude, latitude);
        }

        public static double2 BD09_To_GCJ02(double longitude, double latitude)
        {
            longitude = longitude - 0.0065;
            latitude = latitude - 0.006;
            double z = Math.Sqrt(longitude * longitude + latitude * latitude) - 0.00002 * Math.Sin(latitude * xPi);
            double theta = Math.Atan2(latitude, longitude) - 0.000003 * Math.Cos(longitude * xPi);

            longitude = z * Math.Cos(theta);
            latitude = z * Math.Sin(theta);

            return new double2(longitude, latitude);
        }

        public static double2 WGS84_To_BD09(double longitude, double latitude)
        {
            double2 gcj02 = WGS84_To_GCJ02(longitude, latitude);
            double2 bd09 = GCJ02_To_BD09(gcj02.x, gcj02.y);
            return bd09;
        }
        public static double2 BD09_To_WGS84(double longitude, double latitude)
        {
            double2 gcj02 = BD09_To_GCJ02(longitude, latitude);
            double2 gps84 = GCJ02_To_WGS84(gcj02.x, gcj02.y);
            gps84.x = Math.Round(gps84.x, 6);
            gps84.y = Math.Round(gps84.y, 6);
            return gps84;
        }

        public static bool OutOfChina(double longitude, double latitude)
        {
            return longitude < 72.004 || longitude > 137.8347
                                      || latitude < 0.8293 || latitude > 55.8271;
        }

        private static double TransformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y
                         + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private static double TransformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1
                * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0
                                                                       * pi)) * 2.0 / 3.0;
            return ret;

        }
    }
}