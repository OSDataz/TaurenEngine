/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/20 21:03:51
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Core;

namespace TaurenEngine.ModConfig
{
	public interface IConfigService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IConfigService Instance { get; internal set; }

		/// <summary>
		/// 添加列表配置
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configList"></param>
		void Add<T>(List<T> configList) where T : IConfig;

		/// <summary>
		/// 添加单表配置（全局表）
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="config"></param>
		void Add<T>(T config) where T : IConfig;
	}

	public static class IConfigServiceExtension
	{
		public static void InitInterface(this IConfigService @object)
		{
			if (IConfigService.Instance != null)
				Log.Error("IConfigService重复创建实例");

			IConfigService.Instance = @object;
		}
	}
}