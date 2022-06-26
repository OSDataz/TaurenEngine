/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/16 20:15:06
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 配置组件
	/// </summary>
	public class ConfigComponent : OnceComponent
	{
		protected override void Awake()
		{
			base.Awake();

			if (TaurenFramework.Config == null)
			{
				var config = new ConfigManager();
				TaurenFramework.Config = config;
			}
		}
	}
}