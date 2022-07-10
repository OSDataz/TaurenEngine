/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

namespace TaurenEngine.AR
{
    /// <summary>
    /// 人物遮挡子系统
    /// </summary>
    public class AROcclusion : SystemBase<ISystemOcclusion>
    {
        internal override ISystemOcclusion System
        {
            get
            {
                if (system == null)
                    system = EngineBase?.SystemOcclusion;

                return system;
            }
        }
    }

    public interface ISystemOcclusion : ISystemBase
    {

    }
}