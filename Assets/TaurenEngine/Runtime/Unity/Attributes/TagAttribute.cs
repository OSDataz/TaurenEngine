/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 20:55:25
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Runtime.Unity
{
    /// <summary>
    /// 标记属性，主要用于给字段和类打标记（昵称）。可用于编辑器显示或其它地方使用
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class)]
    public class TagAttribute : Attribute
    {
        /// <summary>
        /// 标记文本
        /// </summary>
        public string tag;

        public TagAttribute(string tag)
        {
            this.tag = tag;
        }
    }
}