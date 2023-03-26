/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:43:39
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// 获取组件，如果没有该组件，会自动添加组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject @object) where T : Component
        {
            if (@object.TryGetComponent<T>(out var component))
                return component;

            return @object.AddComponent<T>();
        }

        /// <summary>
        /// 销毁组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        public static void DestroyComponent<T>(this GameObject @object) where T : Component
        {
            if (@object.TryGetComponent<T>(out var component))
            {
                if (component is MonoComponent monoComponent)
                    monoComponent.Destroy();
                else
                    GameObject.Destroy(component);
            }
        }

		/// <summary>
		/// 获取子对象，如果没有该对象，会自动添加子对象
		/// </summary>
		/// <param name="object"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static GameObject GetOrCreateChild(this GameObject @object, string name)
		{
			var child = @object.transform.Find(name);
			if (child == null)
			{
				child = new GameObject(name).transform;
				child.SetParent(@object.transform);
			}
			return child.gameObject;
		}
	}
}