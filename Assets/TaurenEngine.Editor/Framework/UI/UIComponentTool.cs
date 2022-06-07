/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/2 15:18:22
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TaurenEngine.Framework;
using TaurenEngine.Unity;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TaurenEngine.Editor.Framework
{
	public static class UIComponentTool
	{
		[MenuItem("Assets/TaurenFramework/生成UI代码", true)]
		private static bool GenerateUICodeValidate()
		{
			if (Selection.activeGameObject == null)
				return false;

			if (!PrefabUtility.IsPartOfPrefabAsset(Selection.activeGameObject))
				return false;

			var fullPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(Selection.activeGameObject);
			if (fullPath.IndexOf(UIComponentEditorData.Instance.Data.uiPrefabPath) != 0)
				return false;

			return true;
		}

		[MenuItem("Assets/TaurenFramework/生成UI代码", false, 500)]
		private static void GenerateUICode()
		{
			var uiData = UIComponentEditorData.Instance.Data;
			if (string.IsNullOrWhiteSpace(uiData.generateSavePath))
			{
				Debug.LogError("未设置生成UI代码保存路径，请在UIComponent中设置。");
				return;
			}

			foreach (var gameObject in Selection.gameObjects)
			{
				if (!PrefabUtility.IsPartOfPrefabAsset(gameObject))
					continue;

				var fullPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
				if (fullPath.IndexOf(UIComponentEditorData.Instance.Data.uiPrefabPath) != 0)
					continue;

				GenerateUICode(uiData, fullPath, gameObject);
			}

			AssetDatabase.Refresh();
		}

		private static void GenerateUICode(UIComponentData uiEditorData, string path, GameObject gameObject)
		{
			string template;
			string className;

			var setting = gameObject.GetComponent<UIPanelSetting>();
			if (setting != null)
			{
				template = PanelTemplate;
				className = FormatUIClassName(gameObject.name, "Panel");
			}
			else
			{
				template = UITemplate;
				className = FormatUIClassName(gameObject.name, "UI");
			}

			path = Path.GetDirectoryName(path.Substring(uiEditorData.uiPrefabPath.Length));// 子文件路径
			path = PathEx.FormatPathEnd(uiEditorData.generateSavePath, true) + PathEx.FormatPathStart(path, false);// Asset下完整文件路径

			string headPath;
			if (string.IsNullOrEmpty(uiEditorData.codeNamespace))
			{
				headPath = PathEx.FormatPathEnd(path, false);
				headPath = headPath.Replace(" ", "");
				headPath = headPath.Replace('/', '.');
				headPath = headPath.Replace('\\', '.');
				headPath = headPath.Substring(7);
			}
			else
			{
				headPath = uiEditorData.codeNamespace;
			}

			path = $"{EditorHelper.ProjectPath}{path}/{className}.cs";// 全代码文件路径

			string propertyGet = "";
			string propertySet = "";
			ParseSubObject(gameObject.transform, "", ref propertyGet, ref propertySet);

			// 生成代码文件
			FileEx.SaveText(path, string.Format(
				template,
				TaurenFramework.Version,
				DateTime.Now.ToString(),
				headPath,
				className,
				propertyGet,
				propertySet
				));
		}

		private static string FormatUIClassName(string uiName, string lastName)
		{
			var nameLen = uiName.Length;
			var lastLen = lastName.Length;
			if (nameLen <= lastLen)
				return $"{uiName}{lastName}";

			var lastStr = uiName.Substring(nameLen - lastLen, lastLen);
			if (lastStr.ToLower() == lastName.ToLower())
				return $"{uiName.Substring(0, nameLen - lastLen)}{lastName}";
			else
				return $"{uiName}{lastName}";
		}

		private static void ParseSubObject(Transform transform, string rootPath, ref string propertyGet, ref string propertySet)
		{
			var len = transform.childCount;
			for (int i = 0; i < len; ++i)
			{
				var child = transform.GetChild(i);
				switch (child.name[0])
				{
					case '*':// 生成组件，并解析子组件
						ParseComponent(child, rootPath, ref propertyGet, ref propertySet);

						
						ParseSubObject(child, string.IsNullOrEmpty(rootPath) ? child.name : $"{rootPath}/{child.name}", ref propertyGet, ref propertySet);
						break;

					case '+':// 仅生成组件
						ParseComponent(child, rootPath, ref propertyGet, ref propertySet);
						break;

					case '=':// 跳过该节点
						break;

					default:
						ParseSubObject(child, string.IsNullOrEmpty(rootPath) ? child.name : $"{rootPath}/{child.name}", ref propertyGet, ref propertySet);
						break;
				}
			}
		}

		private static void ParseComponent(Transform transform, string rootPath, ref string propertyGet, ref string propertySet)
		{
			string typeStr;
			string nameStr;

			var name = transform.name.Replace(" ", "");

			if (name.Contains("-"))
			{
				// 自定义类型
				var lastIndex = name.LastIndexOf('-');
				typeStr = name.Substring(lastIndex + 1);
				nameStr = name.Substring(0, lastIndex);

				var type = Array.Find(DefaultUIComponentType, item => item.Name == typeStr);
				if (type != null)
					typeStr = type.FullName;
			}
			else
			{
				// 默认类型
				var list = new List<Component>();
				transform.GetComponents(list);

				Component comp = null;
				int value = -1;
				foreach (var item in list)
				{
					var index = Array.IndexOf(DefaultUIComponentType, item.GetType());
					if (value == -1 || (index != -1 && index < value))
					{
						comp = item;
						value = index;
					}
				}

				typeStr = comp.GetType().FullName;
				nameStr = name;
			}

			nameStr = FormatPropertyName(nameStr);

			propertyGet += $"\r\n		public {typeStr} {nameStr} {{ get; private set; }}";
			propertySet += $"\r\n			{nameStr} = root.Find(\"{rootPath}/{transform.name}\")?.GetComponent<{typeStr}>();";
		}

		private static string FormatPropertyName(string name)
		{
			// 去掉首字符标识符
			name = name.Substring(1);
			// 去掉非法字符
			name = Regex.Replace(name, "[^\\w]", "_");
			// 将连续多个下划线替换为一个
			name = Regex.Replace(name, "[_]+", "_");
			// 去掉首位下划线
			name = name.Trim('_');
			// 若首位是数字则加下划线
			if (Regex.IsMatch(name, "^\\d")) 
				name = "_" + name;

			return name;
		}

		private static Type[] DefaultUIComponentType = new Type[]
		{
			typeof(ButtonEx),
			typeof(Button),
			typeof(ToggleEx),
			typeof(Toggle),
			typeof(InputFieldEx),
			typeof(InputField),
			typeof(ScrollRect),
			typeof(Slider),

			typeof(TextEx),
			typeof(Text),
			typeof(TextMeshPro),
			typeof(ImageEx),
			typeof(Image),
			typeof(RawImageEx),
			typeof(RawImage),

			typeof(RectTransform),
			typeof(Transform),
		};

		private static string PanelTemplate = @"/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v{0}
 *│　Time    ：{1}
 *│
 *│  工具自动生成，切勿自行修改。
 *└────────────────────────┘*/

using TaurenEngine.Framework;
using UnityEngine;

namespace {2}
{{
	public class {3} : UIPanel
	{{{4}

		public override void Init(Transform root)
		{{
			base.Init(root);
			{5}
		}}
	}}
}}";

		private static string UITemplate = @"/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v{0}
 *│　Time    ：{1}
 *│
 *│  工具自动生成，切勿自行修改。
 *└────────────────────────┘*/

using TaurenEngine.Framework;
using UnityEngine;

namespace {2}
{{
	public class {3} : UIBase
	{{{4}

		public override void Init(Transform root)
		{{
			base.Init(root);
			{5}
		}}
	}}
}}";
	}
}