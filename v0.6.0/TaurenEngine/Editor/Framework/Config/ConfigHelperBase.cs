/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.6.0
 *│　Time    ：2022/6/26 17:36:36
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public interface IConfigHelper
	{
		void SetEnumAndSubClass(string[] files);

		void GenerateScript(ConfigData configData, string[] files, Encoding encoding);
		void GenerateConfig(ConfigData configData, string[] files, Encoding encoding);
		void GeneratePrelaodScript(ConfigData configData, Encoding encoding);
	}

	public abstract class ConfigHelperBase
	{
		protected ExcelReader reader = new ExcelReader();

		public void SetEnumAndSubClass(string[] files)
		{
			bool findEnum = false;
			bool findSubClass = false;

			var len = files.Length;
			for (int i = 0; i < len; ++i)
			{
				// 构造Excel工具类
				reader.Load(files[i]);

				// 判断Excel文件中是否存在数据表
				var tableLen = reader.DataSet.Tables.Count;
				if (tableLen < 1)
					continue;

				for (int j = 0; j < tableLen; ++j)
				{
					var sheet = reader.DataSet.Tables[j];
					if (sheet.TableName.ToLower() == "enum")
					{
						SetEnum(sheet);

						if (findSubClass)
						{
							reader.Dispose();
							return;
						}
						else
							findEnum = true;
					}
					else if (sheet.TableName.ToLower() == "class")
					{
						SetSubClass(sheet);

						if (findEnum)
						{
							reader.Dispose();
							return;
						}
						else
							findSubClass = true;
					}
				}
			}

			reader.Dispose();
		}

		#region 枚举数据
		private Dictionary<string, Dictionary<string, int>> _enumMap;

		public void SetEnum(DataTable sheet)
		{
			if (_enumMap == null) _enumMap = new Dictionary<string, Dictionary<string, int>>();
			else _enumMap.Clear();

			var index = 1;
			var enumName = sheet.Rows[index][0].ToString();

			DataRow row;
			string valueStr;
			int value = 0;

			while (!string.IsNullOrWhiteSpace(enumName))
			{
				row = sheet.Rows[index];
				if (!_enumMap.TryGetValue(enumName, out var map))
				{
					map = new Dictionary<string, int>();
					_enumMap.Add(enumName, map);
				}

				valueStr = row[2].ToString();
				if (string.IsNullOrWhiteSpace(row[2].ToString()))
				{
					map.Add(row[1].ToString(), value++);
				}
				else
				{
					var dValue = Convert.ToInt32(valueStr);
					map.Add(row[1].ToString(), dValue);
					if (dValue >= value) value = dValue + 1;
				}

				index += 1;
				if (index >= sheet.Rows.Count)
					break;

				row = sheet.Rows[index];
				if (string.IsNullOrWhiteSpace(row[0].ToString()))
				{
					if (string.IsNullOrWhiteSpace(row[1].ToString()))
						break;
				}
				else
				{
					enumName = row[0].ToString();
					value = 0;
				}
			}
		}

		public int GetEnumValue(string enumName, string enumItem)
		{
			if (_enumMap.TryGetValue(enumName, out var map) && map.TryGetValue(enumItem, out var value))
				return value;

			return 0;
		}
		#endregion

		#region 子类数据
		/// <summary>
		/// 类名，字段名，字段类型
		/// </summary>
		private Dictionary<string, Dictionary<string, string>> _subClassMap;

		protected void SetSubClass(DataTable sheet)
		{
			if (_subClassMap == null) _subClassMap = new Dictionary<string, Dictionary<string, string>>();
			else _subClassMap.Clear();

			var index = 1;
			var className = sheet.Rows[index][0].ToString();

			DataRow row;

			while (!string.IsNullOrWhiteSpace(className))
			{
				row = sheet.Rows[index];
				if (!_subClassMap.TryGetValue(className, out var map))
				{
					map = new Dictionary<string, string>();
					_subClassMap.Add(className, map);
				}
				map.Add(row[1].ToString(), row[2].ToString());

				index += 1;
				if (index >= sheet.Rows.Count)
					break;

				row = sheet.Rows[index];
				if (string.IsNullOrWhiteSpace(row[0].ToString()))
				{
					if (string.IsNullOrWhiteSpace(row[1].ToString()))
						break;
				}
				else
					className = row[0].ToString();
			}
		}

		protected string GetSubClassPropertyType(string className, string propertyName)
		{
			if (_subClassMap.TryGetValue(className, out var map) && map.TryGetValue(propertyName, out var pType))
				return pType;

			return string.Empty;
		}
		#endregion

		#region 预加载数据
		protected class PreloadData
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
		protected string FormatNote(string name)
		{
			return name.Replace("\n", "").Replace("\r", "");
		}

		protected string FormatPropertyType(string type, out ConfigPropertyType pType, out string subDataType)
		{
			var list = type.Split('#');
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
			Debug.LogError($"类型异常：{type}");
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

		protected string ToSheetScriptName(string name)
		{
			return $"ExCo{name.ToUpperFirst()}";
		}
		#endregion
	}
}