/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/15 21:57:16
 *└────────────────────────┘*/

using UnityEditor;

namespace Tauren.Core.Editor
{
	public abstract class DataWindow<T> : EditorWindow where T : IEditorData
	{
		protected T editorData;

		protected abstract T CreateEditorData();

		protected virtual void OnEnable()
		{
			editorData ??= CreateEditorData();
		}

		protected virtual void OnDisable()
		{
			editorData.SaveAssets();
		}

		protected virtual void OnGUI()
		{
			editorData.SerializedObject.ApplyModifiedProperties();
		}
	}
}