/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/6 23:37:42
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 资源组件
	/// </summary>
	public class ResourceComponent : OnceComponent
	{
		protected override void Awake()
		{
			base.Awake();

			if (TaurenFramework.Resource == null)
			{
				var resource = new ResourceManager();
				TaurenFramework.Resource = resource;
			}
		}
	}
}