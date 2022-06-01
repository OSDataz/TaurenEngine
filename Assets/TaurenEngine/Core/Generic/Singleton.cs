﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:49:16
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