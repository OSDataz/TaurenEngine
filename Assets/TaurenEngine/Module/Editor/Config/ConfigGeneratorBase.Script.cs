/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/9 20:54:11
 *└────────────────────────┘*/

using System.Collections.Generic;
using System.Data;
using System.Text;
using TaurenEngine.Editor;
using UnityEngine;

namespace TaurenEditor.ModConfig
{
	/// <summary>
	/// 生成代码
	/// </summary>
	public abstract partial class ConfigGeneratorBase
	{
		protected virtual void ClearScriptData()
		{
			if (enumMap != null)
				enumMap.Clear();

			if (subClassMap != null)
				subClassMap.Clear();

			if (preloadList != null)
				preloadList.Clear();
		}

		/// <summary>
		/// 生成代码类
		/// </summary>
		/// <param name="excelPaths"></param>
		/// <param name="savePath"></param>
		/// <param name="scriptNamespace"></param>
		/// <param name="encoding"></param>
		public virtual void GenerateScript(
			string[] excelPaths,
			string savePath,
			string scriptNamespace,
			Encoding encoding)
		{
			ClearScriptData();
		}

		#region 枚举数据
		protected struct EnumField
		{
			/// <summary> 字段名 </summary>
			public string name;
			/// <summary> 枚举值 </summary>
			public int value;
			/// <summary> 注释 </summary>
			public string note;
		}

		/// <summary>
		/// 枚举名，字段名，枚举值
		/// </summary>
		protected Dictionary<string, List<EnumField>> enumMap;

		protected void AddEnumTable(DataTable sheet)
		{
			var rowMax = sheet.Rows.Count;
			if (rowMax <= 1)
				return;// 配置行数异常

			if (enumMap == null) enumMap = new Dictionary<string, List<EnumField>> ();
			else enumMap.Clear();

			DataRow dataRow;
			string enumName;
			string fieldName;
			List<EnumField> fields = null;

			for (int row = 1; row < rowMax; ++row)// 从第二行开始查找
			{
				dataRow = sheet.Rows[row];

				enumName = dataRow[0].ToString();// 枚举名
				if (!string.IsNullOrWhiteSpace(enumName))
				{
					if (enumName == Key_Note)
						continue;// 注释跳转

					if (enumMap.TryGetValue(enumName, out var value))
					{
						Debug.LogError($"枚举类名重复：{enumName}");
						return;
					}
					else
					{
						fields = new List<EnumField> ();
						enumMap.Add(enumName, fields);
					}
				}

				fieldName = dataRow[1].ToString();// 枚举字段名
				if (string.IsNullOrWhiteSpace(fieldName))
					return;// 无数据异常

				var field = new EnumField();
				field.name = fieldName;
				field.value = int.TryParse(dataRow[2].ToString(), out var enumValue) ? enumValue : 0;
				field.note = dataRow[3].ToString();

				if (fields == null)
					return;

				fields.Add(field);
			}
		}

		protected int GetEnumValue(string enumName, string enumItem)
		{
			if (enumMap.TryGetValue(enumName, out var list))
			{
				foreach (var kv in list)
				{
					if (kv.name == enumItem)
						return kv.value;
				}
			}

			return 0;
		}
		#endregion

		#region 子类数据
		protected struct ClassField
		{
			/// <summary> 字段名 </summary>
			public string name;
			/// <summary> 字段类型 </summary>
			public string type;
			/// <summary> 注释 </summary>
			public string note;
		}

		/// <summary>
		/// 类名，字段名，字段类型
		/// </summary>
		protected Dictionary<string, List<ClassField>> subClassMap;

