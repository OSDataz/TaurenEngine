/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.6.0
 *│　Time    ：2022/6/26 17:35:51
 *└────────────────────────┘*/

using System;
using System.Data;
using System.Text;
using TaurenEngine.Unity;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public class ConfigJsonHelper : ConfigHelperBase, IConfigHelper
	{
		private StringBuilder builder1 = new StringBuilder();
		private StringBuilder builder2 = new StringBuilder();
		private StringBuilder builder3 = new StringBuilder();
		private StringBuilder builder4 = new StringBuilder();

		#region 生成代码
		public void GenerateScript(ConfigData configData, string[] files, Encoding encoding)
		{
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
					var sheetNameLow = sheet.TableName.ToLower();
					if (sheetNameLow == "enum")
					{
						// 枚举表
						GenerateEnumScript(configData, sheet, encoding);
					}
					else if (sheetNameLow == "class")
					{
						// 类对象表
						GenerateClassScript(configData, sheet, encoding);
					}
					else if (sheet.Rows[0][0].ToString().ToLower() == "map")
					{
						// 全局表
						GenerateMapScript(configData, sheet, encoding);
					}
					else
					{
						// 正常表
						GenerateConfigScript(configData, sheet, encoding);
					}
				}
			}

			reader.Dispose();
		}

		/// <summary>
		/// 生成枚举类代码
		/// </summary>
		/// <param name="sheet"></param>
		/// <param name="encoding"></param>
		private void GenerateEnumScript(ConfigData configData, DataTable sheet, Encoding encoding)
		{
			SetEnum(sheet);

			var index = 1;
			var enumName = sheet.Rows[index][0].ToString();
			builder1.Clear();
			builder2.Clear();
			DataRow row;

			while (!string.IsNullOrWhiteSpace(enumName))
			{
				row = sheet.Rows[index];
				builder2.Append($"\n		{row[1]} = {GetEnumValue(enumName, row[1].ToString())},");

				index += 1;

				if (index >= sheet.Rows.Count)
				{
					// 一个枚举完成
					builder1.Append($"\n{string.Format(EnumTemplate, enumName, builder2.ToString())}");
					break;
				}

				row = sheet.Rows[index];
				if (string.IsNullOrWhiteSpace(row[0].ToString()))
				{
					if (string.IsNullOrWhiteSpace(row[1].ToString()))
					{
						// 一个枚举完成
						builder1.Append($"\n{string.Format(EnumTemplate, enumName, builder2.ToString())}");
						break;
					}
				}
				else
				{
					// 一个枚举完成
					builder1.Append($"\n{string.Format(EnumTemplate, enumName, builder2.ToString())}");

					// 处理新枚举
					builder2.Clear();
					enumName = row[0].ToString();
				}
			}

			var enumStr = string.Format(CodeTemplate.GetScriptTemplate(),
				configData.scriptNamespace,
				builder1.ToString()
				);

			FileEx.SaveText($"{configData.scriptSavePath}/{ToSheetScriptName("ClientEnum")}.cs", enumStr, encoding);
		}

		/// <summary>
		/// 生成数据子类代码
		/// </summary>
		/// <param name="sheet"></param>
		/// <param name="encoding"></param>
		private void GenerateClassScript(ConfigData configData, DataTable sheet, Encoding encoding)
		{
			SetSubClass(sheet);

			var index = 1;
			var className = sheet.Rows[index][0].ToString();
			builder1.Clear();
			builder2.Clear();
			builder3.Clear();
			builder4.Clear();
			DataRow row;

			while (!string.IsNullOrWhiteSpace(className))
			{
				row = sheet.Rows[index];
				var proType = FormatPropertyType(row[2].ToString(), out var pType, out var subDataType);
				var proNameUpper = row[1].ToString().ToUpperFirst();
				builder2.Append($"\n		/// <summary> {FormatNote(row[3].ToString())} </summary>\n		public {proType} {proNameUpper}  {{ get; private set; }}");
				builder3.Append($"\n			public {proType} {row[1]};");
				if (pType == ConfigPropertyType.Class)
					builder4.Append($"\n				{proNameUpper}.SetData(data.{row[1]});");
				else
					builder4.Append($"\n				{proNameUpper} = data.{row[1]};");

				index += 1;

				if (index >= sheet.Rows.Count)
				{
					// 一个子类完成
					builder1.Append($"\n{string.Format(ClassTemplate, className, builder2.ToString(), builder3.ToString(), builder4.ToString(), string.Empty)}");
					break;
				}

				row = sheet.Rows[index];
				if (string.IsNullOrWhiteSpace(row[0].ToString()))
				{
					if (string.IsNullOrWhiteSpace(row[1].ToString()))
					{
						// 一个子类完成
						builder1.Append($"\n{string.Format(ClassTemplate, className, builder2.ToString(), builder3.ToString(), builder4.ToString(), string.Empty)}");
						break;
					}
				}
				else
				{
					// 一个子类完成
					builder1.Append($"\n{string.Format(ClassTemplate, className, builder2.ToString(), builder3.ToString(), builder4.ToString(), string.Empty)}");

					// 处理新枚举
					builder2.Clear();
					builder3.Clear();
					builder4.Clear();
					className = row[0].ToString();
				}
			}

			var classStr = string.Format(CodeTemplate.GetScriptTemplate(),
				configData.scriptNamespace,
				builder1.ToString()
				);

			FileEx.SaveText($"{configData.scriptSavePath}/{ToSheetScriptName("ClientClass")}.cs", classStr, encoding);
		}

		/// <summary>
		/// 生成全局配置表
		/// </summary>
		/// <param name="sheet"></param>
		private void GenerateMapScript(ConfigData configData, DataTable sheet, Encoding encoding)
		{
			builder1.Clear();
			builder2.Clear();
			builder3.Clear();

			var len = sheet.Rows.Count;
			for (int i = 2; i < len; ++i)
			{
				var note = sheet.Rows[i][0].ToString();
				if (note == "##")
					continue;

				var typeStr = sheet.Rows[i][2].ToString();
				if (string.IsNullOrWhiteSpace(typeStr))
					continue;

				var nameStr = sheet.Rows[i][1].ToString();

				var proType = FormatPropertyType(typeStr, out var pType, out var subDataType);
				var proNameUpper = nameStr.ToUpperFirst();
				builder1.Append($"\n		/// <summary> {FormatNote(note)} </summary>\n		public {proType} {proNameUpper}  {{ get; private set; }}");
				builder2.Append($"\n			public {proType} {nameStr};");
				if (pType == ConfigPropertyType.Class)
					builder3.Append($"\n				{proNameUpper}.SetData(data.{nameStr});");
				else
					builder3.Append($"\n				{proNameUpper} = data.{nameStr};");
			}

			var className = ToSheetScriptName(sheet.TableName);

			var classStr = string.Format(CodeTemplate.GetScriptTemplate(),
				configData.scriptNamespace,
				"\n" + string.Format(ClassTemplate, className, builder1.ToString(), builder2.ToString(), builder3.ToString(), string.Empty)
				);

			AddPreloadConfig($"{configData.configSavePath}/{sheet.TableName}.json", className, true);

			FileEx.SaveText($"{configData.scriptSavePath}/{className}.cs", classStr, encoding);
		}

		/// <summary>
		/// 生成正常配置表
		/// </summary>
		/// <param name="sheet"></param>
		/// <param name="encoding"></param>
		private void GenerateConfigScript(ConfigData configData, DataTable sheet, Encoding encoding)
		{
			builder1.Clear();
			builder2.Clear();
			builder3.Clear();
			builder4.Clear();

			var noteIndex = FindInCol_0(sheet, "##");
			if (noteIndex == -1)
				noteIndex = 1;

			var len = sheet.Columns.Count;
			for (int i = 1; i < len; ++i)
			{
				var typeStr = sheet.Rows[0][i].ToString();
				if (string.IsNullOrWhiteSpace(typeStr))
					continue;

				var nameStr = sheet.Rows[1][i].ToString();

				var proType = FormatPropertyType(typeStr, out var pType, out var subDataType);
				var proNameUpper = nameStr.ToUpperFirst();
				builder1.Append($"\n		/// <summary> {FormatNote(sheet.Rows[noteIndex][i].ToString())} </summary>\n		public {proType} {proNameUpper}  {{ get; private set; }}");
				builder2.Append($"\n			public {subDataType} {nameStr};");
				if (pType == ConfigPropertyType.Class)
				{
					builder3.Append($@"
				{proNameUpper} = new {proType}();
				{proNameUpper}.SetData(data.{nameStr});");
				}
				else if (pType == ConfigPropertyType.ListClass)
				{
					builder3.Append($@"
				{proNameUpper} = new {proType}();
				var {nameStr}Len = data.{nameStr}.Count;
				for (var {nameStr}i = 0; {nameStr}i < {nameStr}Len; ++{nameStr}i)
				{{
					var item = new Item();
					item.SetData(data.{nameStr}[{nameStr}i]);
					{proNameUpper}.Add(item);
				}}");
				}
				else
				{
					builder3.Append($"\n				{proNameUpper} = data.{nameStr};");
				}
			}

			var className = ToSheetScriptName(sheet.TableName);

			var index = 0;
			len = sheet.Rows.Count;
			for (int i = 2; i < len; ++i)
			{
				var note = sheet.Rows[i][0].ToString();
				if (string.IsNullOrWhiteSpace(note) || note == "##" || note == "sub")
					continue;

				if (note[0] == '*')
					builder4.Append($"\n		public static {className} {note.Split('#')[0].Substring(1)} => Hotfix.Framework.ConfigHelper.GetList<{className}>()[{index}];");

				index += 1;
			}

			var classStr = string.Format(CodeTemplate.GetScriptTemplate(),
				configData.scriptNamespace,
				"\n" + string.Format(ClassTemplate, className, builder1.ToString(), builder2.ToString(), builder3.ToString(), builder4.ToString())
				);

			AddPreloadConfig($"{configData.configSavePath}/{sheet.TableName}.json", className, false);

			FileEx.SaveText($"{configData.scriptSavePath}/{className}.cs", classStr, encoding);
		}
		#endregion

		#region 生成仅客户端的配置
		public void GenerateConfig(ConfigData configData, string[] files, Encoding encoding)
		{
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
					var sheetNameLow = sheet.TableName.ToLower();
					if (sheetNameLow == "enum" || sheetNameLow == "class")
						continue;// 枚举和子类表无数据

					if (sheet.Rows[0][0].ToString().ToLower() == "map")
					{
						// 全局表
						GenerateMapConfig(configData, sheet, encoding);
					}
					else
					{
						// 正常表
						GenerateConfigConfig(configData, sheet, encoding);
					}
				}
			}

			reader.Dispose();
		}

		private void GenerateMapConfig(ConfigData configData, DataTable sheet, Encoding encoding)
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
				if (note == "##")
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

			FileEx.SaveText($"{configData.configSavePath}/{sheet.TableName}.json", builder1.ToString(), encoding);
		}

		private void GenerateConfigConfig(ConfigData configData, DataTable sheet, Encoding encoding)
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
				if (note == "sub" || note == "##")
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

			FileEx.SaveText($"{configData.configSavePath}/{sheet.TableName}.json", builder1.ToString(), encoding);
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
			var list = pType.Split('#');
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
				var subIndex = FindInCol_0(sheet, "sub");
				if (subIndex >= 0)
				{
					NameToJson(builder, pName);
					ClassToJson(builder, sheet, row, ref col, list[1], subIndex);
				}
				else
					Debug.LogError($"类{list[1]}未找到 sub");
			}
			else if (typeLow == "list")
			{
				NameToJson(builder, pName);
				builder.Append("[");

				if (list[1].ToLower() == "class")
				{
					var subIndex = FindInCol_0(sheet, "sub");
					if (subIndex >= 0)
					{
						ListClassToJson(builder, sheet, row, ref col, list[2], subIndex);
					}
					else
						Debug.LogError($"类{list[2]}未找到 sub");
				}
				else
				{
					var subSplit = pType[pType.Length - 1];
					var valueList = pValue.Split(subSplit);
					var len = valueList.Length;

					if (len > 0)
					{
						var subType = pType.Substring(pType.IndexOf("#") + 1);

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
			var pNList = pName.Split('#');
			pName = pNList[0];
			var liStr = pNList[1];
			PropertyToJson(builder, sheet, row, ref col, GetSubClassPropertyType(classType, pName), pName);

			col += 1;
			var pType = sheet.Rows[0][col].ToString();
			pName = sheet.Rows[subIndex][col].ToString();
			while (string.IsNullOrWhiteSpace(pType) && !string.IsNullOrWhiteSpace(pName))
			{
				pNList = pName.Split('#');
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

		#region 生成预加载表
		public void GeneratePrelaodScript(ConfigData configData, Encoding encoding)
		{
			builder1.Clear();

			string parseFuncStr;
			foreach (var data in preloadList)
			{
				if (data.isMap) parseFuncStr = "ParseMapConfig";
				else parseFuncStr = "ParseConfig";

				builder1.Append($"\n			Load(\"{data.path}\", json => {{ {parseFuncStr}<{data.className}, {data.className}.Data>(json); CheckLoadComplete(); }});");
			}

			var classStr = string.Format(CodeTemplate.GetScriptTemplate(),
				configData.scriptNamespace,
				"\n" + string.Format(PreloadTemplate, preloadList.Count, builder1.ToString())
				);

			FileEx.SaveText($"{configData.scriptSavePath}/ConfigPreload.cs", classStr, encoding);
		}
		#endregion

		#region 代码模板
		private string EnumTemplate = @"	public enum {0}
	{{{1}
	}}";

		private string ClassTemplate = @"	public class {0} : Hotfix.Framework.IConfig
	{{{1}
		
		public class Data
		{{{2}
		}}

		public void SetData(object jsonData)
		{{
			if (jsonData is Data data)
			{{{3}
			}}
			else
				TaurenEngine.Framework.Debugger.Error($""配置表类型错误：{{jsonData.GetType()}}->{{typeof(Data)}}"");
		}}{4}
	}}";

		private string PreloadTemplate = @"	public class ConfigPreload : Hotfix.Framework.ConfigPreloadBase
	{{
		public void Preload(System.Action onAllLoadComplete)
		{{
			this.onAllLoadComplete = onAllLoadComplete;

			configCount = {0};
			{1}
		}}
	}}";
		#endregion
	}
}