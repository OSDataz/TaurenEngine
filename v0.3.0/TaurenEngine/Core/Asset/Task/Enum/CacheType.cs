/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/19 21:44:26
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public enum CacheType
	{
		/// <summary>
		/// 不缓存
		/// </summary>
		NoCahce,
		/// <summary>
		/// 缓存（但长时间不引用会释放）
		/// </summary>
		Cahche,
		/// <summary>
		/// 永久缓存（只要有一个请求永久缓存，该资源就会永久缓存）
		/// </summary>
		PermanentCache
	}
}