		protected void AddSubClass(DataTable sheet)
		{
			var rowMax = sheet.Rows.Count;
			if (rowMax <= 1)
				return;// 配置行数异常

			if (subClassMap == null) subClassMap = new Dictionary<string, List<ClassField>>();
			else subClassMap.Clear();

			DataRow dataRow;
			string className;
			string fieldName;
			List<ClassField> fields = null;

			for (int row = 1; row < rowMax; ++row)// 从第二行开始查找
			{
				dataRow = sheet.Rows[row];

				className = dataRow[0].ToString();// 子类名
				if (!string.IsNullOrWhiteSpace(className))
				{
					if (className == Key_Note)
						continue;// 注释跳转

					if (subClassMap.TryGetValue(className, out var value))
					{
						Debug.LogError($"子类名重复：{className}");
						return;
					}
					else
					{
						fields = new List<ClassField>();
						subClassMap.Add(className, fields);
					}
				}

				fieldName = dataRow[1].ToString();// 子类字段名
				if (string.IsNullOrWhiteSpace(fieldName))
					return;// 无数据异常

				var field = new ClassField();
				field.name = fieldName;
				field.type = dataRow[2].ToString();
				field.note = dataRow[3].ToString();

				if (fields == null)
					return;

				fields.Add(field);
			}
		}

		protected string GetSubClassPropertyType(string className, string propertyName)
		{
			if (subClassMap.TryGetValue(className, out var list))
			{
				foreach (var kv in list)
				{
					if (kv.name == propertyName)
						return kv.type;
				}
			}

			return string.Empty;
		}
		#endregion

		#region 预加载数据
		protected struct PreloadData
		{
			public string path;
			public string className;
			public bool isMap;
		}

		protected List<PreloadData> preloadList = new List<PreloadData>();

		protected void AddPreloadConfig(string path, string className, bool isMap)
		{
			preloadList.Add(new PreloadData()
			{
				path = path,
				className = className,
				isMap = isMap
			});
		}
		#endregion

		#region 公用函数
		/// <summary>
		/// 配置文件字段类型
		/// </summary>
		protected enum ConfigPropertyType
		{
			Value,
			Class,
			ListClass
		}

		protected string ToSheetScriptName(string name)
		{
			return $"Ex{name.ToUpperFirst()}";
		}

		protected string FormatNote(string name)
		{
			return name.Replace("\n", "").Replace("\r", "");
		}

		protected string FormatPropertyType(string type, out ConfigPropertyType pType, out string subDataType)
		{
			var list = type.Split(Key_Split);
			var typeLow = list[0].ToLower();

			if (typeLow == "int" || typeLow == "int32")
			{
				pType = ConfigPropertyType.Value;
				subDataType = "int";
				return subDataType;
			}
			else if (typeLow == "long" || typeLow == "int64")
			{
				pType = ConfigPropertyType.Value;
				subDataType = "long";
				return subDataType;
			}
			else if (typeLow == "string"
				|| typeLow == "bool"
				|| typeLow == "float"
				|| typeLow == "double"
				)
			{
				pType = ConfigPropertyType.Value;
				subDataType = typeLow;
				return typeLow;
			}
			else if (typeLow == "vector2"
				|| typeLow == "vector3"
				|| typeLow == "vector4"
				|| typeLow == "color")
			{
				pType = ConfigPropertyType.Value;
				subDataType = "UnityEngine." + typeLow.ToUpperFirst();
				return subDataType;
			}
			else if (list.Length >= 2)
			{
				if (typeLow == "enum")
				{
					pType = ConfigPropertyType.Value;
					subDataType = list[1];
					return subDataType;
				}
				else if (typeLow == "class")
				{
					pType = ConfigPropertyType.Class;
					subDataType = $"{list[1]}.Data";
					return list[1];
				}
				else if (typeLow == "list")
				{
					if (list[1].ToLower() == "class")
						pType = ConfigPropertyType.ListClass;
					else
						pType = ConfigPropertyType.Value;

					var subType = FormatPropertyType(type.Substring(type.IndexOf('#') + 1), out var outValue, out var outSubDataType);
					subDataType = $"System.Collections.Generic.List<{outSubDataType}>";
					return $"System.Collections.Generic.List<{subType}>";
				}
			}

			pType = ConfigPropertyType.Value;
			subDataType = string.Empty;

			Debug.LogError($"字段类型异常：{type}");
			return type;
		}

		/// <summary>
		/// 在第一列查找一个指定字符串
		/// </summary>
		/// <param name="sheet"></param>
		/// <param name="content"></param>
		/// <returns></returns>
		protected int FindInCol_0(DataTable sheet, string content)
		{
			var len = sheet.Rows.Count;
			for (int i = 2; i < len; ++i)
			{
				if (sheet.Rows[i][0].ToString() == content)
					return i;
			}

			return -1;
		}
		#endregion
	}
}