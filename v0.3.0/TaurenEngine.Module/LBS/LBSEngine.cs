/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// LBS引擎
    /// </summary>
    public class LBSEngine : SingletonBehaviour<LBSEngine>
    {
        void Awake()
        {
            StartupLBSEngine();
        }

        internal void StartupLBSEngine()
        {
            EngineBase = new TencentMapEx();
            EngineBase.Awake();
        }

        void OnEnable()
        {
            EngineBase.OnEnable();
        }

        void Start()
        {
            EngineBase.Start();
        }

        void Update()
        {
            EngineBase.Update();
        }

        void OnDisable()
        {
            TDebug.Log("LBSEngine OnDisable");
	        EngineBase.OnDisable();
        }

        protected override void OnDestroy()
        {
            EngineBase.OnDestroy();
            EngineBase = null;

            base.OnDestroy();
        }

        #region 对外接口
        internal LBSEngineBase EngineBase { get; private set; }

        /// <summary>
        /// LBS引擎类型
        /// </summary>
        public LBSEngineType EngineType => EngineBase?.EngineType ?? LBSEngineType.None;

        /// <summary>
        /// 腾讯地图
        /// </summary>
        public TencentMapEx TencentMapEx => EngineBase as TencentMapEx;
        #endregion
    }
}