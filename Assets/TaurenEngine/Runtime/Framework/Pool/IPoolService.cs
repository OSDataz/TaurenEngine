/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/4 10:51:55
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Runtime.Framework
{
	/// <summary>
	/// 对象池服务
	/// </summary>
	public interface IPoolService : IService
	{
		/// <summary>
		/// 接口唯一实例
		/// </summary>
		public static IPoolService Instance { get; internal set; }

		/// <summary>
		/// 获取或创建对象池
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		ObjectPool<T> GetOrCreatePool<T>() where T : IRecycle, new();

		/// <summary>
		/// 获取或创建对象池
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		TypePool GetOrCreatePool(Type type);

		/// <summary>
		/// 从对象池取出一个对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T Get<T>() where T : IRecycle, new();

		/// <summary>
		/// 向对象池放入一个对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		void Add<T>(T item) where T : IRecycle, new();

		/// <summary>
		/// 清理对象池
		/// </summary>
		/// <typeparam name="T"></typeparam>
		void ClearPool<T>() where T : IRecycle, new();

		/// <summary>
		/// 销毁对象池
		/// </summary>
		/// <typeparam name="T"></typeparam>
		void DestroyPool<T>() where T : IRecycle, new();
	}

	public static class IPoolServiceExtension
	{
		public static void InitInterface(this IPoolService @object)
		{
			if (IPoolService.Instance != null)
				Log.Error("IPoolService重复创建实例");

			IPoolService.Instance = @object;
		}
	}
}