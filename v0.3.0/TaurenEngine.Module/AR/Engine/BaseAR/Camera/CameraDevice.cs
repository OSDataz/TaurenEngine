/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using System;
using TaurenEngine.Core;
using UnityEngine;
using UnityEngine.UI;

namespace TaurenEngine.AR
{
    /// <summary>
    /// 调用手机摄像头渲染到Unity
    /// </summary>
    public class CameraDevice : MonoBehaviour
    {
        private WebCamTexture _webCamTexture;

        private RawImage _rawImage;

        private AspectRatioFitter _aspectRatioFitter;
        private readonly Rect _uvRectForVideoVerticallyMirrored = new Rect(1f, 0f, -1f, 1f);
        private readonly Rect _uvRectForVideoNotVerticallyMirrored = new Rect(0f, 0f, 1f, 1f);
        private int _webCamW = 0;
        private int _webCamH = 0;

        private bool _isAvailable = false;

        void Awake()
        {
            TDebug.Log("CameraDevice Awake");

            _rawImage = gameObject.GetOrAddComponent<RawImage>();
            _rawImage.raycastTarget = false;

            _aspectRatioFitter = gameObject.GetOrAddComponent<AspectRatioFitter>();
            // 宽度、高度、位置和锚点都会被自动调整，以使得该矩形覆盖父物体的整个区域，同时保持宽高比
            _aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.EnvelopeParent;
            _aspectRatioFitter.aspectRatio = 1.0f;

            var cRectTransform = gameObject.GetOrAddComponent<RectTransform>();
            cRectTransform.localPosition = Vector3.zero;
            cRectTransform.localScale = new Vector3(1, 1, 1);

            try
            {
                Application.RequestUserAuthorization(UserAuthorization.WebCam);
                _isAvailable = WebCamTexture.devices.Length > 0;

                if (_isAvailable)
                {
                    // Get Main Camera == Back Camera
                    _webCamTexture = new WebCamTexture(
                        WebCamTexture.devices[0].name,
                        Screen.width,
                        Screen.height,
                        30);

                    _rawImage.texture = _webCamTexture;
                }
                else
                {
                    TDebug.Log("No Camera Device found");
                }
            }
            catch (Exception e)
            {
                _isAvailable = false;
                TDebug.Log("Camera Device is not available: " + e);
                throw;
            }
        }

        void OnEnable()
        {
            TDebug.Log("CameraDevice OnEnable");

            StartWebCam();
        }

        void Start()
        {
            TDebug.Log("CameraDevice Start");
        }

        void Update()
        {
            if (!_isAvailable)
                return;

            UpdateOrientation();
        }

        void OnDisable()
        {
            TDebug.Log("CameraDevice OnDisable");

            StopWebCam();
        }

        protected void OnDestroy()
        {
            TDebug.Log("CameraDevice OnDestroy");

            StopWebCam();
        }

        // 更新相机方向
        private void UpdateOrientation()
        {
            if (_webCamTexture.width < 100)
                return;

            var curAngles = _rawImage.rectTransform.localEulerAngles;
            curAngles.z = -_webCamTexture.videoRotationAngle;// 渲染显示和手机反向旋转。返回一个顺时针方向的角度，它可用于旋转的多边形，以便相机内容显示在正确的方位
            if (_webCamTexture.videoVerticallyMirrored)// 纹理图像是否垂直翻转
                curAngles.z += 180f;

            _rawImage.rectTransform.localEulerAngles = curAngles;

            if (_webCamTexture.width != _webCamW || _webCamTexture.height != _webCamH)
            {
                _webCamW = _webCamTexture.width;
                _webCamH = _webCamTexture.height;

                _aspectRatioFitter.aspectRatio = (float)_webCamW / (float)_webCamH;// 更新UI宽高比
            }

            if (_webCamTexture.videoVerticallyMirrored)
                _rawImage.uvRect = _uvRectForVideoVerticallyMirrored;
            else
                _rawImage.uvRect = _uvRectForVideoNotVerticallyMirrored;
        }

        /// <summary>
        /// 修改分辨率显示
        /// </summary>
        /// <param name="factor"></param>
        public void ChangeResolutionAndPlay(float factor)
        {
            StopWebCam();

            _webCamTexture.requestedWidth = Mathf.RoundToInt(_webCamTexture.requestedWidth * factor);
            _webCamTexture.requestedHeight = Mathf.RoundToInt(_webCamTexture.requestedHeight * factor);

            StartWebCam();
        }

        private void StartWebCam()
        {
            if (!_isAvailable)
                return;

            if (_webCamTexture)
            {
                _webCamTexture.Play();
                TDebug.Log("手机摄像头已开启");
            }
        }

        private void StopWebCam()
        {
            if (_webCamTexture)
            {
                _webCamTexture.Stop();
            }
        }

        public bool IsAvailable => _isAvailable;
    }
}