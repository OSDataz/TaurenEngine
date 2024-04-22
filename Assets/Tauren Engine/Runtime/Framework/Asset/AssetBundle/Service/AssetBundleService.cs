/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 21:12:51
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	/// <summary>
	/// AB包服务
	/// </summary>
	public class AssetBundleService : IAssetBundleService
	{
		public bool Enabled { get; set; }

		public AssetBundleService()
		{
			this.InitInterface();
		}

		#region 数据
		public readonly Dictionary<string, AssetItem> assetMap = new Dictionary<string, AssetItem>();
		public readonly Dictionary<string, AssetBundleItem> abMap = new Dictionary<string, AssetBundleItem>();

		public bool InAssetBundle(string assetPath)
		{
			if (!Enabled)
				return false;

			return assetMap.ContainsKey(assetPath);
		}
		#endregion
	}
}