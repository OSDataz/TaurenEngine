/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/20 0:56:05
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public class LoadSetting : IRecycle
	{
		#region 对象池
		public static LoadSetting Get() => SuperPool.Instance.Get<LoadSetting>();

		public void Recycle() => SuperPool.Instance.Add(this);
		#endregion

		/// <summary>
		/// 加载优先级（数值越大，加载优先级越高）
		/// </summary>
		public int loadPriority;
		/// <summary>
		/// 缓存类型
		/// </summary>
		public CacheType cacheType;
		/// <summary>
		/// 是否获取未引用的新对象
		/// </summary>
		public bool isNewRes;
		/// <summary>
		/// 是否重新加载（不管资源是否有缓存）
		/// </summary>
		public bool isNewLoad;

		public void Clear()
		{
			loadPriority = 1;
			cacheType = CacheType.Cahche;
			isNewRes = false;
			isNewLoad = false;
		}

		public void Destroy() => Clear();
	}
}