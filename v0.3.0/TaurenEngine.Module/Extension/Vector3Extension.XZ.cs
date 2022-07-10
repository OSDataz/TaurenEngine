/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/26 14:05:09
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
    public static partial class Vector3Extension
    {
        public static float MagnitudeXZ(this Vector3 value) => Mathf.Sqrt(value.x * value.x + value.z + value.z);

        public static Vector3 NormalizedXZ(this Vector3 value)
        {
            var m = value.MagnitudeXZ();
            if (m.Equals(0.0f)) return new Vector3();
            return new Vector3(value.x / m, 0, value.z / m);
        }

        public static float DistanceXZ(this Vector3 value, Vector3 v)
        {
            var a = value.x - v.x;
            var b = value.z - v.z;
            return Mathf.Sqrt(a * a + b * b);
        }

        public static float Distance2XZ(this Vector3 value, Vector3 v)
        {
            var a = value.x - v.x;
            var b = value.z - v.z;
            return a * a + b * b;
        }
    }
}