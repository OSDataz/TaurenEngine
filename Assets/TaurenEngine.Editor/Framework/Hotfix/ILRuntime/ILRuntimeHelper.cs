/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/5 18:06:53
 *└────────────────────────┘*/

using System;
using System.IO;
using System.Reflection;
using System.Text;
using TaurenEngine.Core;
using TaurenEngine.Framework;
using TaurenEngine.Unity;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public static class ILRuntimeHelper
	{
		[MenuItem("TaurenEngine/ILRuntime/打开ILRuntime中文文档")]
		private static void OpenDocumentation()
		{
			Application.OpenURL("https://ourpalm.github.io/ILRuntime/");
		}

		[MenuItem("TaurenEngine/ILRuntime/打开ILRuntime视频教程")]
		private static void OpenTutorial()
		{
			Application.OpenURL("https://learn.u3d.cn/tutorial/ilruntime");
		}

		#region 通过自动分析热更DLL生成CLR绑定
		public static void GenerateCLRBindingByAnalysis(ILRuntimeData data)
		{
			var dlls = HotfixEditorData.Instance.Data.dlls;
			if (dlls.Count == 0)
				return;

			foreach (var dll in dlls)
			{
				// 用新的分析热更dll调用引用来生成绑定代码
				ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();
				using (FileStream fs = new FileStream($"{EditorPath.ProjectPath}/Library/ScriptAssemblies/{dll.name}", FileMode.Open, FileAccess.Read))
				{
					domain.LoadAssembly(fs);

					new ILRuntimeRegister().InitAdaptor(domain);

					ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, data.generateCLRSavePath);
				}
			}

			AssetDatabase.Refresh();
		}
		#endregion

		#region 生成跨域继承适配器
		public static void GenerateCrossbindAdapter(PropertyList<ILRuntimeAdaptorGroupEo> list)
		{
			foreach (var group in list.List)
			{
				GenerateCrossbindAdapter(group, false);
			}

			AssetDatabase.Refresh();
		}

		public static void GenerateCrossbindAdapter(ILRuntimeAdaptorGroupEo groupEo, bool refresh)
		{
			//由于跨域继承特殊性太多，自动生成无法实现完全无副作用生成，所以这里提供的代码自动生成主要是给大家生成个初始模版，简化大家的工作
			//大多数情况直接使用自动生成的模版即可，如果遇到问题可以手动去修改生成后的文件，因此这里需要大家自行处理是否覆盖的问题

			var saveRootPath = PathEx.FormatPathEnd(groupEo.AdaptorSavePath.Value, true);

			Assembly assembly;
			string assemblyName = groupEo.AssemblyNameWithoutExtension;
			if (!string.IsNullOrEmpty(assemblyName))
				assembly = Assembly.Load(assemblyName);
			else
				assembly = null;

			if (assembly == null)
			{
				Debug.LogError($"未找到指定程序集，Name：{assemblyName}");
				return;
			}

			var item = new ILRuntimeAdaptorDrawer();
			var len = groupEo.AdaptorList.Length;
			for (int i = 0; i < len; ++i)
			{
				item.SetData(groupEo.AdaptorList.GetItem(i));

				if (!item.Selected.Value)
					continue;

				Type type;
				if (string.IsNullOrEmpty(assemblyName))
					type = Type.GetType(item.FullName.Value);
				else
					type = assembly.GetType(item.FullName.Value);

				if (type == null)
				{
					Debug.LogWarning($"未找到类型，Assembly：{assemblyName} FullName：{item.FullName.Value}");
					continue;
				}

				using (StreamWriter sw = new StreamWriter($"{saveRootPath}{item.FileName.Value}Adapter.cs"))
				{
					sw.WriteLine(ILRuntime.Runtime.Enviorment.CrossBindingCodeGenerator.GenerateCrossBindingAdapterCode(type, groupEo.GenerateNamespace.Value));
				}
			}

			// 生成适配器绑定文件
			if (!string.IsNullOrEmpty(groupEo.AdaptorRegisterCodePath.Value))
				GenerateAdpatorRegisterCode(groupEo);

			Debug.Log($"{groupEo.Name.Value} 生成跨域继承适配器完成");

			if (refresh)
				AssetDatabase.Refresh();
		}

		private static void GenerateAdpatorRegisterCode(ILRuntimeAdaptorGroupEo groupEo)
		{
			StringBuilder bindStr = new StringBuilder();
			var files = Directory.GetFiles($"{EditorPath.ProjectPath}/{groupEo.AdaptorSavePath.Value}", "*.cs");
			foreach (var file in files)
			{
				bindStr.Append($"\r\n			domain.RegisterCrossBindingAdaptor(new {Path.GetFileNameWithoutExtension(file)}());");
			}

			var className = Path.GetFileNameWithoutExtension(groupEo.AdaptorRegisterCodePath.Value).SubstringBefore(".");

			FileEx.SaveText(groupEo.AdaptorRegisterCodePath.Value, string.Format(
				adpatorRegisterTemplate,
				CodeTemplate.GetHeadAnnotation(),
				groupEo.GenerateNamespace.Value,
				className,
				bindStr.ToString()
				));
		}

		private static string adpatorRegisterTemplate = @"{0}

namespace {1}
{{
	public partial class {2}
	{{
		/// <summary>
		/// 热更继承本地基类适配
		/// </summary>
		public void InitAdaptor(ILRuntime.Runtime.Enviorment.AppDomain domain)
		{{{3}
		}}
	}}
}}";
		#endregion
	}
}