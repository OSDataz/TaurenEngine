/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
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

		public void Draw(string label)
		{
			_index = EditorGUILayout.IntPopup(label, _index, _displayList, _indexList);
		}

		public T Value => _contentList[_index];
	}
}