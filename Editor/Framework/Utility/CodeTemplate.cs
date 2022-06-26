﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/10 10:48:38
 *└────────────────────────┘*/

using TaurenEngine.Framework;

namespace TaurenEngine.Editor.Framework
{
	/// <summary>
	/// 生成代码模板
	/// </summary>
	public static class CodeTemplate
	{
		public static string GetHeadAnnotation()
		{
			return $@"/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v{TaurenFramework.Version}
 *│
 *│  该文件由工具自动生成，切勿自行修改。
 *└────────────────────────┘*/";
		}

		public static string GetScriptTemplate() => GetHeadAnnotation() + @"

namespace {0}
{{{1}
}}";
	}
}