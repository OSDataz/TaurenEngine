/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using System;
using UnityEngine;
#if ARFoundation
using UnityEngine.XR.ARSubsystems;
#endif

namespace TaurenEngine.AR
{
    /// <summary>
    /// AR Foundation引擎设置
    /// </summary>
    [Serializable]
    public class ARFoundationSetting
    {
        [Tooltip("平面检测预制体显示")]
        public GameObject planePrefab;

        [Tooltip("图片最大追踪数量")]
        public int trackImageMaxNum = 4;

#if ARFoundation
	    [Tooltip("The library of images which will be detected and/or tracked in the physical environment.")]
	    public XRReferenceImageLibrary serializedLibrary;
#endif
    }
}