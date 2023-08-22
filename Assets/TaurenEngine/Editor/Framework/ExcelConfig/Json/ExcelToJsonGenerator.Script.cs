/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/9 20:55:57
 *└────────────────────────┘*/

using System.Data;
using System.Text;
using TaurenEngine.Runtime.Core;

namespace TaurenEngine.Editor
{
	/// <summary>
	/// 生成代码
	/// </summary>
	public partial class ExcelToJsonGenerator : ConfigGeneratorBase
	{
		#region 临时值
		protected StringBuilder builder1 = new StringBuilder();
		protected StringBuilder builder2 = new StringBuilder();
		protected StringBuilder builder3 = new StringBuilder();
		protected StringBuilder builder4 = new StringBuilder();
		#endregion

		public override void GenerateScript(
			string[] excelPaths, 
			string savePath, 
			string scriptNamespace, 
			Encoding encoding)
		{
			base.GenerateScript(excelPaths, savePath, scriptNamespace, encoding);

			var len = excelPaths.Length;
			for (int i = 0; i < len; ++i)
			{
				// 构造Excel工具类
				reader.Load(excelPaths[i]);

				// 判断Excel文件中是否存在数据表
				var tableLen = reader.DataSet.Tables.Count;
				if (tableLen < 1)
					continue;

				for (int j = 0; j < tableLen; ++j)// 遍历子表
				{
					var sheet = reader.DataSet.Tables[j];
					var sheetNameLow = sheet.TableName.ToLower();
					if (sheetNameLow == Key_Enum)
					{
						// 枚举表
						AddEnumTable(sheet);
					}
					else if (sheetNameLow == Key_Class)
					{
						// 类对象表
						AddSubClass(sheet);
					}
					else if (IsMapSheet(sheet))
					{
						// 全局表
						GenerateMapScript(sheet, savePath, scriptNamespace, encoding);
					}
					else
					{
						// 正常表
						GenerateConfigScript(sheet, savePath, scriptNamespace, encoding);
					}
				}
			}

			GenerateEnumScript(savePath, scriptNamespace, encoding);// 生成枚举代码
			GenerateSubClassScript(savePath, scriptNamespace, encoding);// 生成子类代码
			GeneratePrelaodScript(savePath, scriptNamespace, encoding);// 生成配置预加载表

			reader.Dispose();
		}

		#region 枚举表
		private void GenerateEnumScript(string savePath, string scriptNamespace, Encoding encoding)
		{
			if (enumMap == null)
				return;

			if (enumMap.Count == 0)
				return;

			builder1.Clear();

			foreach (var kv in enumMap)
			{
				builder2.Clear();

				var len = kv.Value.Count;
				for (int i = 0; i < len; ++i)
				{
					var field = kv.Value[i];
					if (!string.IsNullOrWhiteSpace(field.note))
					{
						builder2.Append($"\n		/// <summary> {FormatNote(field.note)} </summary>");
					}

					builder2.Append($"\n		{field.name} = {field.value},");
				}

				builder1.Append($"\n{string.Format(EnumTemplate, kv.Key, builder2.ToString())}");
			}

			var enumStr = string.Format(CodeTemplate.GetScriptTemplate(), scriptNamespace, builder1.ToString());

			FileUtils.SaveText($"{savePath}/{ToSheetScriptName("Enum")}.cs", enumStr, encoding);
		}

		private const string EnumTemplate = @"	public enum {0}
	{{{1}
	}}";
		#endregion

		#region 子类表
		private void GenerateSubClassScript(string savePath, string scriptNamespace, Encoding encoding)
		{
			if (subClassMap == null)
				return;

			if (subClassMap.Count == 0)
				return;

			builder1.Clear();

			foreach (var kv in subClassMap)
			{
				builder2.Clear();
				builder3.Clear();
				builder4.Clear();

				var len = kv.Value.Count;
				for (int i = 0; i < len; ++i)
				{
					var field = kv.Value[i];
					if (!string.IsNullOrWhiteSpace(field.note))
					{
						builder2.Append($"\n		/// <summary> {FormatNote(field.note)} </summary>");// 属性注释
					}

					var proType = FormatPropertyType(field.type, out var pType, out var subDataType);
					var proNameUpper = field.name.ToUpperFirst();

					builder2.Append($"\n		public {proType} {proNameUpper} {{ get; private set; }}");// 属性
					builder3.Append($"\n			public {proType} {field.name};");// Data字段

					if (pType == ConfigPropertyType.Class)
						builder4.Append($"\n				{proNameUpper}.SetData(data.{field.name});");
					else
						builder4.Append($"\n				{proNameUpper} = data.{field.name};");
				}

				builder1.Append($"\n{string.Format(ClassTemplate, kv.Key, builder2.ToString(), builder3.ToString(), builder4.ToString(), string.Empty)}");
			}

			var classStr = string.Format(CodeTemplate.GetScriptTemplate(), scriptNamespace, builder1.ToString());

			FileUtils.SaveText($"{savePath}/{ToSheetScriptName("Class")}.cs", classStr, encoding);
		}

