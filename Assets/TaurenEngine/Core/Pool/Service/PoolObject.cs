/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/24 10:12:22
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 使用对象池对象（谨慎使用，建议只用于简单对象，复杂对象继承结构不利于后续维护）
	/// </summary>
	/// <typeparam name="T">当前对象类型</typeparam>
	public abstract class PoolObject<T> : DObject, IRecycle where T : PoolObject<T>, new()
	{
		#region 对象池静态函数
		private static ObjectPool<T> _pool;

		/// <summary>
		/// 获取对象池
		/// </summary>
		/// <returns></returns>
		public static ObjectPool<T> GetPool() => _pool ??= IPoolService.Instance.GetOrCreatePool<T>();

		/// <summary>
		/// 重置对象池
		/// </summary>
		public static void ResetPool() => _pool = null;
		#endregion

		public abstract void Clear();

		/// <summary>
		/// 将当前对象回收进对象池，正常情况下不须主动调用，会在移除引用队列后自动清理
		/// </summary>
		public override void Destroy()
		{
			GetPool().Add(this as T);
		}

		public override void OnDestroy()
		{
			Clear();
		}
	}
}