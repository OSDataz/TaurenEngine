/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/9 17:43:40
 *└────────────────────────┘*/

using TaurenEngine.Framework;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	[CustomEditor(typeof(HotfixComponent))]
	public class HotfixComponentEditor : UnityEditor.Editor
	{
		private HotfixEditorData _hotfixData;
		private HotfixDllDrawer _dllDrawer;

		protected void OnEnable()
		{
			_hotfixData ??= HotfixEditorData.Instance;
			_dllDrawer ??= new HotfixDllDrawer();
			var reList = _hotfixData.Dlls.InitReorderableList();
			reList.drawHeaderCallback = OnDrawHeader;
			reList.drawElementCallback = OnDrawElement;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			_hotfixData.IsDidReloadScripts.Draw("修改代码后自动更新热更Dll：");

			EditorGUILayout.BeginHorizontal();
			_hotfixData.HotfixDllSavePath.Draw("热更Dll保存路径：");
			if (GUILayout.Button("更新热更Dll"))
			{
				HotfixTool.UpdateHotfixDll();
			}
			EditorGUILayout.EndHorizontal();
			_hotfixData.Dlls.ReorderableList.DoLayoutList();
		}

		private void OnDrawHeader(Rect rect)
		{
			EditorGUI.LabelField(new Rect(rect) { width = 150, x = 70 }, "热更程序集名(带后缀)");
			EditorGUI.LabelField(new Rect(rect) { width = 150, x = 250 }, "Mono引用");
		}

		private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
		{
			_dllDrawer.SetData(_hotfixData.Dlls.GetItem(index));
			_dllDrawer.Draw(rect);
		}
	}
}