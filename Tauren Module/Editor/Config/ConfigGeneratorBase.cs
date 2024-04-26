/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/9 20:47:23
 *└────────────────────────┘*/

using System.Data;

namespace Tauren.Module.Editor
{
	/// <summary>
	/// 配置表生成工具基类
	/// </summary>
	public abstract partial class ConfigGeneratorBase
	{
		/// <summary> 枚举表 </summary>
		protected const string Key_Enum = "enum";
		/// <summary> 子类表 </summary>
		protected const string Key_Class = "class";
		/// <summary> 注释关键词 </summary>
		protected const string Key_Note = "##";
		/// <summary> 子类型相关配置 </summary>
		protected const string Key_Sub = "sub";
		/// <summary> 间隔符 </summary>
		protected const char Key_Split = '#';
		/// <summary> 全局表快捷属性标记 </summary>
		protected const char Key_Quick = '*';

		protected ExcelReader reader = new ExcelReader();

		/// <summary>
		/// 是否是Map表
		/// </summary>
		/// <param name="sheet"></param>
		/// <returns></returns>
		protected bool IsMapSheet(DataTable sheet)
		{
			return sheet.Rows[0][0].ToString().ToLower() == "map";
		}
	}
}