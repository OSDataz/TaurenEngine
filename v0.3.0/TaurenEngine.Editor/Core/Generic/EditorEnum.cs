/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/27 20:46:42
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.Reflection;
using TaurenEngine.Core;

namespace TaurenEngine.Editor
{
	/// <summary>
	/// 枚举编辑器显示解析数据
	/// </summary>
    public sealed class EditorEnum
    {
		#region 静态管理
		private static Dictionary<Type, EditorEnum> _dictionary;

		public static EditorEnum Get<T>() where T : Enum
		{
			_dictionary ??= new Dictionary<Type, EditorEnum>();
			if (!_dictionary.TryGetValue(typeof(T), out var enumPopup))
			{
				enumPopup = new EditorEnum();
				enumPopup.Init<T>();
				_dictionary.Add(typeof(T), enumPopup);
			}

			return enumPopup;
		}

		/// <summary>
		/// 刷新所有枚举列表
		/// </summary>
		public static void RefreshAll()
		{
			if (_dictionary == null)
				return;

			foreach (var kv in _dictionary)
			{
				kv.Value.Refresh();
			}
		}
		#endregion

		private Type _type;

		/// <summary>
		/// 显示标签
		/// </summary>
		public string[] tagArray;
		/// <summary>
		/// 枚举名
		/// </summary>
		public string[] nameArray;
		/// <summary>
		/// 枚举Int数据
		/// </summary>
		public int[] intArray;
		/// <summary>
		/// 枚举下标
		/// </summary>
		public int[] indexArray;

		private void Init<T>() where T : Enum
		{
			_type = typeof(T);

			Refresh();
		}

		/// <summary>
		/// 刷新枚举列表
		/// </summary>
		private void Refresh()
		{
			var fields = _type.GetFields();
			var length = fields.Length;
			tagArray = new string[length - 1];
			nameArray = new string[length - 1];
			intArray = new int[length - 1];
			indexArray = new int[length - 1];

			for (int i = 1; i < length; ++i)
			{
				var field = fields[i];
				var j = i - 1;
				tagArray[j] = field.GetCustomAttribute<TagAttribute>()?.tag ?? field.Name;
				nameArray[j] = field.Name;
				intArray[j] = (int)Enum.Parse(_type, field.Name);
				indexArray[j] = j;
			}
		}
	}
}