﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/9/8 10:10:13
 *└────────────────────────┘*/

namespace TaurenEngine
{
    /// <summary>
    /// 可回收对象接口
    /// </summary>
    public interface IRecycle : IObject
    {
        /// <summary>
        /// 清理对象，在放入对象池时调用
        /// </summary>
        void Clear();
    }
}