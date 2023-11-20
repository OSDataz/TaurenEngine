/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/16 21:06:41
 *└────────────────────────┘*/

using System.Text;

namespace TaurenEditor.ModConfig
{
	/// <summary>
	/// 生成资源文件
	/// </summary>
	public abstract partial class ConfigGeneratorBase
	{
		/// <summary>
		/// 生成配置文件
		/// </summary>
		/// <param name="excelPaths"></param>
		/// <param name="savePath"></param>
		/// <param name="encoding"></param>
		public abstract void GenerateFile(string[] excelPaths, string savePath, Encoding encoding);
	}
}