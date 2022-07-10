/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System;
using TencentMap.API;
using TencentMap.CoordinateSystem;
using TencentMap.WebService;
using Location = TaurenEngine.LocationEx.Location;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// 腾讯地图逆地址解析
    /// </summary>
    public class TencentMapRGC
    {
        private MapController _mapController;

        public TencentMapRGC()
        {
            var setting = LBSEngineSetting.Instance.tencentMapSetting;

            _mapController = setting.mapController;
        }

        /// <summary>
        /// 逆地址解析，封装的WebService逆地址解析服务
        /// </summary>
        /// <param name="loc"></param>
        /// <param name="callback"></param>
        public void ReverseGeoCoder(Location loc, Action<RGCResult> callback)
        {
            RGCOption option = new RGCOption();
            option.SetGetPoi(1);
            option.SetCoordinate(new Coordinate()
            {
                lontitude = loc.longitude,
                latitude = loc.latitude
            });
            WebServiceManager.GetInstance().ReverseGeoCoder(option, callback);
        }
    }
}