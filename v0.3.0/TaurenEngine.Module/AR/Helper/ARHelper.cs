/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.AR
{
    public class ARHelper
    {
        /// <summary>
        /// 是否是支持AR的设备
        /// </summary>
        /// <returns></returns>
        public static bool IsARDevice()
        {
            return Application.platform == RuntimePlatform.Android ||
                   Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }
}