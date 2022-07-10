/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using TaurenEngine.LocationEx;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// 地理围栏方形
    /// </summary>
    public class MapGeofenceBox : MapGeofenceBase
    {
	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="locMax">GPS坐标</param>
	    /// <param name="locMin">GPS坐标</param>
        public void SetData(Location locMax, Location locMin)
        {
            SetData(locMax.longitude, locMax.latitude, locMin.longitude, locMin.latitude);
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="center">GPS坐标</param>
	    /// <param name="radius">单位：米</param>
        public void SetData(Location center, double radius)
        {
            SetData(center.longitude, center.latitude, radius);
        }

	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="longitude">GPS坐标</param>
	    /// <param name="latitude">GPS坐标</param>
	    /// <param name="radius">单位：米</param>
        public void SetData(double longitude, double latitude, double radius)
        {
	        var lonRadius = Location.MeterToLongitude(radius, latitude);
	        var latRadius = Location.MeterToLatitude(radius);

	        SetData(longitude + lonRadius, latitude + latRadius, longitude - lonRadius, latitude - latRadius);
        }

        public void SetData(double lonMax, double latMax, double lonMin, double latMin)
        {
            if (lonMax > lonMin)
            {
                posMax.x = lonMax;
                posMin.x = lonMin;
            }
            else
            {
                posMax.x = lonMin;
                posMin.x = lonMax;
            }

            if (latMax > latMin)
            {
                posMax.y = latMax;
                posMin.y = latMin;
            }
            else
            {
                posMax.y = latMin;
                posMin.y = latMax;
            }
        }

        public override MapGeofenceBase Clone()
        {
	        return new MapGeofenceBox()
	        {
		        posMax = posMax,
		        posMin = posMin
	        };
        }

        public double Weight => posMax.x - posMin.x;
        public double Height => posMax.y - posMin.y;
    }
}