﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/22 16:11:37
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	public interface IAudioService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IAudioService Instance { get; internal set; }
	}

	public static class IAudioServiceExtension
	{
		public static void InitInterface(this IAudioService @object)
		{
			if (IAudioService.Instance != null)
				Log.Error("IAudioService重复创建实例");
			
			IAudioService.Instance = @object;
		}
	}
}