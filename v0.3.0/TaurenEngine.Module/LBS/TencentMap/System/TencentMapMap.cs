/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.AR;
using TaurenEngine.Core;
using TaurenEngine.HardwareEx;
using TaurenEngine.LocationEx;
using TaurenEngine.Mathematics;
using TencentMap.API;
using TencentMap.CoordinateSystem;
using UnityEngine;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// 腾讯地图地图显示控制
    /// </summary>
    public class TencentMapMap
    {
        private MapController _mapController;

        private GameObject _mapContainer;
        private GameObject _cameraPivot;
        private GameObject _markContainer;

        private GpsLocation _gpsLocation;

        public TencentMapMap()
        {
            var setting = LBSEngineSetting.Instance.tencentMapSetting;

            _mapController = setting.mapController;
            _mapContainer = _mapController.gameObject;
            _cameraPivot = setting.cameraPivot;

            _gpsLocation = ARLocation.Instance.GpsLocation;
            _northDevice = NorthDevice.Instance;

            _markContainer = GameObjectHelper.GetOrCreateGameObject("MapMarkContainer");
            _markContainer.transform.position = _mapContainer.transform.position;
            _markContainer.transform.rotation = _mapContainer.transform.rotation;
            _markContainer.SetActive(IsShowMap);

            InitMainTag();
        }

        private float _deltaTime = 0.0f;
        internal void Update()
        {
	        if (!IsShowMap)
		        return;

	        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
	        {
		        var deltaPos = Input.GetTouch(0).deltaPosition;
		        MoveOffset(-deltaPos.x, -deltaPos.y);
	        }

	        if (!IsOffset)
	        {
		        if (_deltaTime > 1.0f)
		        {
			        Location = _gpsLocation.LocationAuto;

                    _deltaTime = 0.0f;
		        }

		        _deltaTime += Time.deltaTime;
	        }

	        if (_mainTag != null)
	        {
		        AddToMap(_mainTag);

		        var angle = _mainTag.transform.eulerAngles;
		        angle.y = _northDevice.Update();
		        _mainTag.transform.eulerAngles = angle;
            }
        }

        /// <summary>
        /// 显示层级（需预设）
        /// </summary>
        public int ShowLayer { get; set; }

        /// <summary>
        /// 是否显示地图
        /// </summary>
        public bool IsShowMap
        {
            get => _mapContainer.activeSelf;
            set
            {
	            if (IsShowMap == value)
		            return;

	            _mapContainer.SetActive(value);
                _cameraPivot.SetActive(value);
                _markContainer.SetActive(value);

                if (_mainTag != null)
					_mainTag.IsShow = value;

                if (value)
                {
	                _northDevice.SetEnabled(this, true);

	                Location = _gpsLocation.LocationAuto;
                }
                else
                {
	                _northDevice.SetEnabled(this, false);
                }
            }
        }

        #region 当前位置显示
        private MainMapTag _mainTag;
        private readonly NorthDevice _northDevice;

        private void InitMainTag()
        {
	        if (LBSEngineSetting.Instance.tencentMapSetting.mainTag == null)
		        return;

	        _mainTag = GameObject.Instantiate(LBSEngineSetting.Instance.tencentMapSetting.mainTag);
	        _mainTag.transform.SetParent(_markContainer.transform);
	        _mainTag.IsShow = false;
        }
        #endregion

        #region GPS定位
        /// <summary>
        /// 地图中心点经纬度
        /// </summary>
        public Location Location
        {
            get
            {
                var value = _mapController.GetCoordinate();
                return new Location(value.lontitude, value.latitude, 0, false);
            }
            set
            {
                _mapController.SetCoordinate(new Coordinate()
                {
                    lontitude = value.longitude,
                    latitude = value.latitude
                });
                _mapController.DidRender();
            }
        }
        #endregion

        #region 地图标记管理
        private readonly List<IMapTag> _tags = new List<IMapTag>();

        /// <summary>
        /// 添加对象到地图上
        /// </summary>
        /// <param name="go"></param>
        /// <param name="loc"></param>
        public void AddToMap(IMapTag tag)
        {
	        var go = tag.gameObject;
            if (!_tags.Contains(tag))
            {
                go.layer = ShowLayer;
                go.transform.SetParent(_markContainer.transform);
                _tags.Add(tag);
            }

            var toPos = ConvertLocationToWorld(tag.Location);
            var pos = go.transform.position;
            pos.x = toPos.x;
            pos.z = toPos.z;
            go.transform.position = pos;
        }

        /// <summary>
        /// 从地图移除对象
        /// </summary>
        /// <param name="go"></param>
        public void RemoveFromMap(IMapTag tag)
        {
            _tags.Remove(tag);
        }

        private void UpdateMapTags()
        {
	        foreach (var tag in _tags)
	        {
		        AddToMap(tag);
	        }
        }

        /// <summary>
        /// 经纬度坐标转换成世界坐标
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public Vector3 ConvertLocationToWorld(Location loc)
        {
            return _mapController.ConvertCoordinateToWorld(new Coordinate()
            {
                lontitude = loc.longitude,
                latitude = loc.latitude
            });
        }

        public Vector3 ConvertLocationToWorld(double2 pos)
        {
	        return _mapController.ConvertCoordinateToWorld(new Coordinate()
	        {
		        lontitude = pos.x,
		        latitude = pos.y
	        });
        }

        /// <summary>
        /// 经纬度坐标转化为屏幕坐标
        /// x, y：表示屏幕坐标，bottom-left为原点，单位：像素
        /// </summary>
        /// <param name="loc"></param>
        /// <returns></returns>
        public Vector2 ConvertLocationToScreen(Location loc)
        {
            return _mapController.ConvertCoordinateToScreen(new Coordinate()
            {
                lontitude = loc.longitude,
                latitude = loc.latitude
            });
        }

        public Location ConvertScreenToLocation(Vector2 screenPosition)
        {
            var coord = _mapController.ConvertScreenToCoordinate(screenPosition);
            return new Location(coord.lontitude, coord.latitude, 0, false);
        }
        #endregion

        #region 显示控制
        public bool IsOffset { get; set; }

        /// <summary>
        /// 移动复位
        /// </summary>
        public void MoveReset()
        {
	        if (!IsOffset)
		        return;

	        IsOffset = false;
	        Location = _gpsLocation.LocationAuto;
        }

        /// <summary>
        /// 移动地图
        /// x、y 为屏幕坐标偏移量
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveOffset(double x, double y)
        {
	        IsOffset = true;

            _mapController.MoveOffset(x, y);// 在移动的过程状态, 调用 MoveOffset(x,y);
            _mapController.DidRender();// 在移动的结束状态, 调用一次 DidRender()
        }

        public void MoveToWorldPosition(Vector3 worldPosition)
        {
	        IsOffset = true;

            _mapController.MoveToWorldPositon(worldPosition);
            _mapController.DidRender();
        }

        /// <summary>
        /// 缩放地图，地图目标缩放级别
        /// </summary>
        public int ZoomLevel
        {
            get => (int)_mapController.GetZoomLevel();
            set
            {
                if (value > 18) value = 18;
                else if (value < 4) value = 4;

                if (ZoomLevel == value)
                    return;

                _mapController.SetZoomLevel(value);// 缩放地图过程状态, 调用 SetZoomLevel(zoomLevel);
                _mapController.DidRender();// 缩放地图结束状态, 调用一次 DidRender()

                UpdateMapTags();
            }
        }

        /// <summary>
        /// 是否显示3D楼块，针对17、18级
        /// 默认显示。true表示显示建筑物（3D）， false表示隐藏建筑物（2D）
        /// 
        /// 初始化设置：在Start()中调用;
        /// 运行时设置：在Update()或OnGUI()中设置一次调用
        /// </summary>
        /// <param name="enable"></param>
        public void SetBuildingEnable(bool enable)
        {
            _mapController.SetBuildingEnable(enable);
        }

        public void HidePartialBuilding(Location center, float width, float height)
        {
            _mapController.HidePartialBuilding(new Coordinate()
            {
                lontitude = center.longitude,
                latitude = center.latitude
            }, width, height);
        }
        #endregion

        #region 旋转
        /// <summary>
        /// 平面旋转地图
        /// rotationDelta 为平面旋转角度偏移量
        /// </summary>
        /// <param name="rotationDelta"></param>
        public void RotateDelta(double rotationDelta)
        {
            _mapController.RotateDelta(rotationDelta);// 旋转地图过程状态, 调用 RotateDelta(rotationDelta);
            _mapController.DidRender();// 旋转地图结束状态, 调用一次 DidRender()
        }

        /// <summary>
        /// 俯视旋转地图
        /// overlookDelta 为俯视旋转角度偏移量
        /// </summary>
        /// <param name="overlookDelta"></param>
        public void OverlookDelta(double overlookDelta)
        {
            _mapController.OverlookDelta(overlookDelta);// 俯视旋转地图过程状态, 调用 OverlookDelta(overlookDelta);
            _mapController.DidRender();// 旋转地图结束状态, 调用一次 DidRender()
        }

        public double Rotation => _mapController.GetRotation();
        public double Overlooking => _mapController.GetOverlooking();
        #endregion

        #region 设置样式
        /// <summary>
        /// 切换个性化底图样式
        /// 参数为在官网配置的样式槽位号，从1开始
        /// </summary>
        /// <param name="index"></param>
        public void SetMapStyle(int index)
        {
            _mapController.SetMapStyle(index);
        }

        public void SetMapTextFont(Font font)
        {
            _mapController.SetMapTextFont(font);
        }
        #endregion

        /// <summary>
        /// 设置是否开启瓦片数据的 本地缓存, 默认开启 当手机剩余存储空间不够时，开发者可以设置false
        /// </summary>
        /// <param name="isNeedLocalCache"></param>
        public void SwitchLocalCache(bool isNeedLocalCache)
        {
            _mapController.SwitchLocalCache(isNeedLocalCache);
        }
    }
}