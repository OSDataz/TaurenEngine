/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using TaurenEngine.LocationEx;
using TaurenEngine.Mathematics;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// 地理围栏圆形
    /// </summary>
    public class MapGeofenceRound : MapGeofenceBase
    {
        private double2 _center;
        private double _radius2;

        public void SetData(Location center, double radius)
        {
            SetData(center.longitude, center.latitude, radius);
        }

        public void SetData(double longitude, double latitude, double radius)
        {
            _center.x = longitude;
            _center.y = latitude;
            _radius2 = radius * radius;

            posMax.x = longitude + radius;
            posMax.y = latitude + radius;
            posMin.x = longitude - radius;
            posMin.y = latitude - radius;
        }

        public override bool CheckInside(double longitude, double latitude)
        {
            if (!base.CheckInside(longitude, latitude))
                return false;

            longitude -= _center.x;
            latitude -= _center.y;
            return longitude * longitude + latitude * latitude < _radius2;
        }

        public override MapGeofenceBase Clone()
        {
            return new MapGeofenceRound()
            {
	            posMax = posMax,
	            posMin = posMin,
	            _center = _center,
	            _radius2 = _radius2
            };
        }

        public double Radius => _radius2 * 0.5;
    }
}