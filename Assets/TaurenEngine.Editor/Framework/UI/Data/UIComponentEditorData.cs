/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/2 14:41:14
 *└────────────────────────┘*/

namespace TaurenEngine.Editor.Framework
{
	public sealed class UIComponentEditorData : EditorDataSingleton<UIComponentEditorData, UIComponentData>
	{
		protected override void UpdateProperty()
		{
			UIPrefabPath = GetProperty(UIPrefabPath, nameof(Data.uiPrefabPath));
			GenerateSavePath = GetProperty(GenerateSavePath, nameof(Data.generateSavePath));
		}

		protected override string SavePath => "Assets/EditorConfig/UIComponentConfig.asset";

		public PropertyString UIPrefabPath { get; private set; }
		public PropertyString GenerateSavePath { get; private set; }
	}
}