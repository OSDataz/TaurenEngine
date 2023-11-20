/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.9.0
 *│　Time    ：2022/11/23 21:17:52
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 引用对象
	/// </summary>
	public interface IRefrenceObject : IDestroyed
	{
		/// <summary>
		/// 引用计数
		/// </summary>
		int RefCount { get; set; }
	}

	/// <summary>
	/// 接口IRefrenceObject的扩展方法
	/// </summary>
	public static class IRefrenceObjectExtension
	{
		/// <summary>
		/// 该对象被持有，增加引用计数
		/// </summary>
		/// <param name="object"></param>
		internal static void AddRefCount(this IRefrenceObject @object)
		{
			@object.RefCount += 1;
		}

		/// <summary>
		/// 释放引用计数，如果引用计数为0，将触发Destroy销毁对象
		/// </summary>
		/// <param name="object"></param>
		internal static void DelRefCount(this IRefrenceObject @object)
		{
			if (--@object.RefCount == 0)
				@object.Destroy();
		}
	}
}