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
    /// 根据太阳方位控制光线朝向
    /// </summary>
    public class LightWorldDirection : MonoBehaviour
    {
        /// <summary>
        /// 默认可设置为摄像机
        /// </summary>
        public Transform bindTransform;

        private GpsLocation _gpsLocation;
        private float _tan;

        void Awake()
        {
	        _gpsLocation = ARLocation.Instance.GpsLocation;
            _tan = Mathf.Tan(60 * 2 * Mathf.PI / 360);// 最小是60度
        }

        void Update()
        {
            if (_gpsLocation.IsReady)
            {
                var lights = GameObject.FindObjectsOfType<Light>();
                if (lights.Length > 0)
                {
                    var light = lights[0].gameObject;
                    var sunPos = MathHelper.GetPositionSun();

                    sunPos = (bindTransform.position - sunPos).normalized;// 太阳的方向单位向量
                    sunPos.y = Mathf.Min(sunPos.y, -(sunPos.MagnitudeXZ() / _tan));

                    light.transform.rotation = Quaternion.LookRotation(sunPos);

                    TDebug.Log("环境光朝向重定位：" + light.transform.rotation + " " + light.transform.rotation.eulerAngles);
                }

                Destroy(this);
            }
        }

        void OnDestroy()
        {
            bindTransform = null;
            _gpsLocation = null;
        }
    }
}