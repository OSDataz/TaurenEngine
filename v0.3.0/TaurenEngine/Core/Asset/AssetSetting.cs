/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/21 15:03:19
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public class AssetSetting : AssetBase
	{
		#region 加载器
		public void RegisterLoader(Loader loader)
			=> core.Loader.RegisterLoader(loader);
		#endregion

		#region 资源
		public void RegisterResource<TRes, TLoadRes>() where TLoadRes : LoadRes<TRes>, new()
			=> core.LoadRes.RegisterResource<TRes, TLoadRes>();
		#endregion
	}
}