		private const string ClassTemplate = @"	public class {0} : TaurenEngine.Runtime.Framework.IConfig
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
				TaurenEngine.Runtime.Framework.Log.Error($""配置表类型错误：{{jsonData.GetType()}}->{{typeof(Data)}}"");
		}}{4}
	}}";
		#endregion

		#region Map表
		private void GenerateMapScript(DataTable sheet, string savePath, string scriptNamespace, Encoding encoding)
		{
			builder1.Clear();
			builder2.Clear();
			builder3.Clear();

			var len = sheet.Rows.Count;
			for (int i = 2; i < len; ++i)
			{
				var note = sheet.Rows[i][0].ToString();
				if (note == Key_Note)
					continue;

				var typeStr = sheet.Rows[i][2].ToString();// 字段类型
				if (string.IsNullOrWhiteSpace(typeStr))
					continue;

				var nameStr = sheet.Rows[i][1].ToString();

				var proType = FormatPropertyType(typeStr, out var pType, out var subDataType);
				var proNameUpper = nameStr.ToUpperFirst();

				builder1.Append($"\n		/// <summary> {FormatNote(note)} </summary>\n		public {proType} {proNameUpper} {{ get; private set; }}");
				builder2.Append($"\n			public {proType} {nameStr};");
				if (pType == ConfigPropertyType.Class)
					builder3.Append($"\n				{proNameUpper}.SetData(data.{nameStr});");
				else
					builder3.Append($"\n				{proNameUpper} = data.{nameStr};");
			}

			var className = ToSheetScriptName(sheet.TableName);

			var classStr = string.Format(CodeTemplate.GetScriptTemplate(),
				scriptNamespace,
				"\n" + string.Format(ClassTemplate, className, builder1.ToString(), builder2.ToString(), builder3.ToString(), string.Empty)
				);

			AddPreloadConfig($"{savePath}/{sheet.TableName}.json", className, true);

			FileUtils.SaveText($"{savePath}/{className}.cs", classStr, encoding);
		}
		#endregion

		#region 正常表
		private void GenerateConfigScript(DataTable sheet, string savePath, string scriptNamespace, Encoding encoding)
		{
			builder1.Clear();
			builder2.Clear();
			builder3.Clear();
			builder4.Clear();

			var noteIndex = FindInCol_0(sheet, Key_Note);
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

				builder1.Append($"\n		/// <summary> {FormatNote(sheet.Rows[noteIndex][i].ToString())} </summary>\n		public {proType} {proNameUpper} {{ get; private set; }}");
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

			// 快速引用字段
			var className = ToSheetScriptName(sheet.TableName);

			var index = 0;
			len = sheet.Rows.Count;
			for (int i = 2; i < len; ++i)
			{
				var note = sheet.Rows[i][0].ToString();
				if (string.IsNullOrWhiteSpace(note) || note == Key_Note || note == Key_Sub)
					continue;

				if (note[0] == Key_Quick)
					builder4.Append($"\n		public static {className} {note.Split(Key_Split)[0].Substring(1)} => TaurenEngine.Runtime.Framework.InstanceManager.Instance.Get<TaurenEngine.Runtime.Framework.ConfigModel>().GetList<{className}>()[{index}];");

				index += 1;
			}

			var classStr = string.Format(CodeTemplate.GetScriptTemplate(),
				scriptNamespace,
				"\n" + string.Format(ClassTemplate, className, builder1.ToString(), builder2.ToString(), builder3.ToString(), builder4.ToString())
				);

			AddPreloadConfig($"{savePath}/{sheet.TableName}.json", className, false);

			FileUtils.SaveText($"{savePath}/{className}.cs", classStr, encoding);
		}
		#endregion

		#region 预加载表
		protected void GeneratePrelaodScript(string savePath, string scriptNamespace, Encoding encoding)
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
				scriptNamespace,
				"\n" + string.Format(PreloadTemplate, preloadList.Count, builder1.ToString())
				);

			FileUtils.SaveText($"{savePath}/ConfigPreload.cs", classStr, encoding);
		}

		private string PreloadTemplate = @"	public class ConfigPreload : TaurenEngine.Runtime.Framework.ConfigPreloadBase
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