/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/5 21:47:21
 *└────────────────────────┘*/

namespace TaurenEngine
{
	public static class PoolHelper
	{
		/// <summary>
		/// 获取对象池
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static ObjectPool<T> GetPool<T>() where T : IRecycle, new()
		{
			return IPoolService.Instance.GetPool<T>();
		}
	}
}