/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 20:55:25
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Editor
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class)]
    public class TagAttribute : Attribute
    {
        public string tag;

        public TagAttribute(string tag)
        {
            this.tag = tag;
        }
    }
}