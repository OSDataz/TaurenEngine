/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/12/11 21:08:28
 *└────────────────────────┘*/

using System.Collections.Generic;
using Tauren.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace Tauren.Framework.Editor
{
	public sealed class SymbolsEditorData : EditorData<SymbolsData>
	{
		protected override string SavePath => $"Assets/Tauren Config/Project/SymbolsConfig.asset";

		protected override void UpdateProperty()
		{
			SymbolsList = GetProperty(SymbolsList, nameof(Data.symbolsList));
		}

		public List<string> GetSymbols()
		{
			var list = new List<string>();
			foreach (var item in Data.symbolsList)
			{
				if (item.selected)
					list.Add(item.value);
			}

			return list;
		}

		public PropertyList<SymbolsItemEo> SymbolsList { get; private set; }
	}

	public sealed class SymbolsItemEo : EditorObject
	{
		public override void SetData(SerializedProperty property)
		{
			base.SetData(property);

			Value = GetProperty(Value, "value");
			Selected = GetProperty(Selected, "selected");
			Description = GetProperty(Description, "description");
		}

		public override bool Draw()
		{
			EditorGUILayout.BeginHorizontal();

			Selected.Draw(GUILayout.Width(25));

			EditorGUILayout.LabelField("宏：", GUILayout.Width(20));
			Value.Draw();

			EditorGUILayout.LabelField("说明：", GUILayout.Width(30));
			Description.Draw();

			if (GUILayout.Button("Delete", GUILayout.Width(70)))
			{
				if (EditorUtility.DisplayDialog($"删除确认", $"\n删除 - {Description.Value}：{Value.Value}，\n\n确认是否删除？", "删除", "取消"))
					RemoveSelf();

				EditorGUILayout.EndHorizontal();
				return false;
			}
			EditorGUILayout.EndHorizontal();

			return true;
		}

		public override void Clear()
		{
			Value.Value = string.Empty;
			Selected.Value = false;
			Description.Value = string.Empty;
		}

		public PropertyString Value { get; private set; }
		public PropertyBool Selected { get; private set; }
		public PropertyString Description { get; private set; }
	}
}