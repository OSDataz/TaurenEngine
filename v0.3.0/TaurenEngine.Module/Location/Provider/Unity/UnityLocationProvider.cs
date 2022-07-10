/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 10:17:59
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;
using TaurenEngine.HardwareEx;
using UnityEngine;

namespace TaurenEngine.LocationEx
{
    /// <summary>
    /// Unity自带定位，WGS84坐标系
    /// </summary>
    internal class UnityLocationProvider : ILocationProvider
    {
        private LocationDevice _device;
        private LocationService _locationService;

        private bool _isReady = false;

        public void Awake()
        {
            _device = LocationDevice.Instance;
            _locationService = _device.LocationService;
        }

        public void OnEnable()
        {
            TDebug.Log("启动Unity定位");
            var setting = LocationManager.Instance.setting;
            _device.desiredAccuracyInMeters = setting.unitySettnig.desiredAccuracyInMeters;
            _device.updateDistanceInMeters = setting.unitySettnig.updateDistanceInMeters;
            _device.SetEnabled(this, true);
        }

        public void OnDisable()
        {
            TDebug.Log("停止Unity定位");
            _device.SetEnabled(this, false);
            _isReady = false;
        }

        public void OnDestroy()
        {
            _device = null;
            _locationService = null;
        }

        public bool GetLocation(ref Location loc)
        {
            if (!_isReady)
            {
                if (_device.IsAvailable && _locationService.isEnabledByUser)
                {
                    if (_locationService.status == LocationServiceStatus.Running)
                    {
                        _isReady = true;
                    }
                    else if (_locationService.status == LocationServiceStatus.Stopped)
                    {
                        _device.SetEnabled(this, true);
                        return false;
                    }
                }
                else
                    return false;
            }

            var info = _locationService.lastData;
            if (Math.Abs(info.longitude) + Math.Abs(info.latitude) + Math.Abs(info.altitude) > 0.00000001f)// 防止定位失败
            {
                loc.longitude = info.longitude;
                loc.latitude = info.latitude;
                loc.altitude = info.altitude;
                return true;
            }
            else
                return false;
        }

        public LocationSwitch GetLocationSwitch(LocationType locationType)
        {
            if (locationType == LocationType.GCJ02) return LocationSwitch.WGS84_To_GCJ02;
            if (locationType == LocationType.BD09) return LocationSwitch.WGS84_To_BD09;

            return LocationSwitch.None;
        }

        public bool IsAvailable => _device.IsAvailable;
        public bool IsReady => _isReady;
    }
}