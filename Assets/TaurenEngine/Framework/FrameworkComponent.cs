/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/18 0:00:07
 *└────────────────────────┘*/

namespace TaurenEngine
{
	public abstract class FrameworkComponent : MonoComponent
	{
		protected virtual void Awake()
		{
			TaurenFramework.AddComponent(this);
		}

		protected override void OnDestroy()
		{
			TaurenFramework.RemoveComponent(this);

			base.OnDestroy();
		}
	}
}