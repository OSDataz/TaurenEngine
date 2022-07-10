/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/9/8 10:10:13
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
    public interface IRecycle
    {
        /// <summary>
        /// 【外部切勿调用】清理对象，在放入对象池时调用
        /// </summary>
        void Clear();
        /// <summary>
        /// 【外部切勿调用】销毁对象，在对象池过剩，销毁对象时调用
        /// </summary>
        void Destroy();
    }
}