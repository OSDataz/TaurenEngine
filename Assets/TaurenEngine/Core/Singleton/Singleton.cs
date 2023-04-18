/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:49:16
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 独立的单例
	/// <para>注意：当前框架设计单例统一管理，不推荐使用，避免单个管理脱管</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Singleton<T> where T : class, new()
	{
		private static readonly object _lock = new object();

		private static T _instance;
		/// <summary>
		/// 单例实例
		/// </summary>
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