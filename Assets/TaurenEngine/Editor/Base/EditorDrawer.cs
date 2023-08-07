/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/9 18:43:56
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
	public abstract class EditorDrawer
	{
		protected SerializedProperty property;

		public virtual void SetData(SerializedProperty property)
		{
			this.property = property;
		}

		public abstract void Draw(Rect rect);

		#region 序列化字段
		protected SerializedProperty GetProperty(string propertyPath)
		{
			return property.FindPropertyRelative(propertyPath);
		}
		#endregion

		#region 编辑字段
		protected TProperty GetProperty<TProperty>(string propertyPath)
			where TProperty : EditorProperty, new()
		{
			var property = new TProperty();
			property.SetData(GetProperty(propertyPath));
			return property;
		}

		protected TProperty GetProperty<TProperty>(TProperty property, string propertyPath)
			where TProperty : EditorProperty, new()
		{
			property ??= new TProperty();
			property.SetData(GetProperty(propertyPath));
			return property;
		}

		protected TObject GetObject<TObject, TData>(TData data, string propertyPath)
			where TObject : EditorObject<TData>, new()
		{
			var tObject = new TObject();
			tObject.SetData(data, GetProperty(propertyPath));
			return tObject;
		}

		protected TObject GetObject<TObject, TData>(TObject tObject, TData data, string propertyPath)
			where TObject : EditorObject<TData>, new()
		{
			tObject ??= new TObject();
			tObject.SetData(data, GetProperty(propertyPath));
			return tObject;
		}
		#endregion
	}
}