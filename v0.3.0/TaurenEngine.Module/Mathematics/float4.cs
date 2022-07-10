/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/6 22:10:08
 *└────────────────────────┘*/

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TaurenEngine.Mathematics
{
    public partial struct float4 : IEquatable<float4>, IFormattable
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public float4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other) => other is float4 other1 && Equals(other1);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(float4 other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(float4 lhs, float4 rhs)
        {
            double num1 = lhs.x - rhs.x;
            double num2 = lhs.y - rhs.y;
            double num3 = lhs.z - rhs.z;
            double num4 = lhs.w - rhs.w;
            return num1 * num1 + num2 * num2 + num3 * num3 + num4 * num4 < 9.99999943962493E-11;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(float4 lhs, float4 rhs) => !(lhs == rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator +(float4 lhs, float4 rhs) => new float4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator +(float4 lhs, float rhs) => new float4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator +(float lhs, float4 rhs) => new float4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator -(float4 lhs, float4 rhs) => new float4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator -(float4 lhs, float rhs) => new float4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator -(float lhs, float4 rhs) => new float4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator -(float4 val) => new float4(-val.x, -val.y, -val.z, -val.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator *(float4 lhs, float4 rhs) => new float4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator *(float4 lhs, float rhs) => new float4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator *(float lhs, float4 rhs) => new float4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator /(float4 lhs, float4 rhs) => new float4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator /(float4 lhs, float rhs) => new float4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator /(float lhs, float4 rhs) => new float4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator %(float4 lhs, float4 rhs) => new float4(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z, lhs.w % rhs.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator %(float4 lhs, float rhs) => new float4(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs, lhs.w % rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator %(float lhs, float4 rhs) => new float4(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z, lhs % rhs.w);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator ++(float4 val) => new float4(++val.x, ++val.y, ++val.z, ++val.w);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float4 operator --(float4 val) => new float4(--val.x, --val.y, --val.z, --val.w);

        #region Unity相关
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float4(Color val) => new float4(val.r, val.g, val.b, val.a);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Color(float4 val) => new Color(val.x, val.y, val.z, val.w);
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2 ^ w.GetHashCode() >> 1;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => $"float4({x}f, {y}f, {z}f, {w}f)";
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider) => $"float4({x.ToString(format, formatProvider)}f, {y.ToString(format, formatProvider)}f, {z.ToString(format, formatProvider)}f, {w.ToString(format, formatProvider)}f)";
    }
}