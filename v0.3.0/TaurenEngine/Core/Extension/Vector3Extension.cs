/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/26 14:06:39
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
    public static partial class Vector3Extension
    {
        public static float Distance2(this Vector3 value, Vector3 vec)
        {
            double num1 = value.x - vec.x;
            double num2 = value.y - vec.y;
            double num3 = value.z - vec.z;
            return (float)(num1 * num1 + num2 * num2 + num3 * num3);
        }
    }
}