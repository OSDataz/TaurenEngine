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
    public abstract class MapGeofenceBase
    {
        protected double2 posMax;
        protected double2 posMin;

        public bool CheckInside(Location loc)
        {
            return CheckInside(loc.longitude, loc.latitude);
        }

        public virtual bool CheckInside(double longitude, double latitude)
        {
            return longitude < posMax.x && latitude < posMax.y && longitude > posMin.x && latitude > posMin.y;
        }

        public abstract MapGeofenceBase Clone();

        public virtual double2 CenterPos => new double2((PosMax.x + posMin.x) * 0.5f, (PosMax.y + posMin.y) * 0.5f);
        public Location CenterLoc
        {
	        get
	        {
		        var pos = CenterPos;
		        return new Location(pos.x, pos.y, 0, true);
	        }
        }

        public double2 PosMax => posMax;
        public double2 PosMin => posMin;
    }
}