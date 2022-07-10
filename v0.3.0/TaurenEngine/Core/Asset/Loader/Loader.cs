/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/20 19:23:49
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public abstract class Loader : AssetBase
	{
		/// <summary>
		/// 加载器类型
		/// </summary>
		public abstract int Type { get; }

		public abstract void ReleaseRes(LoadRes loadRes);
	}
}