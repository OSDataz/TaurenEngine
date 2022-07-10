/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/20 11:04:04
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public class FileLoader : AsyncLoader
	{
		public override int Type { get; } = (int)LoaderType.File;

		public override void ReleaseRes(LoadRes loadRes)
		{

		}
	}
}