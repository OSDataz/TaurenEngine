/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/24 10:12:22
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 使用对象池对象（谨慎使用，建议只用于简单对象，复杂对象继承结构不利于后续维护）
	/// </summary>
	/// <typeparam name="T">当前对象类型</typeparam>
	public abstract class PoolObject<T> : RefrenceObject, IRecycle where T : PoolObject<T>, new()
	{
		#region 对象池静态函数
		public static ObjectPool<T> GetPool() => PoolHelper.GetPool<T>();
		#endregion

		private ObjectPool<T> _pool;

		public abstract void Clear();

		/// <summary>
		/// 将当前对象回收进对象池
		/// </summary>
		public override void Destroy()
		{
			_pool ??= GetPool();// 主要性能考虑，重复使用时避免两次查找操作
			_pool.Add(this as T);
		}

		public override void OnDestroy()
		{
			Clear();
			_pool = null;
		}
	}
}