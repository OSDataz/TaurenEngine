/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/2/28 17:28:41
 *└────────────────────────┘*/

using UnityEditor;

namespace TaurenEditor.Tools
{
	public static class ResourceTools
	{
		[MenuItem("Assets/资源检测/查找依赖外部文件夹资源", true)]
		private static bool CheckExternalDependenciesValidate()
		{
			if (Selection.activeObject == null)
				return false;

			return Selection.activeObject is DefaultAsset;
		}

		[MenuItem("Assets/资源检测/查找依赖外部文件夹资源", false, 10)]
		private static void CheckExternalDependencies()
		{
			if (Selection.activeObject is DefaultAsset file)
			{
				var activePath = AssetDatabase.GetAssetPath(Selection.activeObject);
				if (string.IsNullOrEmpty(activePath))
					return;

				ResourceHelper.CheckExternalDependencies(activePath);
			}
		}
	}
}