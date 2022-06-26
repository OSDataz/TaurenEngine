/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/25 17:37:07
 *└────────────────────────┘*/

using TaurenEngine.Framework;
using TaurenEngine.Unity;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public static class FrameworkTools
	{
		[MenuItem("GameObject/初始化TaurenFramework组件", false, 20)]
		private static void CreateFramework()
		{
			var taurenGo = GameObjectHelper.GetOrCreateGameObject("TaurenFramework");

			CreateFramework<DebugComponent>(taurenGo, "Debug");
			CreateFramework<FrameComponent>(taurenGo, "Frame");
			CreateFramework<EventComponent>(taurenGo, "Event");
			CreateFramework<AudioComponent>(taurenGo, "Audio");
			CreateFramework<ResourceComponent>(taurenGo, "Resource");
			CreateFramework<UIComponent>(taurenGo, "UI");
			CreateFramework<CameraComponent>(taurenGo, "Camera");
			var hotfixCom = CreateFramework<HotfixComponent>(taurenGo, "Hotfix");
			CreateFramework<ILRuntimeComponent>(hotfixCom.gameObject, "ILRuntime");
			CreateFramework<LocalizationComponent>(taurenGo, "Localization");
			CreateFramework<ConfigComponent>(taurenGo, "Config");
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