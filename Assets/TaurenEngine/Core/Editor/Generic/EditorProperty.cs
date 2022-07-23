/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:42:02
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine;
using UnityEditor;

namespace TaurenEditor
{
	public abstract class EditorProperty : IEditorProperty
	{
		protected SerializedProperty property;

		public virtual void SetData(SerializedProperty property)
		{
			this.property = property;
		}

		/// <summary>
		/// 序列化数据修改后，更新 序列化数据 到 存储文件数据
		/// </summary>
		protected void UpdateModified()
		{
			property.serializedObject.ApplyModifiedProperties();
		}

		public abstract void Clear();

		#region 引用对象
		private EditorProperty _parent;
		public bool IsReference { get; private set; }

		public EditorProperty Parent
		{
			get => _parent;
			set
			{
				_parent = value;
				IsReference = _parent != null && (_parent.IsReference || _parent is IEditorReference);
			}
		}

		/// <summary>
		/// 将自身从容器中移除
		/// </summary>
		public void RemoveSelf()
		{
			if (_parent is IEditorReference reference)
			{
				Clear();
				reference.Remove(this);
			}
		}
		#endregion

		#region 引用子对象
		private List<EditorProperty> _childs;

		protected void AddChild(EditorProperty child)
		{
			if (_childs == null)
				_childs = new List<EditorProperty>();

			_childs.AddUnique(child);
		}

		internal virtual void UpdateProperty(bool updateSelf)
		{
			if (updateSelf)
				SetData(property);

			if (_childs != null)
			{
				var len = _childs.Count;
				for (int i = 0; i < len; ++i)
				{
					_childs[i].UpdateProperty(true);
				}
			}
		}
		#endregion
	}
}