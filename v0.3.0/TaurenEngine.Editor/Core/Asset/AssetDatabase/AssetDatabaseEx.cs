/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/8 0:03:39
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
	public class AssetDatabaseEx
	{
		public static T Load<T>(string filePath) where T : Object => AssetDatabase.LoadAssetAtPath<T>(filePath);
	}
}