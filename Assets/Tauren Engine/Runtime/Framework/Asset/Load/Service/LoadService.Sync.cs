/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/10 14:25:40
 *└────────────────────────┘*/

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// 同步加载
	/// </summary>
	public partial class LoadService
	{
		public ILoadData Load(string path, LoadType loadType)
		{
			var loadItem = new LoadItem();
			loadItem.path = path;
			loadItem.loadType = loadType;

			if (loadType == LoadType.Asset)
			{
				LoadByAsset(loadItem);
			}
			else if (loadType == LoadType.Resources)
			{
				LoadByResources(loadItem);
			}
			else if (loadType == LoadType.RawFile)
			{
				LoadByFile(loadItem);	
			}

			if (loadItem.Code != LoadCode.Success)
				ToErrorLog(loadItem);

			return loadItem;
		}
	}
}