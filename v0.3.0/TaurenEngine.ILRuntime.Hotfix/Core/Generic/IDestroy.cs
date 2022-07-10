/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/11/6 18:14:26
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 可销毁对象
	/// </summary>
	public interface IDestroy
	{
		/// <summary>
		/// 是否已销毁
		/// </summary>
		bool IsDestroyed { get; }
		/// <summary>
		/// 销毁对象，销毁后不可再使用
		/// </summary>
		void Destroy();
	}
}