/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/10/2 20:09:43
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using UnityEditorInternal;

namespace TaurenEngine.Editor
{
	/// <summary>
	/// 序列化列表删除时，所有序列化对象都会打乱，需要重新获取
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PropertyList<T> : EditorProperty, IEditorReference where T : EditorProperty, new()
	{
		#region 初始化
		private List<T> _list;
		public List<T> List
		{
			get
			{
				if (_list == null)
				{
					_list = new List<T>();

					var len = property.arraySize;
					for (int i = 0; i < len; ++i)
					{
						var item = new T();
						item.Parent = this;
						item.SetData(property.GetArrayElementAtIndex(i));
						_list.Add(item);
					}
				}
				return _list;
			}
		}
		#endregion

		#region 重载更新
		internal override void UpdateProperty(bool updateSelf)
		{
			var len = Math.Min(property.arraySize, List.Count);
			for (int i = 0; i < len; ++i)
			{
				List[i].SetData(property.GetArrayElementAtIndex(i));
				List[i].UpdateProperty(updateSelf);
			}
		}
		#endregion

		public void Add() => Add(property.arraySize);

		public void Add(int index)
		{
			if (index < 0 && index > property.arraySize)
				return;

			property.InsertArrayElementAtIndex(index);

			var item = new T();
			item.Parent = this;
			List.Add(item);

			UpdateProperty(false);
			item.Clear();

			UpdateModified();
		}

		public void Remove(IEditorProperty item)
		{
			if (item is T tItem)
				Remove(tItem);
		}

		public void Remove(T item) => Remove(List.IndexOf(item));

		public void Remove(int index)
		{
			if (index < 0 && index >= property.arraySize)
				return;

			property.DeleteArrayElementAtIndex(index);

			List.RemoveAt(index);
			UpdateProperty(false);

			UpdateModified();
		}

		public override void Clear()
		{
			_list?.Clear();
			property.ClearArray();

			UpdateModified();
		}

		public int Length => property.arraySize;

		#region 绘制列表 - 待完善
		private ReorderableList _reList;
		/// <summary>
		/// 初始化绘制
		/// <para>绘制头部栏 <c>drawHeaderCallback = rect => {}</c>，对应<c>displayHeader</c></para>
		/// <para>绘制列表项 <c>drawElementCallback = (rect, index, isActive, isFocused) => {}</c>，若item赋值则不需要设置</para>
		/// <para>处理旋转回调 <c>onSelectCallback = list => {}</c></para>
		/// <para>处理添加项 <c>onAddCallback = list => {}</c>, 对应<c>displayAddButton</c></para>
		/// <para>处理删除项 <c>onRemoveCallback = list => {}</c>，对应<c>displayRemoveButton</c></para>
		/// </summary>
		/// <param name="draggable">是否能拖动</param>
		/// <param name="displayHeader">是否显示头部栏</param>
		/// <param name="displayAddButton">是否显示添加按钮</param>
		/// <param name="displayRemoveButton">是否显示删除按钮</param>
		public void InitReorderableList(
			bool draggable = true,
			bool displayHeader = true,
			bool displayAddButton = true,
			bool displayRemoveButton = true)
		{
			if (_reList == null)
			{
				_reList = new ReorderableList(property.serializedObject, property,
					draggable, displayHeader, displayAddButton, displayRemoveButton);
			}
			else
			{
				_reList.draggable = draggable;
				_reList.displayAdd = displayAddButton;
				_reList.displayRemove = displayRemoveButton;
			}
		}

		public ReorderableList ReorderableList
		{
			get 
			{
				if (_reList == null)
					_reList = new ReorderableList(property.serializedObject, property);

				return _reList;
			}
		}
		#endregion
	}
}