/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:44:45
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine
{
    public static class TransformExtension
    {
        /// <summary>
        /// 获取所有直接子对象
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static List<GameObject> GetChilds(this Transform @object)
        {
            var childs = new List<GameObject>();
            if (@object.childCount == 0)
                return childs;

            var len = @object.childCount;
            for (var i = 0; i < len; ++i)
            {
                childs.Add(@object.GetChild(i).gameObject);
            }

            return childs;
        }

        /// <summary>
        /// 获取组件，如果没有该组件，会自动添加组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this Transform @object) where T : Component
        {
            return @object.gameObject.GetOrAddComponent<T>();
        }

        /// <summary>
        /// 获取子对象，如果没有该对象，会自动添加子对象
        /// </summary>
        /// <param name="object"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject GetOrCreateChild(this Transform @object, string name)
        {
            var child = @object.Find(name);
            if (child == null)
            {
                child = new GameObject(name).transform;
                child.SetParent(@object);
            }
            return child.gameObject;
        }

        /// <summary>
        /// 获取直接子对象的指定组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// <returns></returns>
        public static T GetComponentInChild<T>(this Transform @object) where T : Component
        {
            var len = @object.childCount;
            for (int i = 0; i < len; ++i)
            {
                if (@object.GetChild(i).TryGetComponent<T>(out var component))
                    return component;
            }

            return null;
        }

        /// <summary>
        /// 获取直接子对象的所有组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="object"></param>
        /// <returns></returns>
        public static List<T> GetComponentsInChild<T>(this Transform @object) where T : Component
        {
            var list = new List<T>();
            var len = @object.childCount;
            for (int i = 0; i < len; ++i)
            {
                if (@object.GetChild(i).TryGetComponent<T>(out var component))
                    list.Add(component);
            }

            return list;
        }

        /// <summary>
        /// 设置父对象，并设置到初始位置、旋转、缩放
        /// </summary>
        /// <param name="object"></param>
        /// <param name="parent"></param>
        public static void SetParentOrigin(this Transform @object, Transform parent)
        {
            @object.SetParent(parent);

            @object.localPosition = Vector3.zero;
            @object.localRotation = Quaternion.identity;
            @object.localScale = Vector3.one;
        }

        /// <summary>
        /// 设置父对象，并设置到初始位置、旋转和原始缩放
        /// </summary>
        /// <param name="object"></param>
        /// <param name="parent"></param>
        public static void SetParentOriginScale(this Transform @object, Transform parent)
        {
            var scale = @object.localScale;

            @object.SetParent(parent);

            @object.localPosition = Vector3.zero;
            @object.localRotation = Quaternion.identity;
            @object.localScale = scale;
        }
    }
}