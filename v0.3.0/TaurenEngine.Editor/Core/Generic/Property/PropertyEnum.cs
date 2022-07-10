/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/27 14:39:30
 *└────────────────────────┘*/

using System;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
	public class PropertyEnum<T> : EditorProperty where T : Enum
	{
		public override void Clear() => Value = 0;

		public int Value
		{
			get => property.enumValueIndex;
			set
			{
				if (property.enumValueIndex == value)
					return;

				property.enumValueIndex = value;
				UpdateModified();
			}
		}

		public T ValueEnum
		{
			get => (T)Enum.ToObject(typeof(T), property.enumValueIndex);
			set { property.enumValueIndex = (int)(object)value; }
		}

		public void Draw(string label)
		{
			ValueEnum = (T)EditorGUILayout.EnumPopup(label, ValueEnum);
		}

		public void Draw(Rect rect)
		{
			ValueEnum = (T)EditorGUI.EnumPopup(rect, ValueEnum);
		}

		public void DrawEnum(string label)
		{
			var editorEnum = EditorEnum.Get<T>();

			Value = EditorGUILayout.IntPopup(label, property.enumValueIndex, editorEnum.tagArray, editorEnum.intArray);
		}

		public void DrawEnum(Rect rect)
		{
			var editorEnum = EditorEnum.Get<T>();

			Value = EditorGUI.IntPopup(rect, property.enumValueIndex, editorEnum.tagArray, editorEnum.intArray);
		}
	}
}