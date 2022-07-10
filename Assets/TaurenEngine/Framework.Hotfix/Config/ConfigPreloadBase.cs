/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.6.0
 *│　Time    ：2022/6/26 18:32:16
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine.Framework.Hotfix
{
	public abstract class ConfigPreloadBase
	{
		protected Action onAllLoadComplete;

		protected int configCount;
		protected readonly List<ulong> resIds = new List<ulong>();

		protected void Load(string path, System.Action<string> onLoadComplete)
		{
			// todo 加载配置表
		}

		protected void CheckLoadComplete()
		{
			if (configCount == 1)
			{
				onAllLoadComplete?.Invoke();

				var len = resIds.Count;
				for (int i = 0; i < len; ++i)
				{
					// todo 卸载配置表
				}
				resIds.Clear();
			}
			else
				configCount -= 1;
		}

		protected void ParseConfig<TConfig, TData>(string json) where TConfig : IConfig, new()
		{
			//Logger.Log($"解析配置表：{typeof(TData)}\n{json}");
			//var jsonList = JsonHelper.ToObject<List<TData>>(json);
			//var ecList = new List<TConfig>();
			//var len = jsonList.Count;
			//for (int i = 0; i < len; ++i)
			//{
			//	var jsonItem = new TConfig();
			//	jsonItem.SetData(jsonList[i]);
			//	ecList.Add(jsonItem);
			//}

			//ConfigHelper.Add<TConfig>(ecList);
		}

		protected void ParseMapConfig<TConfig, TData>(string json) where TConfig : IConfig, new()
		{
			//Logger.Log($"解析配置表：{typeof(TData)}\n{json}");
			//var jsonData = JsonHelper.ToObject<TData>(json);
			//var jsonItem = new TConfig();
			//jsonItem.SetData(jsonData);
			//ConfigHelper.Add<TConfig>(jsonItem);
		}
	}
}