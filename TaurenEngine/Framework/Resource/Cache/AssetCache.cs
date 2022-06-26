/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 11:56:31
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 缓存资源
	/// </summary>
	internal class AssetCache : Cache
	{
		/// <summary>
		/// 缓存资源
		/// </summary>
		public UnityEngine.Object data;
		/// <summary>
		/// 加载类型
		/// </summary>
		public LoadType loadType;

		/// <summary>
		/// 释放资源
		/// </summary>
		public override void Release()
		{
			TaurenFramework.Resource.Unload(data);
			data = null;
		}
	}
}