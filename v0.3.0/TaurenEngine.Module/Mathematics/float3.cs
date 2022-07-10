/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/6 22:09:52
 *└────────────────────────┘*/

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TaurenEngine.Mathematics
{
    public partial struct float3 : IEquatable<float3>, IFormattable
    {
        public static readonly float3 Zero = new float3(0.0f, 0.0f, 0.0f);

        public float x;
        public float y;
        public float z;

        public float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// 量级
        /// </summary>
		public float Magnitude => (float)Math.Sqrt((double)x * (double)x + (double)y * (double)y + (double)z * (double)z);
        /// <summary>
        /// 单位向量
        /// </summary>
		public float3 Normalized
        {
            get
            {
                var num = Magnitude;
                return (double)num > 9.99999974737875E-06 ? this / num : Zero;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other) => other is float3 other1 && Equals(other1);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(float3 other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(float3 lhs, float3 rhs)
        {
            double num1 = lhs.x - rhs.x;
            double num2 = lhs.y - rhs.y;
            double num3 = lhs.z - rhs.z;
            return num1 * num1 + num2 * num2 + num3 * num3 < 9.99999943962493E-11;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(float3 lhs, float3 rhs) => !(lhs == rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator +(float3 lhs, float3 rhs) => new float3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator +(float3 lhs, float rhs) => new float3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator +(float lhs, float3 rhs) => new float3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator -(float3 lhs, float3 rhs) => new float3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator -(float3 lhs, float rhs) => new float3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator -(float lhs, float3 rhs) => new float3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator -(float3 val) => new float3(-val.x, -val.y, -val.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator *(float3 lhs, float3 rhs) => new float3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator *(float3 lhs, float rhs) => new float3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator *(float lhs, float3 rhs) => new float3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator /(float3 lhs, float3 rhs) => new float3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator /(float3 lhs, float rhs) => new float3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator /(float lhs, float3 rhs) => new float3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator %(float3 lhs, float3 rhs) => new float3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator %(float3 lhs, float rhs) => new float3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator %(float lhs, float3 rhs) => new float3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator ++(float3 val) => new float3(++val.x, ++val.y, ++val.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 operator --(float3 val) => new float3(--val.x, --val.y, --val.z);

        #region Unity相关
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator float3(Vector3 val) => new float3(val.x, val.y, val.z);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(float3 val) => new Vector3(val.x, val.y, val.z);
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => $"float3({x}f, {y}f, {z}f)";
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider) => $"float3({x.ToString(format, formatProvider)}f, {y.ToString(format, formatProvider)}f, {z.ToString(format, formatProvider)}f)";
    }
}