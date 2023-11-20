/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/10/8 20:44:18
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;
using System;
using TaurenEngine.Editor;
using TaurenEngine.Core;

namespace TaurenEditor.Tools
{
	public sealed class CopyFileEditorData : EditorData<CopyFileData>
	{
		protected override string SavePath => $"{EditorHelper.ConfigPath}/Editor/CopyFileConfig.asset";

		protected override void UpdateProperty()
		{
			Groups = GetProperty(Groups, nameof(Data.groups));
		}

		public PropertyList<CopyFileGroupEo> Groups { get; private set; }
	}

	public sealed class CopyFileGroupEo : EditorObject
	{
		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			ShowDetails = GetProperty(ShowDetails, "showDetails");
			Name = GetProperty(Name, "name");
			UpdateTime = GetProperty(UpdateTime, "updateTime");
			Items = GetProperty(Items, "items");
		}

		public override bool Draw()
		{
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			ShowDetails.DrawFoldout(Name.Value);

			UpdateTime.DrawLabel();

			if (GUILayout.Button("Execute", GUILayout.Width(200)))
			{
				Items.List.ForEach(item => item.CopyFile());
				UpdateTime.Value = DateTime.Now.ToString();
				Debug.Log($"{Name.Value} 复制完成");
			}

			if (GUILayout.Button("Delete", GUILayout.Width(70)))
			{
				if (EditorUtility.DisplayDialog($"删除确认", $"\n删除 - {Name.Value}，\n\n确认是否删除？", "删除", "取消"))
					RemoveSelf();

				EditorGUILayout.EndHorizontal();
				return false;
			}
			EditorGUILayout.EndHorizontal();

			if (!ShowDetails.Value)
				return true;

			EditorGUILayout.BeginHorizontal();
			Name.Draw("Name：");

			if (GUILayout.Button("New Path", GUILayout.Width(70)))
			{
				Items.Add();
			}
			EditorGUILayout.EndHorizontal();

			Items.List.ForRemoveFunc(item => item.Draw());

			return true;
		}

		public override void Clear()
		{
			ShowDetails.Value = true;
			Name.Value = string.Empty;
			Items.Clear();
		}

		public PropertyBool ShowDetails { get; private set; }
		public PropertyString Name { get; private set; }
		public PropertyString UpdateTime { get; private set; }
		public PropertyList<CopyFileItemEo> Items { get; private set; }
	}

	public sealed class CopyFileItemEo : EditorObject
	{
		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			CopyPath = GetProperty(CopyPath, "copyPath");
			PastePath = GetProperty(PastePath, "pastePath");
		}

		public override bool Draw()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("From", GUILayout.Width(30));
			CopyPath.Draw();
			EditorGUILayout.LabelField("To", GUILayout.Width(18));
			PastePath.Draw();

			if (GUILayout.Button("Delete", GUILayout.Width(70)))
			{
				RemoveSelf();

				EditorGUILayout.EndHorizontal();
				return false;
			}
			EditorGUILayout.EndHorizontal();
			return true;
		}

		public override void Clear()
		{
			CopyPath.Value = string.Empty;
			PastePath.Value = string.Empty;
		}

		public void CopyFile()
		{
			FileUtils.Copy(CopyPath.Value, PastePath.Value);
		}

		public PropertyString CopyPath { get; private set; }
		public PropertyString PastePath { get; private set; }
	}
}