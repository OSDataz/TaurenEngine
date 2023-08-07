/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/8/24 20:55:22
 *└────────────────────────┘*/

namespace TaurenEngine.Runtime.Core
{
	/// <summary>
	/// 线程任务接口，用户需要继承此接口，并提供任务的run方法实现
	/// </summary>
	public interface IThreadTask
	{
		/// <summary>
		/// 任务执行函数
		/// </summary>
		void Run();
	}
}