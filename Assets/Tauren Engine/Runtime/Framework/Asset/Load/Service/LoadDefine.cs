/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/9 17:41:45
 *└────────────────────────┘*/

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 加载状态
	/// </summary>
	public enum LoadStatus
	{
		/// <summary>
		/// 未加载
		/// </summary>
		None,
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

	public class LoadCode
	{
		/// <summary>
		/// 未加载
		/// </summary>
		public const int None = -1;
		/// <summary>
		/// 加载成功
		/// </summary>
		public const int Success = 0;
		/// <summary>
		/// 加载失败
		/// </summary>
		public const int Fail = 1;
	}
}