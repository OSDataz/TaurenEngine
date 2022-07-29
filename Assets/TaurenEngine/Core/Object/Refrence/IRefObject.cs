/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/19 23:51:01
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 带有引用计数的对象接口
	/// </summary>
	public interface IRefObject : IDestroyed
	{
		/// <summary>
		/// 引用计数
		/// <para>注意：切勿自行赋值</para>
		/// </summary>
		int RefCount { get; set; }
	}

	/// <summary>
	/// 接口IRefObject的扩展方法
	/// </summary>
	public static class IRefObjectExtension
	{
		/// <summary>
		/// 该对象被持有，增加引用计数
		/// </summary>
		/// <param name="object"></param>
		internal static void AddRefCount(this IRefObject @object)
		{
			@object.RefCount += 1;
		}

		/// <summary>
		/// 释放引用计数，如果引用计数为0，将触发Destroy销毁对象
		/// </summary>
		/// <param name="object"></param>
		internal static void DelRefCount(this IRefObject @object)
		{
			if (--@object.RefCount == 0)
				@object.Destroy();
		}
	}
}