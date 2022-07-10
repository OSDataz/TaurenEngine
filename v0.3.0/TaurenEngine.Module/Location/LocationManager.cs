/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 10:18:17
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.LocationEx
{
    public class LocationManager : SingletonBehaviour<LocationManager>
    {
        public LocationSetting setting = new LocationSetting();

        private ILocationProvider _provider;
        private LocationSwitch _locationSwitch = LocationSwitch.None;

        private Location _location;
        private Location _curLocation;
        private Location _lastLocation;

        void Awake()
        {
            _provider = new AMapLocationProvider();
            _provider.Awake();
            if (!_provider.IsAvailable)
            {
                TDebug.Log($"不支持高德GPS定位");

                _provider.OnDisable();
                _provider.OnDestroy();
                _provider = null;
            }

            if (_provider == null)
            {
                _provider = new UnityLocationProvider();
                _provider.Awake();

                if (!_provider.IsAvailable)
                    TDebug.Log("不支持Unity GPS定位");
            }

            _locationSwitch = _provider.GetLocationSwitch(setting.locationType);
        }

        void OnEnable()
        {
            _provider.OnEnable();
        }

        void Update()
        {
            if (_provider.GetLocation(ref _curLocation))
            {
                IsUpdate = setting.ignoreAltitude ? !_curLocation.EqualIgnore(_lastLocation) : _curLocation != _lastLocation;

                if (IsUpdate)
                {
                    //TDebug.Log($"定位更新 {_curLocation.longitude.ToString("F4")} {_curLocation.latitude.ToString("F4")} {_curLocation.altitude.ToString("F2")}");
                    _lastLocation = _curLocation;

                    if (_locationSwitch == LocationSwitch.WGS84_To_GCJ02)
                    {
                        var d2 = LocationMath.WGS84_To_GCJ02(_curLocation.longitude, _curLocation.latitude);
                        _location.longitude = d2.x;
                        _location.latitude = d2.y;
                        _location.altitude = _curLocation.altitude;
                        _location.ignoreAltitude = _curLocation.ignoreAltitude;
                    }
                    else if (_locationSwitch == LocationSwitch.WGS84_To_BD09)
                    {
                        var d2 = LocationMath.WGS84_To_BD09(_curLocation.longitude, _curLocation.latitude);
                        _location.longitude = d2.x;
                        _location.latitude = d2.y;
                        _location.altitude = _curLocation.altitude;
                        _location.ignoreAltitude = _curLocation.ignoreAltitude;
                    }
                    else
                    {
                        _location = _curLocation;
                    }
                }
            }
            else
            {
                IsUpdate = false;
            }
        }

        void OnDisable()
        {
            _provider.OnDisable();
        }

        protected override void OnDestroy()
        {
            if (IsDestroyed)
                return;

            _provider.OnDestroy();
            _provider = null;

            base.OnDestroy();
        }

        /// <summary> 定位信息是否更新 </summary>
        public bool IsUpdate { get; private set; }

        public Location Location => _location;
        /// <summary>
        /// 定位是否已启动（已获得定位）
        /// </summary>
        public bool IsReady => _provider.IsReady;
        /// <summary>
        /// 是否支持定位
        /// </summary>
        public bool IsAvailable => _provider.IsAvailable;
    }
}