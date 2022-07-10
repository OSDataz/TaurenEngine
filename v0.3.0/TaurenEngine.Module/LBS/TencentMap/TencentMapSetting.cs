/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System;
using TencentMap.API;
using UnityEngine;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// 腾讯地图引擎设置
    /// </summary>
    [Serializable]
    public class TencentMapSetting
    {
        [Tooltip("地图控制器 map_mapObj_prefab")]
        public MapController mapController;
        [Tooltip("摄像机对象 map_cameraPivot_prefab")]
        public GameObject cameraPivot;
        [Tooltip("主箭头")]
        public MainMapTag mainTag;
    }
}