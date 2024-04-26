/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/24 16:33:44
 *└────────────────────────┘*/

namespace Tauren.Core.Runtime
{
	/// <summary>
	/// 有生命周期，能被销毁的对象
	/// </summary>
	public abstract class DObject : IDestroyed
	{
		public virtual bool IsDestroyed { get; protected set; }

		/// <summary>
		/// 对象无引用时系统会自行调用
		/// </summary>
		public void Dispose()
		{
			if (IsDestroyed)
				return;

			IsDestroyed = true;

			OnDestroy();
		}

		protected virtual void OnDestroy() { }
	}
}