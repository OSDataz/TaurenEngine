/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/16 22:00:18
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.Runtime
{
	public class ConfigController : ControllerBase
	{
		private ConfigModel _model = GetModel<ConfigModel>();

		public void Add<T>(List<T> configList) where T : IConfig
		{
			var type = typeof(T);
			if (_model.configMap.ContainsKey(type))
				Log.Print($"刷新配置表：{typeof(T)}");

			_model.configMap[type] = configList;
		}

		public void Add<T>(T config) where T : IConfig
		{
			var type = typeof(T);
			if (_model.configMap.ContainsKey(type))
				Log.Print($"刷新配置表：{typeof(T)}");

			_model.configMap[type] = config;
		}

		public void Remove<T>() where T : IConfig
		{
			_model.configMap.Remove(typeof(T));
		}
	}
}