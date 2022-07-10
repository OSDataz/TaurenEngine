/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/26 11:56:42
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Core
{
    public static class GameObjectHelper
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
		/// 销毁指定的GameObject
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static bool DestroyGameObject(string name)
		{
			var go = GameObject.Find(name);
			if (go != null)
			{
				GameObject.Destroy(go);
				return true;
			}

			return false;
		}
	}
}