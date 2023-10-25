/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/9 20:56:06
 *└────────────────────────┘*/

using System;
using System.Data;
using System.Text;
using TaurenEngine.Launch;
using UnityEngine;

namespace TaurenEditor.Runtime
{
	/// <summary>
	/// 生成配置文件
	/// </summary>
	public partial class ExcelToJsonGenerator
	{
		public override void GenerateFile(string[] excelPaths, string savePath, Encoding encoding)
		{
			var len = excelPaths.Length;
			for (int i = 0; i < len; ++i)
			{
				// 构造Excel工具类
				reader.Load(excelPaths[i]);

				// 判断Excel文件中是否存在数据表
				var tableLen = reader.DataSet.Tables.Count;
				if (tableLen < 1)
					continue;

				for (int j = 0; j < tableLen; ++j)
				{
					var sheet = reader.DataSet.Tables[j];
					var sheetNameLow = sheet.TableName.ToLower();
					if (sheetNameLow == Key_Enum || sheetNameLow == Key_Class)
						continue;// 枚举和子类表无数据

					if (IsMapSheet(sheet))
					{
						// 全局表
						GenerateMapConfig(sheet, savePath, encoding);
					}
					else
					{
						// 正常表
						GenerateConfigConfig(sheet, savePath, encoding);
					}
				}
			}
		}

		#region Map表
		private void GenerateMapConfig(DataTable sheet, string savePath, Encoding encoding)
		{
			builder1.Clear();

			var rLen = sheet.Rows.Count;
			var cLen = sheet.Columns.Count;
			var isFirst1 = true;
			int col = 0;

			builder1.Append("{");
			for (int i = 2; i < rLen; ++i)
			{
				var note = sheet.Rows[i][0].ToString();
				if (note == Key_Note)
					continue;

				if (isFirst1)
					isFirst1 = false;
				else
					builder1.Append(",");

				try
				{
					PropertyToJson(builder1, sheet, i, ref col, sheet.Rows[i][2].ToString(), sheet.Rows[i][1].ToString(), sheet.Rows[i][3].ToString());
				}
				catch (Exception e)
				{
					Debug.LogError($"配置表：{sheet.TableName} 行：{i + 1} 解析失败：{e}");
				}
			}
			builder1.Append("}");

			FileUtils.SaveText($"{savePath}/{sheet.TableName}.json", builder1.ToString(), encoding);
		}
		#endregion

		#region 正常表
		private void GenerateConfigConfig(DataTable sheet, string savePath, Encoding encoding)
		{
			builder1.Clear();

			var rLen = sheet.Rows.Count;
			var cLen = sheet.Columns.Count;
			var isFirst1 = true;
			bool isFirst2;

			builder1.Append("[");
			for (int i = 2; i < rLen; ++i)
			{
				var note = sheet.Rows[i][0].ToString();
				if (note == Key_Note || note == Key_Sub)
					continue;

				if (isFirst1)
				{
					builder1.Append("{");
					isFirst1 = false;
				}
				else
					builder1.Append(",{");

				isFirst2 = true;
				for (int j = 1; j < cLen; ++j)
				{
					if (!isFirst2)
						builder1.Append(",");
					else
						isFirst2 = false;

					try
					{
						PropertyToJson(builder1, sheet, i, ref j);
					}
					catch (Exception e)
					{
						Debug.LogError($"配置表：{sheet.TableName} 行：{i + 1} 列：{j + 1} 解析失败：{e}");
					}
				}

				builder1.Append("}");
			}
			builder1.Append("]");

			FileUtils.SaveText($"{savePath}/{sheet.TableName}.json", builder1.ToString(), encoding);
		}
		#endregion

		#region Json格式
		private void PropertyToJson(StringBuilder builder, DataTable sheet, int row, ref int col)
		{
			var pType = sheet.Rows[0][col].ToString();
			var pName = sheet.Rows[1][col].ToString();
			PropertyToJson(builder, sheet, row, ref col, pType, pName);
		}

