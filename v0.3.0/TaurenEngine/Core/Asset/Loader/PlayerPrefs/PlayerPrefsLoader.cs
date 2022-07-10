/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/20 11:04:25
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public class PlayerPrefsLoader : Loader
	{
		public override int Type { get; } = (int)LoaderType.PlayerPrefs;

		public override void ReleaseRes(LoadRes loadRes)
		{

		}
	}
}