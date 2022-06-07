/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/6 14:59:37
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public sealed class ILRuntimeEditorData : EditorData<ILRuntimeData>
	{
		protected override string SavePath => "Assets/FrameworkConfig/ILRuntimeConfig.asset";

		protected override void UpdateProperty()
		{
			DLLPath = GetProperty(DLLPath, nameof(Data.dllPath));
			GenerateCodeSavePath = GetProperty(GenerateCodeSavePath, nameof(Data.generateCodeSavePath));
			AdaptorGroupList = GetProperty(AdaptorGroupList, nameof(Data.adaptorGroupList));
		}

		public PropertyString DLLPath { get; private set; }
		public PropertyString GenerateCodeSavePath { get; private set; }
		public PropertyList<ILRuntimeAdaptorGroupEo> AdaptorGroupList { get; private set; }
	}

	public sealed class ILRuntimeAdaptorGroupEo : EditorObject
	{
		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			ShowDetails = GetProperty(ShowDetails, "showDetails");
			Name = GetProperty(Name, "name");
			AssemblyName = GetProperty(AssemblyName, "assemblyName");
			GenerateNamespace = GetProperty(GenerateNamespace, "generateNamespace");
			AdaptorSavePath = GetProperty(AdaptorSavePath, "adaptorSavePath");
			AdaptorList = GetProperty(AdaptorList, "adaptorList");
		}

		public override bool Draw()
		{
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			ShowDetails.DrawFoldout(Name.Value);

			if (GUILayout.Button("生成适配器", GUILayout.Width(200)))
			{
				ILRuntimeHelper.GenerateCrossbindAdapter(this);

				Debug.Log($"{Name.Value} 生成跨域继承适配器完成");
			}

			if (GUILayout.Button("Delete", GUILayout.Width(70)))
			{
				if (EditorUtility.DisplayDialog($"删除确认", $"\n当前删除 - {Name.Value}，\n\n确认是否删除？", "删除", "取消"))
				{
					Clear();
					RemoveSelf();
				}
				return false;
			}
			EditorGUILayout.EndHorizontal();

			if (!ShowDetails.Value)
				return true;

			Name.Draw("组命名：");
			AdaptorSavePath.Draw("生成适配器保存地址：");
			GenerateNamespace.Draw("生成适配器命名空间：");

			EditorGUILayout.BeginHorizontal();
			AssemblyName.Draw("非当前程序集名称：");

			if (GUILayout.Button("New", GUILayout.Width(70)))
			{
				AdaptorList.Add();
			}
			EditorGUILayout.EndHorizontal();

			AdaptorList.List.ForFunc(item => item.Draw());

			return true;
		}

		public override void Clear()
		{
			ShowDetails.Value = true;
			Name.Value = string.Empty;
			AssemblyName.Value = string.Empty;
			GenerateNamespace.Value = string.Empty;
			AdaptorSavePath.Value = string.Empty;
			AdaptorList.Clear();
		}

		public PropertyBool ShowDetails { get; private set; }
		public PropertyString Name { get; private set; }
		public PropertyString AssemblyName { get; private set; }
		public PropertyString GenerateNamespace { get; private set; }
		public PropertyString AdaptorSavePath { get; private set; }
		public PropertyList<ILRuntimeAdaptorItemEo> AdaptorList { get; private set; }
	}

	public sealed class ILRuntimeAdaptorItemEo : EditorObject
	{
		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			Selected = GetProperty(Selected, "selected");
			FileName = GetProperty(FileName, "fileName");
			FullName = GetProperty(FullName, "fullName");
		}

		public override bool Draw()
		{
			EditorGUILayout.BeginHorizontal();

			Selected.Draw(GUILayout.Width(20));
			EditorGUILayout.LabelField("文件名：", GUILayout.Width(50));
			FileName.Draw();
			EditorGUILayout.LabelField("全类名：", GUILayout.Width(50));
			FullName.Draw();

			if (GUILayout.Button("Delete", GUILayout.Width(70)))
			{
				RemoveSelf();
				return false;
			}
			EditorGUILayout.EndHorizontal();

			return true;
		}

		public override void Clear()
		{
			Selected.Value = true;
			FileName.Value = string.Empty;
			FullName.Value = string.Empty;
		}

		public PropertyBool Selected { get; private set; }
		public PropertyString FileName { get; private set; }
		public PropertyString FullName { get; private set; }
	}
}