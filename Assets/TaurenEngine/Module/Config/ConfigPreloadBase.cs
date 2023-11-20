/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/20 21:14:51
 *└────────────────────────┘*/

using System.Collections.Generic;
using System;
using TaurenEngine.Core;

namespace TaurenEngine.ModConfig
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

		protected void ParseConfig<TConfig, TData>(string json) where TConfig : IJonsConfig, new()
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

			IConfigService.Instance.Add(ecList);
		}

		protected void ParseMapConfig<TConfig, TData>(string json) where TConfig : IJonsConfig, new()
		{
			//Log.Print($"解析配置表：{typeof(TData)}\n{json}");
			var jsonData = JsonHelper.ToObject<TData>(json);
			var jsonItem = new TConfig();
			jsonItem.SetData(jsonData);

			IConfigService.Instance.Add(jsonItem);
		}
	}
}