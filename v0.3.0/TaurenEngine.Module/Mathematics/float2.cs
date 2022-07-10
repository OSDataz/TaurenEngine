/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/6 22:10:01
 *└────────────────────────┘*/

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TaurenEngine.Mathematics
{
    public partial struct float2 : IEquatable<float2>, IFormattable
    {
        public float x;
        public float y;

        public float2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other) => other is float2 other1 && Equals(other1);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(float2 other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(float2 lhs, float2 rhs)
        {
            double num1 = lhs.x - rhs.x;
            double num2 = lhs.y - rhs.y;
            return num1 * num1 + num2 * num2 < 9.99999943962493E-11;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(float2 lhs, float2 rhs) => !(lhs == rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator +(float2 lhs, float2 rhs) => new float2(lhs.x + rhs.x, lhs.y + rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator +(float2 lhs, float rhs) => new float2(lhs.x + rhs, lhs.y + rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator +(float lhs, float2 rhs) => new float2(lhs + rhs.x, lhs + rhs.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator -(float2 lhs, float2 rhs) => new float2(lhs.x - rhs.x, lhs.y - rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator -(float2 lhs, float rhs) => new float2(lhs.x - rhs, lhs.y - rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator -(float lhs, float2 rhs) => new float2(lhs - rhs.x, lhs - rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator -(float2 val) => new float2(-val.x, -val.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator *(float2 lhs, float2 rhs) => new float2(lhs.x * rhs.x, lhs.y * rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator *(float2 lhs, float rhs) => new float2(lhs.x * rhs, lhs.y * rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator *(float lhs, float2 rhs) => new float2(lhs * rhs.x, lhs * rhs.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator /(float2 lhs, float2 rhs) => new float2(lhs.x / rhs.x, lhs.y / rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator /(float2 lhs, float rhs) => new float2(lhs.x / rhs, lhs.y / rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator /(float lhs, float2 rhs) => new float2(lhs / rhs.x, lhs / rhs.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator %(float2 lhs, float2 rhs) => new float2(lhs.x % rhs.x, lhs.y % rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator %(float2 lhs, float rhs) => new float2(lhs.x % rhs, lhs.y % rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator %(float lhs, float2 rhs) => new float2(lhs % rhs.x, lhs % rhs.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator ++(float2 val) => new float2(++val.x, ++val.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 operator --(float2 val) => new float2(--val.x, --val.y);

        #region Unity相关
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float2(Vector2 val) => new float2(val.x, val.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2(float2 val) => new Vector2(val.x, val.y);
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => $"float2({x}f, {y}f)";
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider) => $"float2({x.ToString(format, formatProvider)}f, {y.ToString(format, formatProvider)}f)";
    }
}