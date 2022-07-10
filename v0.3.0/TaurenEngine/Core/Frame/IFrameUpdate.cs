/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/8/7 21:51:49
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public interface IFrameUpdate
	{
		/// <summary>
		/// 开启帧循环/计时器
		/// </summary>
		void Start();
		/// <summary>
		/// 暂停帧循环/计时器
		/// </summary>
		void Stop();
		/// <summary>
		/// 销毁帧循环/计时器
		/// </summary>
		void Destroy();
	}
}