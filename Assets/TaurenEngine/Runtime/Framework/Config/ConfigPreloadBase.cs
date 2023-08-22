/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/16 21:49:02
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine.Runtime.Framework
{
	public abstract class ConfigPreloadBase
	{
		protected Action onAllLoadComplete;

		protected int configCount;

		protected void Load(string path, Action<string> onLoadComplete)
		{
			
		}

		protected void CheckLoadComplete()
		{
			if (configCount == 1)
			{
				onAllLoadComplete?.Invoke();

				
			}
			else
				configCount -= 1;
		}

		protected void ParseConfig<TConfig, TData>(string json) where TConfig : IConfig, new()
		{
			//Log.Print($"解析配置表：{typeof(TData)}\n{json}");
			var jsonList = JsonHelper.ToObject<List<TData>>(json);
			var ecList = new List<TConfig>();
			var len = jsonList.Count;
			for (int i = 0; i < len; ++i)
			{
				var jsonItem = new TConfig();
				jsonItem.SetData(jsonList[i]);
				ecList.Add(jsonItem);
			}

			InstanceManager.Instance.Get<ConfigController>().Add<TConfig>(ecList);
		}

		protected void ParseMapConfig<TConfig, TData>(string json) where TConfig : IConfig, new()
		{
			//Log.Print($"解析配置表：{typeof(TData)}\n{json}");
			var jsonData = JsonHelper.ToObject<TData>(json);
			var jsonItem = new TConfig();
			jsonItem.SetData(jsonData);

			InstanceManager.Instance.Get<ConfigController>().Add<TConfig>(jsonItem);
		}
	}
}