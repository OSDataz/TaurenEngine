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
			UseByMono = GetProperty(UseByMono, "useByMono");
		}

		public override void Draw(Rect rect)
		{
			Name.Draw(new Rect(rect) { width = 200, height = 21, y = rect.y + 1 });
			UseByMono.Draw(new Rect(rect) { x = 270 });
		}

		public PropertyString Name { get; private set; }
		public PropertyBool UseByMono { get; private set; }
	}
}