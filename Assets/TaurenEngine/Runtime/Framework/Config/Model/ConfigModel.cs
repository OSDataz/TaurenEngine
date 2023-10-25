/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/16 22:00:10
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine.Runtime
{
	/// <summary>
	/// 配置表数据
	/// </summary>
	public class ConfigModel : ModelBase
	{
		public readonly Dictionary<Type, object> configMap = new Dictionary<Type, object>();

		public override void Clear()
		{
			base.Clear();

			configMap.Clear();
		}

		/// <summary>
		/// 配置表加载完成后传入
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public List<T> GetList<T>() where T : IConfig
		{
			if (configMap.TryGetValue(typeof(T), out var value) && value is List<T> configList)
				return configList;

			Log.Error($"未找到配置表：{typeof(T)}");
			return null;
		}

		public T Get<T>(Predicate<T> match) where T : IConfig
		{
			var configList = GetList<T>();
			if (configList == null)
			{
				Log.Error($"未找到配置表：{typeof(T)}");
				return default;
			}

			return configList.Find(match);
		}

		public T Get<T>(int index) where T : IConfig
		{
			var configList = GetList<T>();
			if (configList == null)
			{
				Log.Error($"未找到配置表：{typeof(T)}");
				return default;
			}

			if (index >= configList.Count)
			{
				Log.Error($"获取配置表数据长度异常，Index:{index} Count:{configList.Count}");
				return default;
			}

			return configList[index];
		}

		/// <summary>
		/// 获取Map表
		/// </summary>
		/// <returns></returns>
		public T GetMap<T>() where T : IConfig
		{
			if (configMap.TryGetValue(typeof(T), out var value) && value is T config)
				return config;

			Log.Error($"未找到配置表：{typeof(T)}");
			return default(T);
		}
	}
}