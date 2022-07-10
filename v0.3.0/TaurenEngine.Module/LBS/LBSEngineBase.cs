/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

namespace TaurenEngine.LBS
{
    public enum LBSEngineType
    {
        None,
        TencentMap
    }

    public abstract class LBSEngineBase
    {
        public bool IsDestroyed { get; private set; }

        internal abstract void Awake();
        internal abstract void OnEnable();
        internal abstract void Start();
        internal abstract void Update();
        internal abstract void OnDisable();

        internal virtual void OnDestroy()
        {
            IsDestroyed = true;
        }

        public abstract LBSEngineType EngineType { get; }
    }
}