/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/25 17:13:47
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 【注意】切勿自行创建，请使用 TaurenFramework.Frame
	/// </summary>
	internal class UpdateFrame : Frame
	{
		public override void Start()
		{
			if (Running)
				return;

			Running = true;

			TaurenFramework.Frame.AddFrame(this);
		}

		public override void Stop()
		{
			if (!Running)
				return;

			Running = false;

			TaurenFramework.Frame.RemoveFrame(this);
		}

		public override void RecoveryOrDestroy()
		{
			if (usePool)
				TaurenFramework.Frame.updatePool.Add(this);
			else
				Destroy();
		}
	}
}