/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/18 0:00:07
 *└────────────────────────┘*/

using TaurenEngine.Unity;

namespace TaurenEngine.Framework
{
	public abstract class FrameworkComponent : MonoComponent
	{
		protected virtual void Awake()
		{
			TaurenFramework.AddComponent(this);
		}

		protected virtual void OnDestroy()
		{
			TaurenFramework.RemoveComponent(this);
		}
	}
}