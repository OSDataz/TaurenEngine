/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/10/2 17:27:04
 *└────────────────────────┘*/

using UnityEditor;

namespace TaurenEditor.Unity
{
	public abstract class EditorObject<T> : EditorObject
	{
		public virtual void SetData(T data, SerializedProperty property)
		{
			Data = data;
			base.SetData(property);
		}

		public T Data { get; private set; }

		/// <summary>
		/// 存储文件数据修改后，更新 存储文件数据 到 序列化数据
		/// </summary>
		public new void UpdateModified()
		{
			property.serializedObject.UpdateIfRequiredOrScript();
		}
	}

	public abstract class EditorObject : EditorProperty
	{
		public abstract bool Draw();

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
			property.Parent = this;
			if (property.IsReference) AddChild(property);
			property.SetData(GetProperty(propertyPath));
			return property;
		}

		protected TProperty GetProperty<TProperty>(TProperty property, string propertyPath)
			where TProperty : EditorProperty, new()
		{
			if (property == null)
			{
				property = new TProperty();
				property.Parent = this;
				if (property.IsReference) AddChild(property);
			}
			property.SetData(GetProperty(propertyPath));
			return property;
		}

		protected TObject GetObject<TObject, TData>(TData data, string propertyPath)
			where TObject : EditorObject<TData>, new()
		{
			var tObject = new TObject();
			tObject.Parent = this;
			if (tObject.IsReference) AddChild(tObject);
			tObject.SetData(data, GetProperty(propertyPath));
			return tObject;
		}

		protected TObject GetObject<TObject, TData>(TObject tObject, TData data, string propertyPath)
			where TObject : EditorObject<TData>, new()
		{
			if (tObject == null)
			{
				tObject = new TObject();
				tObject.Parent = this;
				if (tObject.IsReference) AddChild(tObject);
			}
			tObject.SetData(data, GetProperty(propertyPath));
			return tObject;
		}
		#endregion
	}
}