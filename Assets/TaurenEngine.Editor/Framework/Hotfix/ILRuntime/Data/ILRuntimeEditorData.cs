/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/6 14:59:37
 *└────────────────────────┘*/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public sealed class ILRuntimeEditorData : EditorData<ILRuntimeData>
	{
		protected override string SavePath => "Assets/SettingConfig/Project/ILRuntimeConfig.asset";

		protected override void UpdateProperty()
		{
			GenerateCLRSavePath = GetProperty(GenerateCLRSavePath, nameof(Data.generateCLRSavePath));
			AdaptorGroupList = GetProperty(AdaptorGroupList, nameof(Data.adaptorGroupList));
		}

		public PropertyString GenerateCLRSavePath { get; private set; }
		public PropertyList<ILRuntimeAdaptorGroupEo> AdaptorGroupList { get; private set; }
	}

	public sealed class ILRuntimeAdaptorGroupEo : EditorObject
	{
		private ILRuntimeAdaptorDrawer _adaptorDrawer;

		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			ShowDetails = GetProperty(ShowDetails, "showDetails");
			Name = GetProperty(Name, "name");
			AssemblyName = GetProperty(AssemblyName, "assemblyName");
			GenerateNamespace = GetProperty(GenerateNamespace, "generateNamespace");
			AdaptorSavePath = GetProperty(AdaptorSavePath, "adaptorSavePath");
			AdaptorRegisterCodePath = GetProperty(AdaptorRegisterCodePath, "adaptorRegisterCodePath");
			AdaptorList = GetProperty(AdaptorList, "adaptorList");

			_adaptorDrawer ??= new ILRuntimeAdaptorDrawer();
			var reList = AdaptorList.InitReorderableList();
			reList.drawHeaderCallback = OnDrawHeader;
			reList.drawElementCallback = OnDrawElement;
		}

		public void UpdateReorderableList()
		{
			var reList = AdaptorList.InitReorderableList(true);
			reList.drawHeaderCallback = OnDrawHeader;
			reList.drawElementCallback = OnDrawElement;
		}

		public override bool Draw()
		{
			EditorGUILayout.BeginHorizontal();
			ShowDetails.DrawFoldout(Name.Value);

			if (GUILayout.Button("生成适配器", GUILayout.Width(120)))
			{
				ILRuntimeHelper.GenerateCrossbindAdapter(this, true);
			}

			if (GUILayout.Button("删除组", GUILayout.Width(70)))
			{
				if (EditorUtility.DisplayDialog($"删除确认", $"\n当前删除 - {Name.Value}，\n\n确认是否删除？", "删除", "取消"))
				{
					RemoveSelf();

					// 删除后，列表SerializedProperty需要重新更新
					(Parent as PropertyList<ILRuntimeAdaptorGroupEo>)?.List.ForEach(item => item.UpdateReorderableList());
				}

				EditorGUILayout.EndHorizontal();
				return false;
			}
			EditorGUILayout.EndHorizontal();

			if (!ShowDetails.Value)
				return true;

			Name.Draw("组命名：");
			AdaptorSavePath.Draw("生成适配器保存地址：");
			AdaptorRegisterCodePath.Draw("生成适配器绑定文件地址：");
			GenerateNamespace.Draw("生成适配器命名空间：");
			AssemblyName.Draw("非默认程序集名称（无后缀）：");

			AdaptorList.ReorderableList.DoLayoutList();

			return true;
		}

		private void OnDrawHeader(Rect rect)
		{
			EditorGUI.LabelField(new Rect(rect) { width = 100 }, "是否生成");
			EditorGUI.LabelField(new Rect(rect) { width = 100, x = 95 }, "适配文件名");
			EditorGUI.LabelField(new Rect(rect) { width = 150, x = 255 }, "全类名");
		}

		private void OnDrawElement(Rect rect, int index, bool isActive, bool isFocused)
		{
			_adaptorDrawer.SetData(AdaptorList.GetItem(index));
			_adaptorDrawer.Draw(rect);
		}

		public override void Clear()
		{
			ShowDetails.Value = true;
			Name.Value = string.Empty;
			AssemblyName.Value = string.Empty;
			GenerateNamespace.Value = string.Empty;
			AdaptorSavePath.Value = string.Empty;
			AdaptorRegisterCodePath.Value = string.Empty;
			AdaptorList.Clear();
		}

		public PropertyBool ShowDetails { get; private set; }
		public PropertyString Name { get; private set; }
		public PropertyString AssemblyName { get; private set; }
		public PropertyString GenerateNamespace { get; private set; }
		public PropertyString AdaptorSavePath { get; private set; }
		public PropertyString AdaptorRegisterCodePath { get; private set; }
		public PropertyList AdaptorList { get; private set; }

		public string AssemblyNameWithoutExtension
			=> Path.GetFileNameWithoutExtension(AssemblyName.Value);
	}

	public sealed class ILRuntimeAdaptorDrawer : EditorDrawer
	{
		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			Selected = GetProperty(Selected, "selected");
			FileName = GetProperty(FileName, "fileName");
			FullName = GetProperty(FullName, "fullName");
		}

		public override void Draw(Rect rect)
		{
			Selected.Draw(new Rect(rect) { x = 42 });
			FileName.Draw(new Rect(rect) { width = 100, height = 21, x = 75, y = rect.y + 1 });
			FullName.Draw(new Rect(rect) { width = 200, height = 21, x = 185, y = rect.y + 1 });
		}

		public PropertyBool Selected { get; private set; }
		public PropertyString FileName { get; private set; }
		public PropertyString FullName { get; private set; }
	}
}