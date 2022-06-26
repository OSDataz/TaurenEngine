/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 12:00:50
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.Framework
{
	internal class ResourceConfig
	{
		/// <summary>
		/// AB包列表
		/// </summary>
		public List<ABConfig> abList;
		/// <summary>
		/// 资源列表
		/// </summary>
		public List<AssetConfig> assetList;
	}

	internal class ABConfig
	{
		/// <summary>
		/// AB包名字
		/// </summary>
		public string name;
		/// <summary>
		/// AB包路径
		/// </summary>
		public string path;
		/// <summary>
		/// AB包包含资源GUID
		/// </summary>
		public List<string> assetIds;
	}

	internal class AssetConfig
	{
		/// <summary>
		/// 资源GUID
		/// </summary>
		public string guid;
		/// <summary>
		/// 资源路径
		/// </summary>
		public string path;
		/// <summary>
		/// 所属AB包名字
		/// </summary>
		public string abName;
		/// <summary>
		/// 依赖资源GUID列表
		/// </summary>
		public List<string> dependencies;
	}
}