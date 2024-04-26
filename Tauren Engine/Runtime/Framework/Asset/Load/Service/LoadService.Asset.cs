/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/10 11:36:43
 *└────────────────────────┘*/

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 单个文件加载
	/// </summary>
	public partial class LoadService
	{
		#region 同步加载
		private void LoadByAsset(LoadItem loadItem)
		{
			if (IAssetBundleService.Instance.InAssetBundle(loadItem.path))
			{

			}
			else
			{
				
			}
		}
		#endregion

		#region 异步加载
		private void LoadAsyncByAsset(LoadItemAsync loadItem)
		{
			if (IAssetBundleService.Instance.InAssetBundle(loadItem.path))
			{

			}
			else
			{

			}
		}
		#endregion
	}
}