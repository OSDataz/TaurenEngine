/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/9/8 10:05:25
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public abstract class Singleton<T> where T : class, new()
	{
		private static readonly object _lock = new object();
		private static T _instance;
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock)
					{
						_instance ??= new T();
					}
				}
				return _instance;
			}
		}
	}
}