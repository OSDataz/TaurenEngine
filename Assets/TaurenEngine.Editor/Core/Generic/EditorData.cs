/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:29:36
 *└────────────────────────┘*/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
	/// <summary>
	/// 编辑器存储数据
	/// </summary>
	/// <typeparam name="T">数据类</typeparam>
	public abstract class EditorData<T> where T : ScriptableObject
	{
		/// <summary>
		/// 序列化数据
		/// </summary>
		private SerializedObject _serializedObject;

		#region 更新/存储
		/// <summary>
		/// 加载存储文件数据
		/// </summary>
		/// <param name="path">存储地址（Assets/****.asset）</param>
		public void LoadData(string path)
		{
			if (Data == null)
			{
				if (File.Exists(path))
				{
					Data = AssetDatabase.LoadAssetAtPath<T>(path);
				}
				else
				{
					var directoryName = Path.GetDirectoryName(path);
					if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
						Directory.CreateDirectory(directoryName);

					Data = ScriptableObject.CreateInstance<T>();
					AssetDatabase.CreateAsset(Data, path);
					AssetDatabase.Refresh();
				}

				_serializedObject = new SerializedObject(Data);
			}
			else
			{
				AssetDatabase.Refresh();
			}

			UpdateData();
		}

		/// <summary>
		/// 设置字段
		/// </summary>
		protected abstract void UpdateData();

		/// <summary>
		/// 保存 存储文件数据 到 磁盘
		/// </summary>
		public void SaveAssets()
		{
			AssetDatabase.SaveAssets();
		}

		/// <summary>
		/// 存储文件数据修改后，更新 存储文件数据 到 序列化数据
		/// </summary>
		public void UpdateModified()
		{
			_serializedObject.UpdateIfRequiredOrScript();
		}

		/// <summary>
		/// 存储文件数据
		/// </summary>
		public T Data { get; private set; }
		#endregion

		#region 序列化字段
		private SerializedProperty GetProperty(string propertyPath)
		{
			return _serializedObject.FindProperty(propertyPath);
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