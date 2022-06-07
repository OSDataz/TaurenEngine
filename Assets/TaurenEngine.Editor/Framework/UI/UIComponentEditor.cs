/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/2 14:16:20
 *└────────────────────────┘*/

using TaurenEngine.Framework;
using UnityEditor;

namespace TaurenEngine.Editor.Framework
{
	[CustomEditor(typeof(UIComponent))]
	public class UIComponentEditor : UnityEditor.Editor
	{
		private UIComponentEditorData _uiEditorData;

		protected void OnEnable()
		{
			_uiEditorData = UIComponentEditorData.Instance;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			_uiEditorData.UIPrefabPath.Draw("UI预制体路径：");
			_uiEditorData.GenerateSavePath.Draw("生成代码保存路径：");
			_uiEditorData.CodeNamespace.Draw("生成代码命名空间：");
		}
	}
}