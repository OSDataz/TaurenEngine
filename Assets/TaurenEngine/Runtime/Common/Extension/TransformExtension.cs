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
        /// 获取所有下一层级指定组件
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public static Transform[] GetChilds(this Transform @object)
        {
			if (@object.childCount == 0)
				return new Transform[0];

			var len = @object.childCount;
			var childs = new Transform[len];
			for (var i = 0; i < len; ++i)
			{
				childs[i] = @object.GetChild(i);
			}

			return childs;
		}
	}
}