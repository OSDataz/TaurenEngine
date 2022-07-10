/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/19 16:10:35
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 加载状态
	/// </summary>
	internal enum LoadState
	{
		/// <summary>
		/// 等待中
		/// </summary>
		Wait,
		/// <summary>
		/// 等待其他同样资源加载
		/// </summary>
		WaitLoad,
		/// <summary>
		/// 加载中
		/// </summary>
		Loading,
		/// <summary>
		/// 加载成功
		/// </summary>
		Success,
		/// <summary>
		/// 加载失败
		/// </summary>
		Fail
	}
}