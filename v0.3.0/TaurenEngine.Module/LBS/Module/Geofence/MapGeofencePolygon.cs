/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.LocationEx;
using TaurenEngine.Mathematics;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// 地理围栏自定义
    ///
    /// https://www.cnblogs.com/s-c-x/p/10698687.html
    /// </summary>
    public class MapGeofencePolygon : MapGeofenceBase
    {
        private List<double2> _posList = new List<double2>();

        public void SetData(List<Location> list)
        {
            SetData(list.ConvertAll(item => new double2(item.longitude, item.latitude)));
        }

        public void SetData(List<double> list)
        {
            List<double2> dList = new List<double2>();
            int len = list.Count;
            for (int i = 0; i < len; i += 2)
            {
                dList.Add(new double2(list[i], list[i + 1]));
            }

            SetData(dList);
        }

        public void SetData(List<double2> list)
        {
	        if (list?.Count <= 0)
	        {
		        _posList = new List<double2>();
		        return;
	        }

            _posList = list;

            posMax = list[0];
            posMin = list[0];

            int len = list.Count;
            for (int i = 1; i < len; ++i)
            {
                var td = list[i];

                if (td.x > posMax.x) posMax.x = td.x;
                else if (td.x < posMin.x) posMin.x = td.x;

                if (td.y > posMax.y) posMax.y = td.y;
                else if (td.y < posMin.y) posMin.y = td.y;
            }
        }

        public override bool CheckInside(double longitude, double latitude)
        {
            if (!base.CheckInside(longitude, latitude))
                return false;

            bool crossings = false;
            int len = _posList.Count - 1;
            for (int i = 0; i <= len; ++i)
            {
                var cD2 = _posList[i];
                var nD2 = _posList[i == len ? 0 : i + 1];

                if (((cD2.x > longitude) != (nD2.x > longitude)) 
                    && (latitude < (nD2.y - cD2.y) / (nD2.x - cD2.x) * (longitude - cD2.x) + cD2.y))
                    crossings = !crossings;
            }

            return crossings;
        }

        public override MapGeofenceBase Clone()
        {
	        return new MapGeofencePolygon()
	        {
		        posMax = posMax,
		        posMin = posMin,
		        _posList = new List<double2>(_posList.ToArray())
	        };
        }

        public List<double2> PosList => _posList;
    }
}