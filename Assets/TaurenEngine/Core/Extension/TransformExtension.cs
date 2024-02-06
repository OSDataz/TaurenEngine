/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:44:45
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.Core
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

		#region 根据中间路径查找节点
		/// <summary>
		/// 获取指定节点的指定组件
		/// </summary>
		/// <param name="nodePath"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetComponent<T>(this Transform @object, string nodePath) where T : Component
		{
			var childTransform = FindNode(@object, nodePath);
			if (childTransform == null)
			{
				Log.Error($"GetComponent获取节点失败，nodePath：{nodePath}");
				return null;
			}

			return childTransform.GetComponent<T>();
		}

		public static T GetOrAddComponent<T>(this Transform @object, string nodePath) where T : Component
		{
			var childTransform = FindNode(@object, nodePath);
			if (childTransform == null)
			{
				Log.Error($"GetOrAddComponent获取节点失败，nodePath：{nodePath}");
				return null;
			}

			var type = typeof(T);
			var comp = childTransform.GetComponent(type);
			if (comp != null)
				return comp as T;

			return childTransform.gameObject.AddComponent(type) as T;
		}

		/// <summary>
		/// 获取子节点的Transform对象
		/// </summary>
		/// <param name="nodePath"></param>
		/// <returns></returns>
		public static Transform FindNode(this Transform @object, string nodePath)
		{
			if (string.IsNullOrEmpty(nodePath))
			{
				Log.Error($"Find获取节点路径为空，nodePath：{nodePath}");
				return null;
			}

			var index = nodePath.IndexOf("/");
			if (index == -1)
				return FindNodeAux(@object, nodePath);

			return FindNodeAux(@object, nodePath.Substring(0, index), nodePath.Substring(index + 1));
		}

		private static Transform FindNodeAux(Transform transform, string nodeName)
		{
			var node = transform.Find(nodeName);
			if (node == null)
			{
				var len = transform.childCount;
				for (int i = 0; i < len; ++i)
				{
					node = FindNodeAux(transform.GetChild(i), nodeName);
					if (node != null)
						break;
				}
			}

			return node;
		}

		private static Transform FindNodeAux(Transform transform, string nodeHead, string nodeEnd)
		{
			var node = transform.Find(nodeHead);
			if (node != null)
			{
				node = node.Find(nodeEnd);
				if (node != null)
					return node;
			}

			var len = transform.childCount;
			for (int i = 0; i < len; ++i)
			{
				node = FindNodeAux(transform.GetChild(i), nodeHead, nodeEnd);
				if (node != null)
					return node;
			}

			return null;
		}
		#endregion

		#region 设置父节点
		public static void SetParentOrigin(this Transform @object, Transform parent)
		{
			@object.SetParent(parent);

			@object.localPosition = Vector3.zero;
			@object.localRotation = Quaternion.identity;
			@object.localScale = Vector3.one;
		}
		#endregion
	}
}