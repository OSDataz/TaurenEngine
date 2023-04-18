/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:50:12
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.Unity
{
	/// <summary>
	/// MonoBehaviour单例，基于整个框架设计不推荐使用
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class SingletonComponent<T> : MonoComponent where T : SingletonComponent<T>
	{
		private static T instance = null;

		/// <summary>
		/// 单例实例
		/// </summary>
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
							Log.Error($"{typeof(T).Name} extends SingletonBehaviour more than 1!");
					}
					else
					{
						var name = typeof(T).Name;
						var go = GameObject.Find(name);
						if (go == null)
							go = new GameObject(name);

						instance = go.GetOrAddComponent<T>();

						DontDestroyOnLoad(instance.gameObject);// 保证实例不会被释放
					}
				}

				return instance;
			}
		}

		protected override void OnDestroy()
		{
			instance = null;

			base.OnDestroy();
		}
	}
}