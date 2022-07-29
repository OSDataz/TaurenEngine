/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/24 10:12:22
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 使用对象池对象
	/// </summary>
	/// <typeparam name="T">当前对象类型</typeparam>
	public abstract class PoolObject<T> : DObject, IRecycle where T : PoolObject<T>, new()
	{
		#region 对象池静态接口
		/// <summary>
		/// 对象池
		/// </summary>
		protected static readonly ObjectPool<T> Pool = new ObjectPool<T>();

		/// <summary>
		/// 从对象池中获取一个对象
		/// </summary>
		/// <returns></returns>
		public static T GetFromPool() => Pool.Get();

		/// <summary>
		/// 设置对象池最大缓存数量
		/// </summary>
		/// <param name="maximum"></param>
		public static void SetPoolMaximum(int maximum) => Pool.Maximum = maximum;

		/// <summary>
		/// 清理对象池
		/// </summary>
		public static void ClearPool() => Pool.Clear();
		#endregion

		public abstract void Clear();

		/// <summary>
		/// 将当前对象回收进对象池
		/// </summary>
		public override void Destroy()
		{
			Pool.Add(this as T);
		}

		public override void OnDestroy()
			=> Clear();
	}
}