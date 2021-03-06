/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:50:12
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine
{
	/// <summary>
	/// MonoBehaviour单例，基于整个框架设计不推荐使用
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class SingletonBehaviour<T> : MonoComponent where T : SingletonBehaviour<T>
	{
		private static T instance = null;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					var objects = FindObjectsOfType<T>();
					if (objects.Length > 0)
					{
						instance = objects[0];
						if (objects.Length > 1)
							Debug.LogError($"{typeof(T).Name} extends SingletonBehaviour more than 1!");
					}
					else
					{
						instance = GameObjectHelper.GetOrCreateGameObject(typeof(T).Name).GetOrAddComponent<T>();
						DontDestroyOnLoad(instance.gameObject);// 保证实例不会被释放
					}
				}

				return instance;
			}
		}

		protected virtual void OnDestroy()
		{
			instance = null;
		}
	}
}