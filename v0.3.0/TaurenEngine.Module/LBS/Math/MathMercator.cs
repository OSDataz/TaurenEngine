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
    /// 墨卡托地图瓦片算法公式
    /// 
    /// https://www.cnblogs.com/guowq92/p/12988337.html
    /// https://www.cnblogs.com/xiaozhi_5638/p/4748186.html
    /// </summary>
    public class MathMercator
    {
        //以下是根据百度地图JavaScript API破解得到 百度坐标<->墨卡托坐标 转换算法
        private static readonly double[] Array1 = { 75, 60, 45, 30, 15, 0 };
        private static readonly double[] Array3 = { 12890594.86, 8362377.87, 5591021, 3481989.83, 1678043.12, 0 };
        private static readonly double[][] Array2 = {new double[]{-0.0015702102444, 111320.7020616939, 1704480524535203D, -10338987376042340D, 26112667856603880D, -35149669176653700D, 26595700718403920D, -10725012454188240D, 1800819912950474D, 82.5}
            , new double[]{0.0008277824516172526, 111320.7020463578, 647795574.6671607, -4082003173.641316, 10774905663.51142, -15171875531.51559, 12053065338.62167, -5124939663.577472, 913311935.9512032, 67.5}
            , new double[]{0.00337398766765, 111320.7020202162, 4481351.045890365, -23393751.19931662, 79682215.47186455, -115964993.2797253, 97236711.15602145, -43661946.33752821, 8477230.501135234, 52.5}
            , new double[]{0.00220636496208, 111320.7020209128, 51751.86112841131, 3796837.749470245, 992013.7397791013, -1221952.21711287, 1340652.697009075, -620943.6990984312, 144416.9293806241, 37.5}
            , new double[]{-0.0003441963504368392, 111320.7020576856, 278.2353980772752, 2485758.690035394, 6070.750963243378, 54821.18345352118, 9540.606633304236, -2710.55326746645, 1405.483844121726, 22.5}
            , new double[]{-0.0003218135878613132, 111320.7020701615, 0.00369383431289, 823725.6402795718, 0.46104986909093, 2351.343141331292, 1.58060784298199, 8.77738589078284, 0.37238884252424, 7.45}};
        private static readonly double[][] Array4 = {new double[]{1.410526172116255e-8, 0.00000898305509648872, -1.9939833816331, 200.9824383106796, -187.2403703815547, 91.6087516669843, -23.38765649603339, 2.57121317296198, -0.03801003308653, 17337981.2}
            , new double[]{-7.435856389565537e-9, 0.000008983055097726239, -0.78625201886289, 96.32687599759846, -1.85204757529826, -59.36935905485877, 47.40033549296737, -16.50741931063887, 2.28786674699375, 10260144.86}
            , new double[]{-3.030883460898826e-8, 0.00000898305509983578, 0.30071316287616, 59.74293618442277, 7.357984074871, -25.38371002664745, 13.45380521110908, -3.29883767235584, 0.32710905363475, 6856817.37}
            , new double[]{-1.981981304930552e-8, 0.000008983055099779535, 0.03278182852591, 40.31678527705744, 0.65659298677277, -4.44255534477492, 0.85341911805263, 0.12923347998204, -0.04625736007561, 4482777.06}
            , new double[]{3.09191371068437e-9, 0.000008983055096812155, 0.00006995724062, 23.10934304144901, -0.00023663490511, -0.6321817810242, -0.00663494467273, 0.03430082397953, -0.00466043876332, 2555164.4}
            , new double[]{2.890871144776878e-9, 0.000008983055095805407, -3.068298e-8, 7.47137025468032, -0.00000353937994, -0.02145144861037, -0.00001234426596, 0.00010322952773, -0.00000323890364, 826088.5}};

        /// <summary>
        /// 坐标转地图瓦片块行列号
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="zoom">地图层级（2-18）</param>
        /// <param name="tileImgSize">瓦片大小</param>
        /// <returns></returns>
        public static int2 LocationToTile(double longitude, double latitude, int zoom, int tileImgSize)
        {
            var mPos = LocationToMercator(longitude, latitude);
            double dpi = Math.Pow(2, 18 - zoom);// 地图分辨率
            return new int2((int)(mPos.x / dpi / tileImgSize), (int)(mPos.y / dpi / tileImgSize));
        }

        public static double2 LocationToMercator(Location loc)
        {
            return LocationToMercator(loc.longitude, loc.altitude);
        }

        /// <summary>
        /// 百度坐标转墨卡托
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="latitude"></param>
        /// <returns></returns>
        public static double2 LocationToMercator(double longitude, double latitude)
        {
            double[] arr = null;
            //double n_lat = latitude > 74 ? 74 : latitude;
            //n_lat = n_lat < -74 ? -74 : n_lat;
            for (int i = 0; i < Array1.Length; i++)
            {
                if (latitude >= Array1[i])
                {
                    arr = Array2[i];
                    break;
                }
            }
            if (arr == null)
            {
                for (int i = Array1.Length - 1; i >= 0; i--)
                {
                    if (latitude <= -Array1[i])
                    {
                        arr = Array2[i];
                        break;
                    }
                }
            }
            return Converter(longitude, latitude, arr);
        }

        /// <summary>
        /// 墨卡托坐标转百度坐标
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Location MercatorToLocation(double x, double y)
        {
            double[] arr = null;
            x = Math.Abs(x);
            y = Math.Abs(y);
            for (int i = 0; i < Array3.Length; i++)
            {
                if (y >= Array3[i])
                {
                    arr = Array4[i];
                    break;
                }
            }

            var pos2 = Converter(x, y, arr);
            return new Location(pos2.x, pos2.y, 0, false);
        }

        private static double2 Converter(double x, double y, double[] param)
        {
            double T = param[0] + param[1] * Math.Abs(x);
            double cC = Math.Abs(y) / param[9];
            double cF = param[2] + param[3] * cC + param[4] * cC * cC + param[5] * cC * cC * cC + param[6] * cC * cC * cC * cC + param[7] * cC * cC * cC * cC * cC + param[8] * cC * cC * cC * cC * cC * cC;
            T *= (x < 0 ? -1 : 1);
            cF *= (y < 0 ? -1 : 1);

            return new double2(T, cF);
        }
    }
}