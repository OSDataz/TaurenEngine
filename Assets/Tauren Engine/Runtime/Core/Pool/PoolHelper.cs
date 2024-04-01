/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/3/21 16:09:40
 *└────────────────────────┘*/

using System;

namespace Tauren.Core.Runtime
{
	public static class PoolHelper
	{
		/// <summary>
		/// 获取一个指定类型的实例对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T Get<T>() where T : IRecycle, new()
		{
			return IPoolService.Instance.Get<T>();
		}

		/// <summary>
		/// 回收一个对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		public static void Recycle<T>(T item) where T : IRecycle, new()
		{
			IPoolService.Instance.Recycle(item);
		}

		/// <summary>
		/// 设置对象池缓存数量
		/// </summary>
		/// <param name="type"></param>
		/// <param name="size"></param>
		public static void SetPoolSize(Type type, int size)
		{
			IPoolService.Instance.SetPoolSize(type, size);
		}

		/// <summary>
		/// 设置对象池缓存数量
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="size"></param>
		public static void SetPoolSize<T>(int size) where T : IRecycle, new()
		{
			IPoolService.Instance.SetPoolSize(typeof(T), size);
		}

		/// <summary>
		/// 清理对象池
		/// </summary>
		/// <param name="type"></param>
		public static void ClearPool(Type type)
		{
			IPoolService.Instance.ClearPool(type);
		}

		/// <summary>
		/// 清理对象池
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static void ClearPool<T>() where T : IRecycle, new()
		{
			IPoolService.Instance.ClearPool(typeof(T));
		}

		/// <summary>
		/// 销毁对象池
		/// </summary>
		/// <param name="type"></param>
		public static void DestroyPool(Type type)
		{
			IPoolService.Instance.DestroyPool(type);
		}

		/// <summary>
		/// 销毁对象池
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static void DestroyPool<T>() where T : IRecycle, new()
		{
			IPoolService.Instance.DestroyPool(typeof(T));
		}
	}
}