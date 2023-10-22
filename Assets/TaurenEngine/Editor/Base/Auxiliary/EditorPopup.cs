/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/10/2 20:56:21
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
	public class EditorPopup : EditorPopup<string>
	{
		public EditorPopup(string[] contentList) : base(contentList, contentList) { }
	}

	public class EditorPopup<T>
	{
		private int _index;
		private int[] _indexList;
		private string[] _displayList;
		private T[] _contentList;

		public EditorPopup(string[] displayList, T[] contentList)
		{
			_displayList = displayList;
			_contentList = contentList;

			var len = Mathf.Min(displayList.Length, contentList.Length);
			_indexList = new int[len];
			for (int i = 0; i < len; ++i)
			{
				_indexList[i] = i;
			}
		}

		public void Draw(params GUILayoutOption[] options)
		{
			_index = EditorGUILayout.IntPopup(_index, _displayList, _indexList, options);
		}

		public void Draw(string label, params GUILayoutOption[] options)
		{
			_index = EditorGUILayout.IntPopup(label, _index, _displayList, _indexList, options);
		}

		public T Draw(string label, T value, params GUILayoutOption[] options)
		{
			var len = _contentList.Length;
			for (_index = 0; _index < len; ++_index)
			{
				if (_contentList[_index].Equals(value))
				{
					_index = EditorGUILayout.IntPopup(label, _index, _displayList, _indexList, options);
					return Value;
				}
			}

			_index = 0;
			return Value;
		}

		public T Value => _contentList[_index];
	}
}