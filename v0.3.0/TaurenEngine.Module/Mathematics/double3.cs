/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/6 22:09:41
 *└────────────────────────┘*/

using System;
using System.Runtime.CompilerServices;

namespace TaurenEngine.Mathematics
{
	public struct double3 : IEquatable<double3>, IFormattable
	{
		public double x;
		public double y;
		public double z;

		public double3(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other) => other is double3 other1 && Equals(other1);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double3 other) => this == other;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(double3 lhs, double3 rhs)
		{
			var num1 = lhs.x - rhs.x;
			var num2 = lhs.y - rhs.y;
			var num3 = lhs.z - rhs.z;
			return num1 * num1 + num2 * num2 + num3 * num3 < 9.99999943962493E-11;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(double3 lhs, double3 rhs) => !(lhs == rhs);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator +(double3 lhs, double3 rhs) => new double3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator +(double3 lhs, double rhs) => new double3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator +(double lhs, double3 rhs) => new double3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator -(double3 lhs, double3 rhs) => new double3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator -(double3 lhs, double rhs) => new double3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator -(double lhs, double3 rhs) => new double3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator -(double3 val) => new double3(-val.x, -val.y, -val.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator *(double3 lhs, double3 rhs) => new double3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator *(double3 lhs, double rhs) => new double3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator *(double lhs, double3 rhs) => new double3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator /(double3 lhs, double3 rhs) => new double3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator /(double3 lhs, double rhs) => new double3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator /(double lhs, double3 rhs) => new double3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator %(double3 lhs, double3 rhs) => new double3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator %(double3 lhs, double rhs) => new double3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator %(double lhs, double3 rhs) => new double3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator ++(double3 val) => new double3(++val.x, ++val.y, ++val.z);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator --(double3 val) => new double3(--val.x, --val.y, --val.z);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2 ^ z.GetHashCode() >> 2;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString() => $"double3({x}, {y}, {z})";
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider) => $"double3({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)}, {z.ToString(format, formatProvider)})";
	}
}