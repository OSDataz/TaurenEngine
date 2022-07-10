/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.AR
{
    /// <summary>
    /// 平面检测子系统
    /// </summary>
    public class ARPlane : SystemBase<ISystemPlane>
    {
        internal override ISystemPlane System
        {
            get
            {
	            if (system == null)
		            system = EngineBase?.SystemPlane;

                return system;
            }
        }

        public bool GetPanelPos(Vector2 screenPoint, ref Pose pose)
        {
            return System.GetPanelPos(screenPoint, ref pose);
        }
    }

    public interface ISystemPlane : ISystemBase
    {
        bool GetPanelPos(Vector2 screenPoint, ref Pose pose);
    }
}