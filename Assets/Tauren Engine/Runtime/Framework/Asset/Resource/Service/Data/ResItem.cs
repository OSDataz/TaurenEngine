/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/9 17:53:55
 *└────────────────────────┘*/

namespace Tauren.Framework.Runtime
{
	public class ResItem
	{
		/// <summary> 资源地址 </summary>
		public string path;

		/// <summary> 加载类型 </summary>
		public LoadType loadType;

		/// <summary> 所属AB包地址（若未空表示不在AB包中）</summary>
		public string abPath;

		/// <summary> 是否在AB包中 </summary>
		public bool InAssetBundle => string.IsNullOrEmpty(abPath);
	}
}