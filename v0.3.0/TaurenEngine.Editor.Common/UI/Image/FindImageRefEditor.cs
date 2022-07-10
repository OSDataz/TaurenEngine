/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/10/26 15:03:33
 *└────────────────────────┘*/

using TaurenEngine.Core;
using TaurenEngine.Editor.Common;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TaurenEngine.Editor
{
	public static class FindImageRefEditor
	{
		[MenuItem(MenuName.FindImageReferencesInUIPrefabs, true)]
		public static bool FindImageRefValidate()
		{
			if (Selection.activeObject == null)
				return false;

			return Selection.activeObject is Texture2D;
		}

		[MenuItem(MenuName.FindImageReferencesInUIPrefabs, false, 29)]
		public static void FindImageRef()
		{
			var tex2D = Selection.activeObject as Texture2D;

			TDebug.Log($"开始查询图片【{Selection.activeObject.name}】引用");

			AssetsUtil.ForeachPrefabs((prefab) => 
			{
				var images = prefab.GetComponentsInChildren<Image>();
				if (images?.Length <= 0)
					return;

				foreach (var image in images)
				{
					if (image.sprite == null)
						continue;

					if (image.sprite.texture.Equals(tex2D) && PrefabUtility.GetPrefabAssetType(image.gameObject) == PrefabAssetType.NotAPrefab)
					{
						var go = image.gameObject;
						var comStr = go.name;

						while (go.transform.parent != null)
						{
							go = go.transform.parent.gameObject;
							comStr = $"{go.name}\\ {comStr}";
						}

						TDebug.Log($"引用：{comStr}");
					}
				}
			});

			TDebug.Log("查询结束");
		}
	}
}