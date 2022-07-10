/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/27 14:39:16
 *└────────────────────────┘*/

using System;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
	public class PropertyString : EditorProperty
	{
		public override void Clear() => Value = string.Empty;

		public string Value
		{
			get => property.stringValue;
			set
			{
				if (property.stringValue == value)
					return;

				property.stringValue = value;
				UpdateModified();
			}
		}

		public void Draw()
		{
			Value = EditorGUILayout.TextField(property.stringValue);
		}

		public void Draw(string label)
		{
			Value = EditorGUILayout.TextField(label, property.stringValue);
		}

		public void Draw(Rect rect)
		{
			Value = EditorGUI.TextField(rect, property.stringValue);
		}

		public void DrawLabel()
		{
			EditorGUILayout.LabelField(property.stringValue);
		}

		public void DrawLabel(string label)
		{
			EditorGUILayout.LabelField(label, property.stringValue);
		}

		public void DrawLabel(Rect rect)
		{
			EditorGUI.LabelField(rect, property.stringValue);
		}

		public void DrawEnum<T>(string label) where T : Enum
		{
			var editorEnum = EditorEnum.Get<T>();

			var value = property.stringValue;
			var index = Array.FindIndex(editorEnum.nameArray, item => item == value);

			var newIndex = EditorGUILayout.IntPopup(label, index, editorEnum.tagArray, editorEnum.indexArray);
			if (newIndex != index && newIndex >= -1)
			{
				Value = editorEnum.nameArray[newIndex];
			}
		}

		public void DrawEnum<T>(Rect rect) where T : Enum
		{
			var editorEnum = EditorEnum.Get<T>();

			var value = property.stringValue;
			var index = Array.FindIndex(editorEnum.nameArray, item => item == value);

			var newIndex = EditorGUI.IntPopup(rect, index, editorEnum.tagArray, editorEnum.indexArray);
			if (newIndex != index && newIndex >= -1)
			{
				Value = editorEnum.nameArray[newIndex];
			}
		}
	}
}