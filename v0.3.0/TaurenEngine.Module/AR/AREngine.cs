/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.AR
{
    /// <summary>
    /// AR引擎
    /// </summary>
    public class AREngine : SingletonBehaviour<AREngine>
    {
	    #region 序列化配置
        /// <summary>
        /// AR引擎启动顺序
        /// </summary>
        [SerializeField]
        internal List<AREngineType> startupSort = new List<AREngineType>()
        {
	        AREngineType.ARFoundation,
	        AREngineType.EasyAR,
	        AREngineType.BaseAR
        };
        #endregion

        /// <summary>
        /// AR引擎代理
        /// </summary>
        public AREngineProxy Proxy { get; } = new AREngineProxy();

        /// <summary>
        /// 启动引擎
        /// </summary>
        public void Startup()
        {
	        if (Proxy.Status != RunningStatus.None)
	        {
		        TDebug.Log($"AR引擎重复启动，当前状态：{Proxy.Status}");
		        return;
	        }

	        Proxy.Startup();

	        ARLocation.Instance.Startup();
	        ARObjectManager.Instance.Startup();
        }

        /// <summary>
        /// 关闭引擎
        /// </summary>
        public void Close()
        {
	        if (Proxy.Status == RunningStatus.None)
		        return;

	        ARObjectManager.Instance.Clear();
	        ARLocation.Instance.Close();

            ClearSystem();
	        Proxy.Close();
        }

        void Awake()
        {
	        TDebug.Log($"AREngine Awake");

            ArPlane = new ARPlane();
            ArOcclusion = new AROcclusion();
            ArLight = new ARLight();
            ArImage = new ARImage();
        }

        void OnEnable()
        {
            TDebug.Log($"AREngine OnEnable");

	        ArPlane.OnEnable();
	        ArOcclusion.OnEnable();
	        ArLight.OnEnable();
	        ArImage.OnEnable();

            Proxy.EngineBase?.OnEnable();
        }

        void Update()
        {
	        Proxy.EngineBase?.Update();
        }

        void OnDisable()
        {
            TDebug.Log($"AREngine OnDisable");

	        ArPlane.OnDisable();
	        ArOcclusion.OnDisable();
	        ArLight.OnDisable();
	        ArImage.OnDisable();

            Proxy.EngineBase?.OnDisable();
        }

        protected override void OnDestroy()
        {
	        if (IsDestroyed)
		        return;

	        ArPlane.OnDestroy();
	        ArOcclusion.OnDestroy();
	        ArLight.OnDestroy();
	        ArImage.OnDestroy();

            Proxy.Destroy();

            base.OnDestroy();
        }

        internal void ClearSystem()
        {
	        ArPlane.Clear();
	        ArOcclusion.Clear();
	        ArLight.Clear();
	        ArImage.Clear();
        }

        #region 子系统
        /// <summary> 平面检测系统 </summary>
        public ARPlane ArPlane { get; private set; }

        /// <summary> 人物遮挡系统 </summary>
        public AROcclusion ArOcclusion { get; private set; }

        /// <summary> 图像识别系统 </summary>
        public ARImage ArImage { get; private set; }

        /// <summary> 环境光照系统 </summary>
        public ARLight ArLight { get; private set; }
        #endregion
    }
}