/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/6 22:09:26
 *└────────────────────────┘*/

using System;
using System.Runtime.CompilerServices;

namespace TaurenEngine.Mathematics
{
    public struct double2 : IEquatable<double2>, IFormattable
    {
        public static readonly double2 Zero = new double2(0, 0);

        public double x;
        public double y;

        public double2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// 量级
        /// </summary>
        public double Magnitude => Math.Sqrt(x * x + y * y);
        /// <summary>
        /// 单位向量
        /// </summary>
        public double2 Normalized
        {
            get
            {
                var num = Magnitude;
                return num > 9.99999974737875E-06 ? this / num : Zero;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other) => other is double2 other1 && Equals(other1);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(double2 other) => this == other;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(double2 lhs, double2 rhs)
        {
            var num1 = lhs.x - rhs.x;
            var num2 = lhs.y - rhs.y;
            return num1 * num1 + num2 * num2 < 9.99999943962493E-11;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(double2 lhs, double2 rhs) => !(lhs == rhs);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator +(double2 lhs, double2 rhs) => new double2(lhs.x + rhs.x, lhs.y + rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator +(double2 lhs, double rhs) => new double2(lhs.x + rhs, lhs.y + rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator +(double lhs, double2 rhs) => new double2(lhs + rhs.x, lhs + rhs.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator -(double2 lhs, double2 rhs) => new double2(lhs.x - rhs.x, lhs.y - rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator -(double2 lhs, double rhs) => new double2(lhs.x - rhs, lhs.y - rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator -(double lhs, double2 rhs) => new double2(lhs - rhs.x, lhs - rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator -(double2 val) => new double2(-val.x, -val.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator *(double2 lhs, double2 rhs) => new double2(lhs.x * rhs.x, lhs.y * rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator *(double2 lhs, double rhs) => new double2(lhs.x * rhs, lhs.y * rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator *(double lhs, double2 rhs) => new double2(lhs * rhs.x, lhs * rhs.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator /(double2 lhs, double2 rhs) => new double2(lhs.x / rhs.x, lhs.y / rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator /(double2 lhs, double rhs) => new double2(lhs.x / rhs, lhs.y / rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator /(double lhs, double2 rhs) => new double2(lhs / rhs.x, lhs / rhs.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator %(double2 lhs, double2 rhs) => new double2(lhs.x % rhs.x, lhs.y % rhs.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator %(double2 lhs, double rhs) => new double2(lhs.x % rhs, lhs.y % rhs);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator %(double lhs, double2 rhs) => new double2(lhs % rhs.x, lhs % rhs.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator ++(double2 val) => new double2(++val.x, ++val.y);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double2 operator --(double2 val) => new double2(--val.x, --val.y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString() => $"double2({x}, {y})";
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider) => $"double2({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)})";
    }
}