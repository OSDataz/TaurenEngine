/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:29:36
 *└────────────────────────┘*/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace TaurenEditor
{
	/// <summary>
	/// 全局数据单例
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TData"></typeparam>
	public abstract class EditorDataSingleton<T, TData> : EditorData<TData> where T : EditorDataSingleton<T, TData>, new() where TData : ScriptableObject
	{
		private static T _instance;
		public static T Instance 
		{
			get
			{
				if (_instance == null)
				{
					_instance = new T();
					_instance.LoadData();
				}
				return _instance;
			}
		}
	}

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
		public void LoadData()
		{
			if (Data == null)
			{
				if (File.Exists(SavePath))
				{
					Data = AssetDatabase.LoadAssetAtPath<T>(SavePath);
				}
				else
				{
					var directoryName = Path.GetDirectoryName(SavePath);
					if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
						Directory.CreateDirectory(directoryName);

					Data = ScriptableObject.CreateInstance<T>();
					AssetDatabase.CreateAsset(Data, SavePath);
					AssetDatabase.Refresh();
				}

				_serializedObject = new SerializedObject(Data);
			}
			else
			{
				AssetDatabase.Refresh();
			}

			UpdateProperty();
		}

		/// <summary>
		/// 配置保存路径，后缀asset的文件
		/// </summary>
		protected abstract string SavePath { get; }

		/// <summary>
		/// 更新字段
		/// </summary>
		protected abstract void UpdateProperty();

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