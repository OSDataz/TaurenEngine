/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/19 23:43:11
 *└────────────────────────┘*/

using System;

namespace Tauren.Core.Runtime
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
	}
}