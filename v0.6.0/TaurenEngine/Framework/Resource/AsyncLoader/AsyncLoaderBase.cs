/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 11:13:18
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.Framework
{
	/// <summary>
	/// 异步加载基类
	/// </summary>
	internal abstract class AsyncLoaderBase : IRecycle
	{
		public AsyncLoadTask Task { get; private set; }

		public virtual void Load(AsyncLoadTask loadTask)
		{
			Task = loadTask;
		}

		public virtual void Clear()
		{
			Task = null;
		}

		public virtual void Destroy()
		{
			Task = null;
		}
	}
}