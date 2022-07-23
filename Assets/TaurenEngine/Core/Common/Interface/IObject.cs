/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/19 23:43:11
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 能被销毁的对象接口
	/// </summary>
	public interface IObject
	{
		/// <summary>
		/// 该对象是否已经被销毁
		/// <para>注意：切勿自行赋值</para>
		/// </summary>
		bool IsDestroyed { get; set; }

		/// <summary>
		/// 销毁对象执行响应
		/// </summary>
		void OnDestroy();
	}

	public static class IObjectExtension
	{
		/// <summary>
		/// 销毁对象
		/// </summary>
		/// <param name="object"></param>
		public static void Destroy(this IObject @object)
		{
			if (@object.IsDestroyed)
				return;

			@object.IsDestroyed = true;
			@object.OnDestroy();
		}
	}
}