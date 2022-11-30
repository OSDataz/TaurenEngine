/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/4 10:51:55
 *└────────────────────────┘*/

using System;
using UnityEngine;

namespace TaurenEngine
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
		/// 获取对象池
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		ObjectPool<T> GetPool<T>() where T : IRecycle, new();

		/// <summary>
		/// 获取对象池
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		TypePool GetPool(Type type);

		/// <summary>
		/// 向对象池放入一个对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		bool Add<T>(T item) where T : IRecycle, new();

		/// <summary>
		/// 从对象池取出一个对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T Get<T>() where T : IRecycle, new();

		/// <summary>
		/// 对象池中是否有该对象
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		bool Contains<T>(T item) where T : IRecycle, new();

		/// <summary>
		/// 设置对象池最大缓存数量
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="maximum"></param>
		void SetMaximum<T>(int maximum) where T : IRecycle, new();

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
		public static void InitInterface(this IPoolService @object, IPoolService instance)
		{
			if (IPoolService.Instance != null)
				Debug.LogError("IPoolService重复创建实例");

			IPoolService.Instance = instance;
		}
	}
}