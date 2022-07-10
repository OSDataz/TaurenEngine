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
    public abstract class SystemBase<T> where T : ISystemBase
    {
        private readonly SubState _subState = new SubState();

        protected T system;

        internal abstract T System { get; }

        internal void OnEnable()
        {
            if (_subState.SetParentState(true))
                UpdateState();
        }

        internal void OnDisable()
        {
            if (_subState.SetParentState(false))
                UpdateState();
        }

        private void UpdateState()
        {
            TDebug.Log($"ARSystem {GetType().Name} Enabled Changed: {_subState.Enabled}");

            if (_subState.Enabled)
                System?.Enable();
            else
                System?.Disable();
        }

        public bool Enabled
        {
            get => _subState.Enabled;
            set
            {
                if (_subState.SetCurrentState(value))
                    UpdateState();
            }
        }

        /// <summary>
        /// 销毁引擎组件
        /// </summary>
        internal void OnDestroy()
        {
            System?.Destroy();
        }

        /// <summary>
        /// 重置引擎
        /// </summary>
        internal virtual void Clear()
        {
	        System?.Destroy();
            system = default;
        }

        public C GetComponentInstance<C>() where C : Component
        {
	        return AREngine.Instance.gameObject.GetOrAddComponent<C>();
        }

        public void DestroyComponent<C>() where C : Component
        {
            AREngine.Instance.gameObject.DestroyComponent<C>();
        }

        /// <summary>
        /// 子系统是否支持，部分引擎未启动前，无法知道是否支持，需先启动在判断（如：ARFoundation）
        /// </summary>
        public bool IsAvailable => System?.IsAvailable ?? false;

        internal AREngineBase EngineBase => AREngine.Instance.Proxy.EngineBase;
    }

    public interface ISystemBase
    {
        void Enable();

        void Disable();

        /// <summary>
        /// 销毁组件
        /// </summary>
        void Destroy();

        bool IsAvailable { get; }
    }
}