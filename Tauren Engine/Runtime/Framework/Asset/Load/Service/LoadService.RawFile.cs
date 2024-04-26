/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/10 11:36:51
 *└────────────────────────┘*/

using System.IO;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 原生文件加载
	/// </summary>
	public partial class LoadService
	{
		#region 同步加载
		private void LoadByFile(LoadItem loadItem)
		{
			if (File.Exists(loadItem.path))
			{
				var asset = File.ReadAllBytes(loadItem.path);

				loadItem.Asset = CreateAsset(loadItem.path, asset);
				loadItem.Code = LoadCode.Success;
			}
			else
			{
				loadItem.Code = LoadCode.Fail;
			}
		}
		#endregion

		#region 异步加载
		private async void LoadAsyncByFile(LoadItemAsync loadItem)
		{
			if (File.Exists(loadItem.path))
			{
				var asset = await File.ReadAllBytesAsync(loadItem.path);

				loadItem.Asset = CreateAsset(loadItem.path, asset);
				loadItem.Code = LoadCode.Success;
			}
			else
			{
				loadItem.Code = LoadCode.Fail;
			}

			OnLoadAsyncComplete(loadItem);
		}
		#endregion
	}
}