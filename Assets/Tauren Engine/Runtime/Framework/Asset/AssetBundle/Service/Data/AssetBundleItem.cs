/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/18 17:37:36
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace Tauren.Framework.Runtime
{
	public class AssetBundleItem
	{
		public string path;

		public AssetBundle bundle;

		/// <summary>
		/// 依赖其他AB包
		/// </summary>
		public List<AssetBundle> dependencies;
	}
}