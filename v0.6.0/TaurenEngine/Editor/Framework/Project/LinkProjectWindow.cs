/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.6.1
 *│　Time    ：2022/7/6 20:35:52
 *└────────────────────────┘*/

using System.IO;
using TaurenEngine.Unity;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public class LinkProjectWindow : EditorWindow
	{
		[MenuItem("TaurenEngine/Project/链接引擎到项目工具")]
		private static void ShowWindow()
		{
			var window = EditorWindow.GetWindow<LinkProjectWindow>("链接引擎到项目工具");
			window.minSize = new Vector2(500, 60);
			window.Show();
		}

		private string _hotfixPath;

		private void OnGUI()
		{
			_hotfixPath = EditorGUILayout.TextField("项目热更代码全路径地址：", _hotfixPath);

			if (GUILayout.Button("链接项目"))
			{
				var path = PathEx.FormatPathEnd(_hotfixPath, false);
				path = path.Replace("\\", "/");
				if (Directory.Exists(_hotfixPath))
				{
					var index = path.IndexOf("Assets/");
					if (index >= 0)
					{
						var bat = new BatScript();
						bat.MKLink($"{Application.dataPath}/TaurenEngine", $"{path.Substring(0, index + 7)}/TaurenEngine");
						bat.MKLink($"{Application.dataPath}/TaurenEngine.Hotfix/Framework.Hotfix", $"{path}/Framework.Hotfix");
						bat.Run();

						Debug.LogError($"链接项目完成");
					}
					else
						Debug.LogError($"项目热更代码地址未能识别，需限制在项目路径下：{_hotfixPath}");
				}
				else
					Debug.LogError($"地址不存在：{_hotfixPath}");
			}
		}
	}
}