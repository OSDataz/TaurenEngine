/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 14:48:13
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	public abstract class ResourceAdapt : ResourceBase
	{
		internal CacheManager cacheMgr = TaurenFramework.Resource.cacheMgr;
		internal AsyncLoadManager asyncLoadMgr = TaurenFramework.Resource.asyncLoadMgr;

		internal uint ToId() => ++TaurenFramework.Resource.toId;
	}
}