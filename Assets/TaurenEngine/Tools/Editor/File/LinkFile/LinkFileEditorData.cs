/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2022/1/15 11:42:24
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEditor;
using UnityEngine;

namespace TaurenEditor.Tools
{
	public sealed class LinkFileEditorData : EditorData<LinkFileData>
	{
		protected override string SavePath => $"{EditorHelper.ConfigPath}/Editor/LinkFileConfig.asset";

		protected override void UpdateProperty()
		{
			Groups = GetProperty(Groups, nameof(Data.groups));
		}

		public PropertyList<LinkFileGroupEo> Groups { get; private set; }
	}

	public sealed class LinkFileGroupEo : EditorObject
	{
		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			ShowDetails = GetProperty(ShowDetails, "showDetails");
			Name = GetProperty(Name, "name");
			Groups = GetProperty(Groups, "groups");
		}

		public override bool Draw()
		{
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			ShowDetails.DrawFoldout(Name.Value);

			if (GUILayout.Button("Execute", GUILayout.Width(200)))
			{
				var bat = new BatScript();
				Groups.List.ForEach(item => item.LinkFile(bat));
				bat.Run();
				Debug.Log($"{Name.Value} 链接完成");
			}

			if (GUILayout.Button("Delete", GUILayout.Width(70)))
			{
				if (EditorUtility.DisplayDialog($"删除确认", $"\n当前删除 - {Name.Value}，\n\n确认是否删除？", "删除", "取消"))
					RemoveSelf();

				EditorGUILayout.EndHorizontal();
				return false;
			}
			EditorGUILayout.EndHorizontal();

			if (!ShowDetails.Value)
				return true;

			EditorGUILayout.BeginHorizontal();
			Name.Draw("Name：");

			if (GUILayout.Button("New Root", GUILayout.Width(70)))
			{
				Groups.Add();
			}
			EditorGUILayout.EndHorizontal();

			Groups.List.ForRemoveFunc(item => item.Draw());

			return true;
		}

		public override void Clear()
		{
			ShowDetails.Value = true;
			Name.Value = string.Empty;
			Groups.Clear();
		}

		public PropertyBool ShowDetails { get; private set; }
		public PropertyString Name { get; private set; }
		public PropertyList<LinkFileRootGroupEo> Groups { get; private set; }
	}

	public sealed class LinkFileRootGroupEo : EditorObject
	{
		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			RootFromPath = GetProperty(RootFromPath, "rootFromPath");
			RootToPath = GetProperty(RootToPath, "rootToPath");
			Items = GetProperty(Items, "items");
		}

		public override bool Draw()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Root From", GUILayout.Width(60));
			RootFromPath.Draw();

			EditorGUILayout.LabelField("To", GUILayout.Width(18));
			RootToPath.Draw();

			if (GUILayout.Button("New Path", GUILayout.Width(70)))
			{
				Items.Add();
			}

			if (GUILayout.Button("Delete", GUILayout.Width(70)))
			{
				RemoveSelf();

				EditorGUILayout.EndHorizontal();
				return false;
			}
			EditorGUILayout.EndHorizontal();

			Items.List.ForRemoveFunc(item => item.Draw());

			return true;
		}

		public override void Clear()
		{
			RootFromPath.Value = string.Empty;
			RootToPath.Value = string.Empty;
			Items.Clear();
		}

		public void LinkFile(BatScript bat)
		{
			var fromPath = PathUtils.FormatEnd(RootFromPath.Value, false);
			var toPath = PathUtils.FormatEnd(RootToPath.Value, false);

			Items.List.ForEach(item => item.LinkFile(bat, fromPath, toPath));
		}

		public PropertyString RootFromPath { get; private set; }
		public PropertyString RootToPath { get; private set; }
		public PropertyList<LinkFileItemEo> Items { get; private set; }
	}

	public sealed class LinkFileItemEo : EditorObject
	{
		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			FromPath = GetProperty(FromPath, "fromPath");
			ToPath = GetProperty(ToPath, "toPath");
		}

		public override bool Draw()
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(" - From", GUILayout.Width(45));
			FromPath.Draw();
			EditorGUILayout.LabelField("To", GUILayout.Width(18));
			ToPath.Draw();

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
			FromPath.Value = string.Empty;
			ToPath.Value = string.Empty;
		}

		public void LinkFile(BatScript bat, string rootFromPath, string rootToPath)
		{
			bat.MKLink($"{rootFromPath}\\{FromPath.Value}", $"{rootToPath}\\{ToPath.Value}");
		}

		public PropertyString FromPath { get; private set; }
		public PropertyString ToPath { get; private set; }
	}
}