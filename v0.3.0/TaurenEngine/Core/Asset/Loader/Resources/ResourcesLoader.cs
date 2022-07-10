/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/20 11:04:12
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public class ResourcesLoader : AsyncLoader
	{
		public override int Type { get; } = (int)LoaderType.Resources;

		public override void ReleaseRes(LoadRes loadRes)
		{
			
		}
	}
}