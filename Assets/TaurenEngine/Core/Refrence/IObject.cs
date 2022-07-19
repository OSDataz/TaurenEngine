/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/19 23:43:11
 *└────────────────────────┘*/

using System;

namespace TaurenEngine.Core
{
	/// <summary>
	/// 能被销毁释放的对象接口
	/// </summary>
	public interface IObject : IDisposable
	{
		/// <summary>
		/// 该对象是否已经被销毁释放
		/// </summary>
		bool IsDisposed { get; }
	}
}