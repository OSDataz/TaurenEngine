/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/2 21:12:21
 *└────────────────────────┘*/

namespace Tauren.Framework.Runtime
{
	public interface ITimer
	{
		bool IsRunning { get; }

		void Start();

		void Stop();
	}
}