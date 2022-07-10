/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System;
using TaurenEngine.LocationEx;
using TaurenEngine.Mathematics;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// https://blog.csdn.net/flash099/article/details/53995468
    /// </summary>
    public class MathMapTitle
    {
        /// <summary>
        /// 将tile(瓦片)坐标系转换为LatLngt(地理)坐标系，pixelX，pixelY为图片偏移像素坐标
        /// </summary>
        /// <param name="tileX"></param>
        /// <param name="tileY"></param>
        /// <param name="zoom"></param>
        /// <param name="pixelX"></param>
        /// <param name="pixelY"></param>
        /// <returns></returns>
        public static Location TileToLocation(int tileX, int tileY, int zoom, int pixelX = 0, int pixelY = 0)
        {
            double size = Math.Pow(2, zoom);
            double pixelXToTileAddition = pixelX / 256.0;
            double lng = (tileX + pixelXToTileAddition) / size * 360.0 - 180.0;

            double pixelYToTileAddition = pixelY / 256.0;
            double lat = Math.Atan(Math.Sinh(Math.PI * (1 - 2 * (tileY + pixelYToTileAddition) / size))) * 180.0 / Math.PI;
            return new Location(lng, lat, 0, false);
        }

        public static int2 LocationToTile(Location loc, int zoom)
        {
            return LocationToTile(loc.longitude, loc.latitude, zoom);
        }

        /// <summary>
        /// 将LatLngt地理坐标系转换为tile瓦片坐标系，pixelX，pixelY为图片偏移像素坐标
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="zoom"></param>
        /// <returns></returns>
        public static int2 LocationToTile(double longitude, double latitude, int zoom)
        {
            double size = Math.Pow(2, zoom);
            double x = ((longitude + 180) / 360) * size;
            double latRad = latitude * Math.PI / 180;
            double y = (1 - Math.Log(Math.Tan(latRad) + 1 / Math.Cos(latRad)) / Math.PI) / 2;
            y = y * size;

            return new int2((int)x, (int)y);
        }
    }
}