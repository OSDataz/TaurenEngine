/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/24 16:33:44
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 能被销毁的对象
	/// </summary>
	public abstract class DObject : IDestroyed
	{
		public virtual bool IsDestroyed { get; protected set; }

		/// <summary>
		/// 销毁对象；引用计算为0时也会调用
		/// </summary>
		public virtual void Destroy()
		{
			if (IsDestroyed)
				return;

			IsDestroyed = true;

			OnDestroy();
		}

		/// <summary>
		/// 对象无引用时系统会自行调用
		/// <para>手动调用请使用Destroy</para>
		/// </summary>
		public void Dispose()
		{
			if (IsDestroyed)
				return;

			IsDestroyed = true;

			OnDestroy();
		}

		/// <summary>
		/// 销毁释放时调用，推荐在这里写释放逻辑
		/// </summary>
		public virtual void OnDestroy() { }
	}
}