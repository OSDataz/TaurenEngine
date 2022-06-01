/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/25 17:16:13
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	public interface IFrame
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