/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/19 23:43:11
 *└────────────────────────┘*/

using System;

namespace TaurenEngine
{
	/// <summary>
	/// 能被销毁的对象接口
	/// </summary>
	public interface IDestroyed : IDisposable
	{
		/// <summary>
		/// 该对象是否已经被销毁
		/// </summary>
		bool IsDestroyed { get; }

		/// <summary>
		/// 【调动】销毁对象，可能不会实际销毁对象（或放入对象池）
		/// </summary>
		void Destroy();

		/// <summary>
		/// 【重写】销毁对象执行响应，实际销毁逻辑。切勿写重复使用逻辑
		/// </summary>
		void OnDestroy();
	}
}