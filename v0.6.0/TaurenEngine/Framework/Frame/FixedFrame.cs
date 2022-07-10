/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/25 17:13:19
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	internal class FixedFrame : Frame
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
				TaurenFramework.Frame.fixedPool.Add(this);
			else
				Destroy();
		}
	}
}