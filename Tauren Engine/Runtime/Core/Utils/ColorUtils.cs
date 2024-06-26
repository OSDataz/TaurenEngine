﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/9 17:31:35
 *└────────────────────────┘*/

using UnityEngine;

namespace Tauren.Core.Runtime
{
	public static class ColorUtils
	{
		/// <summary>
		/// RGBA转Color数据
		/// </summary>
		/// <param name="r">0-255</param>
		/// <param name="g">0-255</param>
		/// <param name="b">0-255</param>
		/// <param name="a">0-1</param>
		/// <returns></returns>
		public static Color Create(int r, int g, int b, float a = 1)
		{
			return new Color(r / byte.MaxValue, g / byte.MaxValue, b / byte.MaxValue, a);
		}
	}
}