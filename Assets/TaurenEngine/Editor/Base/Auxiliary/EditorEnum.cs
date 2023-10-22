/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/4 10:42:18
 *└────────────────────────┘*/

using System;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
	public class EditorEnum<T> where T : Enum
	{
		private EditorEnumData _enumData;
		private int _index;

		public EditorEnum()
		{
			_enumData = EditorEnumData.Get<T>();
		}

		public void Draw(params GUILayoutOption[] options)
		{
			_index = EditorGUILayout.IntPopup(_index, _enumData.tagArray, _enumData.intArray, options);
		}

		public void Draw(string label, params GUILayoutOption[] options)
		{
			_index = EditorGUILayout.IntPopup(label, _index, _enumData.tagArray, _enumData.intArray, options);
		}

		public void Draw(Rect rect)
		{
			_index = EditorGUI.IntPopup(rect, _index, _enumData.tagArray, _enumData.intArray);
		}

		public T Value => (T)Enum.ToObject(typeof(T), _index);
	}
}