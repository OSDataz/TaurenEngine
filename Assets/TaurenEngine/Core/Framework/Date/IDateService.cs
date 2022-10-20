/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/20 20:42:37
 *└────────────────────────┘*/

namespace TaurenEngine
{
	public interface IDateService : IService
	{
		/// <summary>
		/// 自启动时间（支持子线程访问）
		/// </summary>
		float RealtimeSinceStartup { get; }
	}
}