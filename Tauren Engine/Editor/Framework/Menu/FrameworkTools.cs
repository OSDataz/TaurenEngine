/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/7 21:12:23
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace Tauren.Framework.Editor
{
	public static class FrameworkTools
	{
		[MenuItem("GameObject/创建Tauren框架基础组件", false, 20)]
		private static void InitFramework()
		{
			var taurenGo = GameObject.Find("TaurenFramework");
			if (taurenGo == null)
				taurenGo = new GameObject("TaurenFramework");

			//taurenGo.GetOrAddComponent<LaunchComponent>();// 创建启动组件

			//CreateFramework<AudioComponent>(taurenGo, "Audio");// 创建音频组件
		}

		private static T CreateFramework<T>(GameObject parent, string name) where T : Component
		{
			var component = parent.GetComponentInChildren<T>();
			if (component == null)
			{
				var go = new GameObject(name);
				go.transform.SetParent(parent.transform);
				component = go.AddComponent<T>();
			}

			return component;
		}
	}
}