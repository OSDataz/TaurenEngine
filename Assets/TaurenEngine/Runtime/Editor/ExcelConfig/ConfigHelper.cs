/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/16 21:00:01
 *└────────────────────────┘*/

using System.Text;

namespace TaurenEditor.Runtime
{
	public static class ConfigHelper
	{
		public static void GenerateConfig(ConfigData data, string[] excelList)
		{
			var generator = GetConfigGenerator(data.formatType);
			var encoding = ToEncoding(data.encodingType);

			generator.GenerateScript(excelList, data.scriptSavePath, data.scriptNamespace, encoding);
			generator.GenerateFile(excelList, data.configSavePath, encoding);
		}

		private static ConfigGeneratorBase GetConfigGenerator(ConfigFormatType formatType)
		{
			if (formatType == ConfigFormatType.JSON) return new ExcelToJsonGenerator();
			return new ExcelToJsonGenerator();
		}

		private static Encoding ToEncoding(ConfigEncodingType encodingType)
		{
			if (encodingType == ConfigEncodingType.UTF8) return new UTF8Encoding(false);// return Encoding.UTF8; 改为不带bom的utf8
			if (encodingType == ConfigEncodingType.GB2312) return Encoding.GetEncoding("gb2312");
			return Encoding.Default;
		}
	}
}