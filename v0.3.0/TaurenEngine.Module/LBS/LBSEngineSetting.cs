/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// LBS引擎配置设置
    /// </summary>
    public class LBSEngineSetting : SingletonBehaviour<LBSEngineSetting>
    {
        [Tooltip("TencentMap 所需配置")]
        public TencentMapSetting tencentMapSetting;
    }
}