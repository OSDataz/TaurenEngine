/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/5/30 22:26:41
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.Framework
{
	internal class ResourceConfig
	{
		/// <summary>
		/// AB包列表
		/// </summary>
		public List<ABPackItemConfig> abList;
		/// <summary>
		/// 资源列表
		/// </summary>
		public List<AssetItemConfig> assetList;
	}

	internal class ABPackItemConfig
	{
		public string name;
		public List<string> assetIds;
	}

	internal class AssetItemConfig
	{
		public string guid;
		public string path;
		public string abPackName;
		public List<string> dependencies;
	}
}