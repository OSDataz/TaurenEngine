/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/23 20:31:33
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEditor.ModConfig
{
	public sealed class ConfigData : ScriptableObject
	{
		/// <summary>
		/// Excel文件路径
		/// </summary>
		public string excelPath;

		/// <summary>
		/// 生成代码命名空间
		/// </summary>
		public string scriptNamespace;
		/// <summary>
		/// 生成代码保存路径
		/// </summary>
		public string scriptSavePath;
		/// <summary>
		/// 生成配置保存路径
		/// </summary>
		public string configSavePath;

		/// <summary>
		/// 生成文件格式
		/// </summary>
		public ConfigFormatType formatType;
		/// <summary>
		/// 编码类型
		/// </summary>
		public ConfigEncodingType encodingType;
	}
}