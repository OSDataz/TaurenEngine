/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:53:34
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine
{
	public static class GameObjectUtility
	{
		/// <summary>
		/// 获取指定GameObject，如果没找到，则创建GameObject
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static GameObject GetOrCreateGameObject(string name)
		{
			return GameObject.Find(name) ?? new GameObject(name);
		}

		/// <summary>
		/// 【不建议频繁使用】销毁指定的GameObject
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static bool DestroyGameObject(string name, bool immediate = false)
		{
			var go = GameObject.Find(name);
			if (go != null)
			{
				if (immediate) Object.DestroyImmediate(go);
				else Object.Destroy(go);
				return true;
			}

			return false;
		}
	}
}