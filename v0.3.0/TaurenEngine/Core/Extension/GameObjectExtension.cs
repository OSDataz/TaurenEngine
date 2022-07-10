/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/24 13:59:29
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Core
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// 获取所有直接子对象
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static List<GameObject> GetChilds(this GameObject @object)
        {
            return @object.transform.GetChilds();
        }

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
    }
}