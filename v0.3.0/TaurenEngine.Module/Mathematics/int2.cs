/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/6 22:10:14
 *└────────────────────────┘*/

using System;
using System.Runtime.CompilerServices;

namespace TaurenEngine.Mathematics
{
	public struct int2 : IEquatable<int2>, IFormattable
	{
		public int x;
		public int y;

		public int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other) => other is int2 other1 && Equals(other1);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int2 other) => this == other;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(int2 lhs, int2 rhs) => lhs.x == rhs.x && lhs.y == rhs.y;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(int2 lhs, int2 rhs) => !(lhs == rhs);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int2 lhs, int2 rhs) => new int2(lhs.x + rhs.x, lhs.y + rhs.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int2 lhs, int rhs) => new int2(lhs.x + rhs, lhs.y + rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator +(int lhs, int2 rhs) => new int2(lhs + rhs.x, lhs + rhs.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int2 lhs, int2 rhs) => new int2(lhs.x - rhs.x, lhs.y - rhs.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int2 lhs, int rhs) => new int2(lhs.x - rhs, lhs.y - rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int lhs, int2 rhs) => new int2(lhs - rhs.x, lhs - rhs.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator -(int2 val) => new int2(-val.x, -val.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator *(int2 lhs, int2 rhs) => new int2(lhs.x + rhs.x, lhs.y + rhs.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator *(int2 lhs, int rhs) => new int2(lhs.x * rhs, lhs.y * rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator *(int lhs, int2 rhs) => new int2(lhs * rhs.x, lhs * rhs.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator /(int2 lhs, int2 rhs) => new int2(lhs.x / rhs.x, lhs.y / rhs.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator /(int2 lhs, int rhs) => new int2(lhs.x / rhs, lhs.y / rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator /(int lhs, int2 rhs) => new int2(lhs / rhs.x, lhs / rhs.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator %(int2 lhs, int2 rhs) => new int2(lhs.x % rhs.x, lhs.y % rhs.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator %(int2 lhs, int rhs) => new int2(lhs.x % rhs, lhs.y % rhs);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator %(int lhs, int2 rhs) => new int2(lhs % rhs.x, lhs % rhs.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator ++(int2 val) => new int2(++val.x, ++val.y);
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 operator --(int2 val) => new int2(--val.x, --val.y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() << 2;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString() => $"int2({x}, {y})";
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider) => $"int2({x.ToString(format, formatProvider)}, {y.ToString(format, formatProvider)})";
	}
}