		private void PropertyToJson(StringBuilder builder, DataTable sheet, int row, ref int col, string pType, string pName)
		{
			var pValue = sheet.Rows[row][col].ToString();
			PropertyToJson(builder, sheet, row, ref col, pType, pName, pValue);
		}

		private void PropertyToJson(StringBuilder builder, DataTable sheet, int row, ref int col, string pType, string pName, string pValue)
		{
			var list = pType.Split(Key_Split);
			var typeLow = list[0].ToLower();
			if (typeLow == "int" || typeLow == "int32"
				|| typeLow == "long" || typeLow == "int64"
				|| typeLow == "float"
				|| typeLow == "double"
				)
			{
				ValueToJson(builder, pName, pValue);
			}
			else if (typeLow == "string")
			{
				StringToJson(builder, pName, pValue);
			}
			else if (typeLow == "enum")
			{
				IntToJson(builder, pName, GetEnumValue(list[1], pValue));
			}
			else if (typeLow == "bool")
			{
				BoolToJson(builder, pName, pValue);
			}
			else if (typeLow == "vector2")
			{
				var vList = pValue.Split('_');

				NameToJson(builder, pName);
				builder.Append("{");
				ListValueToJson(builder, "x", vList, 0, "0", true);
				ListValueToJson(builder, "y", vList, 1, "0", false);
				builder.Append("}");
			}
			else if (typeLow == "vector3")
			{
				var vList = pValue.Split('_');

				NameToJson(builder, pName);
				builder.Append("{");
				ListValueToJson(builder, "x", vList, 0, "0", true);
				ListValueToJson(builder, "y", vList, 1, "0", false);
				ListValueToJson(builder, "z", vList, 2, "0", false);
				builder.Append("}");
			}
			else if (typeLow == "vector4")
			{
				var vList = pValue.Split('_');

				NameToJson(builder, pName);
				builder.Append("{");
				ListValueToJson(builder, "x", vList, 0, "0", true);
				ListValueToJson(builder, "y", vList, 1, "0", false);
				ListValueToJson(builder, "z", vList, 2, "0", false);
				ListValueToJson(builder, "w", vList, 3, "0", false);
				builder.Append("}");
			}
			else if (typeLow == "color")
			{
				var vList = pValue.Split('_');

				NameToJson(builder, pName);
				builder.Append("{");
				ListValueToJson(builder, "r", vList, 0, "0", true);
				ListValueToJson(builder, "g", vList, 1, "0", false);
				ListValueToJson(builder, "b", vList, 2, "0", false);
				ListValueToJson(builder, "w", vList, 3, "1", false);
				builder.Append("}");
			}
			else if (typeLow == "class")
			{
				var subIndex = FindInCol_0(sheet, Key_Sub);
				if (subIndex >= 0)
				{
					NameToJson(builder, pName);
					ClassToJson(builder, sheet, row, ref col, list[1], subIndex);
				}
				else
					Debug.LogError($"类{list[1]}未找到 {Key_Sub}");
			}
			else if (typeLow == "list")
			{
				NameToJson(builder, pName);
				builder.Append("[");

				if (list[1].ToLower() == "class")
				{
					var subIndex = FindInCol_0(sheet, Key_Sub);
					if (subIndex >= 0)
					{
						ListClassToJson(builder, sheet, row, ref col, list[2], subIndex);
					}
					else
						Debug.LogError($"类{list[2]}未找到 {Key_Sub}");
				}
				else
				{
					var subSplit = pType[pType.Length - 1];
					var valueList = pValue.Split(subSplit);
					var len = valueList.Length;

					if (len > 0)
					{
						var subType = pType.Substring(pType.IndexOf(Key_Split) + 1);

						PropertyToJson(builder, sheet, row, ref col, subType, string.Empty, valueList[0]);

						for (int i = 1; i < len; ++i)
						{
							builder.Append(",");
							PropertyToJson(builder, sheet, row, ref col, subType, string.Empty, valueList[i]);
						}
					}
				}

				builder.Append("]");
			}
		}

