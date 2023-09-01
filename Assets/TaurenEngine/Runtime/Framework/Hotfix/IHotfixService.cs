﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2023/1/22 22:43:55
 *└────────────────────────┘*/

namespace TaurenEngine.Runtime.Framework
{
	public interface IHotfixService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IHotfixService Instance { get; internal set; }
	}

	public static class IHotfixServiceExtension
	{
		public static void InitInterface(this IHotfixService @object)
		{
			if (IHotfixService.Instance != null)
				Log.Error("IHotfixService重复创建实例");

			IHotfixService.Instance = @object;
		}
	}
}