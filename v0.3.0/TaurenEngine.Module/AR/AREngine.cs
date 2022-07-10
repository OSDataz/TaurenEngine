/*����������������������������������������������������
 *����Engine  ��TaurenEngine
 *����Author  ��Osdataz
 *����Version ��v1.0
 *����Time    ��2021/8/29 11:23:23
 *����������������������������������������������������*/

using System.Collections.Generic;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.AR
{
    /// <summary>
    /// AR����
    /// </summary>
    public class AREngine : SingletonBehaviour<AREngine>
    {
	    #region ���л�����
        /// <summary>
        /// AR��������˳��
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
        /// AR�������
        /// </summary>
        public AREngineProxy Proxy { get; } = new AREngineProxy();

        /// <summary>
        /// ��������
        /// </summary>
        public void Startup()
        {
	        if (Proxy.Status != RunningStatus.None)
	        {
		        TDebug.Log($"AR�����ظ���������ǰ״̬��{Proxy.Status}");
		        return;
	        }

	        Proxy.Startup();

	        ARLocation.Instance.Startup();
	        ARObjectManager.Instance.Startup();
        }

        /// <summary>
        /// �ر�����
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

        #region ��ϵͳ
        /// <summary> ƽ����ϵͳ </summary>
        public ARPlane ArPlane { get; private set; }

        /// <summary> �����ڵ�ϵͳ </summary>
        public AROcclusion ArOcclusion { get; private set; }

        /// <summary> ͼ��ʶ��ϵͳ </summary>
        public ARImage ArImage { get; private set; }

        /// <summary> ��������ϵͳ </summary>
        public ARLight ArLight { get; private set; }
        #endregion
    }
}