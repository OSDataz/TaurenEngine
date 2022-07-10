/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v2.0
 *│　Time    ：2021/7/31 0:05:44
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 【注意】切勿自行创建，请使用FrameManager
	/// </summary>
	internal class FrameLoop : Frame
	{
		public override void CheckPoolDestroy()
		{
			if (usePool)
				FrameManager.Instance.loopPool.Add(this);
			else
				Destroy();
		}
	}
}