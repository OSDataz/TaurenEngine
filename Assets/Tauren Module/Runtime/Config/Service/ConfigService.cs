/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/20 21:08:17
 *└────────────────────────┘*/

using System.Collections.Generic;
using Tauren.Core.Runtime;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 配置表服务
	/// </summary>
	public class ConfigService : IConfigService
	{
		public ConfigService() 
		{
			this.InitInterface();
		}

		public void Add<T>(List<T> configList) where T : IConfig
		{
			var type = typeof(T);
			if (ConfigData.Instance.configMap.ContainsKey(type))
				Log.Print($"刷新配置表：{typeof(T)}");

			ConfigData.Instance.configMap[type] = configList;
		}

		public void Add<T>(T config) where T : IConfig
		{
			var type = typeof(T);
			if (ConfigData.Instance.configMap.ContainsKey(type))
				Log.Print($"刷新配置表：{typeof(T)}");

			ConfigData.Instance.configMap[type] = config;
		}

		public void Remove<T>() where T : IConfig
		{
			ConfigData.Instance.configMap.Remove(typeof(T));
		}
	}
}