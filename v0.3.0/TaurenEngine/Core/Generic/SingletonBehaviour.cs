/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/7/28 22:14:21
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
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
							TDebug.Error($"{typeof(T).Name} extends SingletonBehaviour more than 1!");
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