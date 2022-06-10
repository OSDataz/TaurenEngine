/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/9 17:08:16
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public sealed class HotfixEditorData : EditorDataSingleton<HotfixEditorData, HotfixData>
	{
		protected override string SavePath => "Assets/SettingConfig/Project/HotfixConfig.asset";

		protected override void UpdateProperty()
		{
			Dlls = GetProperty(Dlls, nameof(Data.dlls));
		}

		public PropertyList Dlls { get; private set; }
	}

	public sealed class HotfixDllDrawer : EditorDrawer
	{
		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			Name = GetProperty(Name, "name");
			LaunchClass = GetProperty(LaunchClass, "launchClass");
			LaunchMethod = GetProperty(LaunchMethod, "launchMethod");
			UseByMono = GetProperty(UseByMono, "useByMono");
		}

		public override void Draw(Rect rect)
		{
			Name.Draw(new Rect(rect) { width = 150, height = 21, y = rect.y + 1 });
			LaunchClass.Draw(new Rect(rect) { width = 80, height = 21, x = 195, y = rect.y + 1 });
			LaunchMethod.Draw(new Rect(rect) { width = 80, height = 21, x = 280, y = rect.y + 1 });
			UseByMono.Draw(new Rect(rect) { x = 380 });
		}

		public PropertyString Name { get; private set; }
		public PropertyString LaunchClass { get; private set; }
		public PropertyString LaunchMethod { get; private set; }
		public PropertyBool UseByMono { get; private set; }
	}
}