/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System;
using System.Collections;
using TaurenEngine.Core;
using TaurenEngine.Json;
using UnityEngine.Networking;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// 腾讯地图导航
    ///
    /// 导航：https://lbs.qq.com/service/webService/webServiceGuide/webServiceRoute
    /// </summary>
    public class TencentMapNavi
    {
        public TencentMapNavi()
        {

        }

        /// <summary>
        /// 请求步行导航
        /// </summary>
        /// <param name="request"></param>
        /// <param name="callback"></param>
        public void ReqNaviWalking(TmNaviRequest request, Action<bool, TmNaviWalkingRes> callback)
        {
            LBSEngine.Instance.StartCoroutine(ReqNaviGet<TmNaviRequest, TmNaviWalkingRes>(request, callback));
        }

        /// <summary>
        /// 请求骑行导航
        /// </summary>
        /// <param name="request"></param>
        /// <param name="callback"></param>
        public void ReqNaviBicycling(TmNaviRequest request, Action<bool, TmNaviBicyclingRes> callback)
        {
            LBSEngine.Instance.StartCoroutine(ReqNaviGet<TmNaviRequest, TmNaviBicyclingRes>(request, callback));
        }

        /// <summary>
        /// 请求行车导航
        /// </summary>
        /// <param name="request"></param>
        /// <param name="callback"></param>
        public void ReqNaviDriving(TmNaviDrivingReq request, Action<bool, TmNaviDrivingRes> callback)
        {
            LBSEngine.Instance.StartCoroutine(ReqNaviGet<TmNaviDrivingReq, TmNaviDrivingRes>(request, callback));
        }

        /// <summary>
        /// 请求交通工具导航
        /// </summary>
        /// <param name="request"></param>
        /// <param name="callback"></param>
        public void ReqNaviTransit(TmNaviTransitReq request, Action<bool, TmNaviTransitRes> callback)
        {
            LBSEngine.Instance.StartCoroutine(ReqNaviGet<TmNaviTransitReq, TmNaviTransitRes>(request, callback));
        }

        private IEnumerator ReqNaviGet<TRequest, TResponse>(TRequest request, Action<bool, TResponse> callback) where TRequest : TmNaviRequest where TResponse : TmNaviResponse
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(request.ToUrl());
            yield return webRequest.SendWebRequest();

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    if (request.traveType == TmNaviReqTravelType.transit)
                    {
                        var data = ParseNaviGetData(webRequest.downloadHandler.text);
                        callback?.Invoke(true, data as TResponse);
                    }
                    else
                    {
                        var data = JsonHelper.ToObject<TResponse>(webRequest.downloadHandler.text);
                        data.Parse();
                        callback?.Invoke(true, data);
                    }
                    break;

                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.ProtocolError:
                case UnityWebRequest.Result.DataProcessingError:
                    TDebug.Error($"请求腾讯导航失败：\n{request.ToUrl()}\n{webRequest.error}");
                    callback?.Invoke(false, null);
                    break;
            }
        }

        private TmNaviTransitRes ParseNaviGetData(string jsonString)
        {
            var jData = JsonHelper.ToObject(jsonString);

            var data = new TmNaviTransitRes();
            //data.ParseJData(jData);
            data.Parse();
            return data;
        }
    }
}