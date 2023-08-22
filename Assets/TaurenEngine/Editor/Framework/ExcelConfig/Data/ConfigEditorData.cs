/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/23 20:33:35
 *└────────────────────────┘*/

namespace TaurenEngine.Editor
{
	public class ConfigEditorData : EditorData<ConfigData>
	{
		protected override string SavePath => "Assets/TaurenConfig/Project/ExcelConfig.asset";

		protected override void UpdateProperty()
		{
			ExcelPath = GetProperty(ExcelPath, nameof(Data.excelPath));
			ScriptNamespace = GetProperty(ScriptNamespace, nameof(Data.scriptNamespace));
			ScriptSavePath = GetProperty(ScriptSavePath, nameof(Data.scriptSavePath));
			ConfigSavePath = GetProperty(ConfigSavePath, nameof(Data.configSavePath));
			FormatType = GetProperty(FormatType, nameof(Data.formatType));
			EncodingType = GetProperty(EncodingType, nameof(Data.encodingType));
		}

		public PropertyString ExcelPath { get; private set; }
		public PropertyString ScriptNamespace { get; private set; }
		public PropertyString ScriptSavePath { get; private set; }
		public PropertyString ConfigSavePath { get; private set; }

		public PropertyEnum<ConfigFormatType> FormatType { get; private set; }
		public PropertyEnum<ConfigEncodingType> EncodingType { get; private set; }


	}
}