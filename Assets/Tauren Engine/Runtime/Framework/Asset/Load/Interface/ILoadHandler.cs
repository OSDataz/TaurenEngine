/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/7 15:34:51
 *└────────────────────────┘*/

using Tauren.Core.Runtime;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 加载句柄
	/// </summary>
	public interface ILoadHandler : IDestroyed
	{
		/// <summary>
		/// 停止加载，卸载
		/// </summary>
		void Unload();
	}
}