/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.AR
{
    /// <summary>
    /// AR引擎配置设置
    /// </summary>
    public class AREngineSetting : SingletonBehaviour<AREngineSetting>
    {
        [Tooltip("是否使用定位")]
	    public bool useLocation;

	    [Tooltip("ARFoundation 所需配置")]
        public ARFoundationSetting arFoundationSetting;

        [Tooltip("EasyAR 所需配置")]
        public EasyARSetting easyArSetting;
    }
}