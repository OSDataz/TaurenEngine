/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

namespace TaurenEngine.AR
{
    /// <summary>
    /// 环境光照子系统
    /// </summary>
    public class ARLight : SystemBase<ISystemLight>
    {
        internal override ISystemLight System
        {
            get
            {
                if (system == null)
                    system = EngineBase?.SystemLight;

                return system;
            }
        }

        #region 真实光方向控制
        private bool _useWorldLight = false;

        /// <summary>
        /// 是否使用世界方向灯光。真实太阳方位
        /// </summary>
        public bool UseWorldLight
        {
	        get => _useWorldLight;
	        set
	        {
		        if (_useWorldLight == value)
			        return;

		        _useWorldLight = value;

		        if (_useWorldLight)
			        GetComponentInstance<LightWorldDirection>();
		        else
			        DestroyComponent<LightWorldDirection>();
	        }
        }

        internal override void Clear()
        {
            UseWorldLight = false;

            base.Clear();
        }
        #endregion
    }

    public interface ISystemLight : ISystemBase
    {

    }
}