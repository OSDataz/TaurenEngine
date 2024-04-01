/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/4 10:51:55
 *└────────────────────────┘*/

using System;

namespace Tauren.Core.Runtime
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
		/// 从对象池取出一个对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T Get<T>() where T : IRecycle, new();

		/// <summary>
		/// 向对象池回收一个对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		void Recycle<T>(T item) where T : IRecycle, new();

		/// <summary>
		/// 设置对象池缓存数量
		/// </summary>
		/// <param name="type"></param>
		/// <param name="size"></param>
		void SetPoolSize(Type type, int size);

		/// <summary>
		/// 清理对象池
		/// </summary>
		/// <param name="type"></param>
		void ClearPool(Type type);

		/// <summary>
		/// 销毁对象池
		/// </summary>
		/// <param name="type"></param>
		void DestroyPool(Type type);
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