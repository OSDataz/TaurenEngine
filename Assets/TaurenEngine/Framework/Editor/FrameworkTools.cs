/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/22 16:22:33
 *└────────────────────────┘*/

using TaurenEngine.Framework;
using UnityEditor;
using UnityEngine;

namespace TaurenEditor.Framework
{
	public static class FrameworkTools
	{
		[MenuItem("GameObject/初始化TaurenFramework组件", false, 20)]
		private static void InitFramework()
		{
			var taurenGo = GameObject.Find("TaurenFramework");
			if (taurenGo == null)
				taurenGo = new GameObject("TaurenFramework");

			CreateFramework<AudioComponent>(taurenGo, "Audio");
			CreateFramework<ResourceComponent>(taurenGo, "Resource");
			CreateFramework<NetworkComponent>(taurenGo, "Network");
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