		private void NameToJson(StringBuilder builder, string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return;

			builder.Append($"\"{name}\":");
		}

		private void ValueToJson(StringBuilder builder, string name, string value)
		{
			if (string.IsNullOrWhiteSpace(name))
				builder.Append(value);
			else if (string.IsNullOrWhiteSpace(value))
				builder.Append($"\"{name}\":{0}");
			else
				builder.Append($"\"{name}\":{value}");
		}

		private void IntToJson(StringBuilder builder, string name, int value)
		{
			if (string.IsNullOrWhiteSpace(name))
				builder.Append(value);
			else
				builder.Append($"\"{name}\":{value}");
		}

		private void ListValueToJson(StringBuilder builder, string name, string[] vList, int index, string defaultValue, bool isFirst)
		{
			if (!isFirst)
				builder.Append(",");

			if (index < vList.Length)
				ValueToJson(builder, name, vList[index]);
			else
				ValueToJson(builder, name, defaultValue);
		}

		private void StringToJson(StringBuilder builder, string name, string value)
		{
			if (string.IsNullOrWhiteSpace(name))
				builder.Append($"\"{value}\"");
			else
				builder.Append($"\"{name}\":\"{value}\"");
		}

		private void BoolToJson(StringBuilder builder, string name, string value)
		{
			if (string.IsNullOrWhiteSpace(name))
				builder.Append(value.ToLower());
			else if (string.IsNullOrWhiteSpace(value))
				builder.Append($"\"{name}\":false");
			else
				builder.Append($"\"{name}\":{value.ToLower()}");
		}

		private void ClassToJson(StringBuilder builder, DataTable sheet, int row, ref int col, string classType, int subIndex)
		{
			builder.Append("{");

			var pName = sheet.Rows[subIndex][col].ToString();
			PropertyToJson(builder, sheet, row, ref col, GetSubClassPropertyType(classType, pName), pName);

			col += 1;
			var pType = sheet.Rows[0][col].ToString();
			var sType = sheet.Rows[subIndex][col].ToString();
			while (string.IsNullOrWhiteSpace(pType) && !string.IsNullOrWhiteSpace(sType))
			{
				builder.Append(",");
				pName = sheet.Rows[subIndex][col].ToString();
				PropertyToJson(builder, sheet, row, ref col, GetSubClassPropertyType(classType, pName), pName);

				col += 1;
				pType = sheet.Rows[0][col].ToString();
				sType = sheet.Rows[subIndex][col].ToString();
			}
			col -= 1;

			builder.Append("}");
		}

		private void ListClassToJson(StringBuilder builder, DataTable sheet, int row, ref int col, string classType, int subIndex)
		{
			builder.Append("{");

			var pName = sheet.Rows[subIndex][col].ToString();
			var pNList = pName.Split(Key_Split);
			pName = pNList[0];
			var liStr = pNList[1];
			PropertyToJson(builder, sheet, row, ref col, GetSubClassPropertyType(classType, pName), pName);

			col += 1;
			var pType = sheet.Rows[0][col].ToString();
			pName = sheet.Rows[subIndex][col].ToString();
			while (string.IsNullOrWhiteSpace(pType) && !string.IsNullOrWhiteSpace(pName))
			{
				pNList = pName.Split(Key_Split);
				pName = pNList[0];
				if (liStr == pNList[1])
				{
					builder.Append(",");
				}
				else
				{
					liStr = pNList[1];
					builder.Append("},{");
				}
				PropertyToJson(builder, sheet, row, ref col, GetSubClassPropertyType(classType, pName), pName);

				col += 1;
				pType = sheet.Rows[0][col].ToString();
				pName = sheet.Rows[subIndex][col].ToString();
			}
			col -= 1;

			builder.Append("}");
		}
		#endregion
	}
}