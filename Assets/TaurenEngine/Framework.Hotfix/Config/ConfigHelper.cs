/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.6.0
 *│　Time    ：2022/6/26 18:00:53
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using TaurenEngine.Core;

namespace TaurenEngine.Framework.Hotfix
{
	public interface IConfig
	{
		void SetData(object jsonData);
	}

	/// <summary>
	/// 配置表辅助类
	/// </summary>
	public class ConfigHelper
	{
		private static Dictionary<Type, object> configMap = new Dictionary<Type, object>();

		/// <summary>
		/// 配置表加载完成后传入
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="configList"></param>
		public static void Add<T>(List<T> configList) where T : IConfig
		{
			var type = typeof(T);
			if (configMap.ContainsKey(type))
			{
				Debugx.Warning("重复添加配置表");
				return;
			}

			configMap.Add(type, configList);
		}

		public static void Add<T>(T config) where T : IConfig
		{
			var type = typeof(T);
			if (configMap.ContainsKey(type))
			{
				Debugx.Warning("重复添加配置表");
				return;
			}

			configMap.Add(type, config);
		}

		public static void Remove<T>() where T : IConfig
		{
			configMap.Remove(typeof(T));
		}

		public static List<T> GetList<T>() where T : IConfig
		{
			if (configMap.TryGetValue(typeof(T), out var value) && value is List<T> configList)
				return configList;

			return null;
		}

		public static T Get<T>(Predicate<T> match) where T : IConfig
		{
			var configList = GetList<T>();
			if (configList == null)
				return default;

			return configList.Find(match);
		}

		/// <summary>
		/// 获取全局表
		/// </summary>
		/// <returns></returns>
		public static T Get<T>() where T : IConfig
		{
			if (configMap.TryGetValue(typeof(T), out var value) && value is T config)
				return config;

			return default(T);
		}
	}
}