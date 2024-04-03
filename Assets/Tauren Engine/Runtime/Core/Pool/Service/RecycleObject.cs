/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/24 10:12:22
 *└────────────────────────┘*/

namespace Tauren.Core.Runtime
{
	/// <summary>
	/// 使用对象池对象（谨慎使用，建议只用于简单对象，复杂对象继承结构不利于后续维护）
	/// </summary>
	/// <typeparam name="T">当前对象类型</typeparam>
	public abstract class RecycleObject<T> : DObject, IRecycle where T : RecycleObject<T>, new()
	{
		#region 对象池静态函数
		private static IPool _pool;

		/// <summary>
		/// 获取对象池
		/// </summary>
		/// <returns></returns>
		public static IPool GetPool() => _pool ??= IPoolService.Instance.GetPool(typeof(T));

		/// <summary>
		/// 从对象池中取出对象
		/// </summary>
		/// <returns></returns>
		public static T GetFromPool() => GetPool().Get() as T;

		/// <summary>
		/// 重置对象池
		/// </summary>
		public static void ResetPool() => _pool = null;
		#endregion

		public abstract void Clear();

		/// <summary>
		/// 将当前对象回收进对象池
		/// </summary>
		public virtual void RecycleToPool()
		{
			GetPool().Recycle(this);
		}

		protected override void OnDestroy()
		{
			Clear();
		}
	}
}