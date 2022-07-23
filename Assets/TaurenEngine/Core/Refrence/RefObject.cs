/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/19 23:59:55
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 引用计数型对象基类
	/// </summary>
	public class RefObject : IRefObject
	{
		public int RefCount { get; set; }

		public virtual bool IsDestroyed { get; private set; }

		/// <summary>
		/// 对象无引用时会自行调用；引用计算为0时也会调用
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