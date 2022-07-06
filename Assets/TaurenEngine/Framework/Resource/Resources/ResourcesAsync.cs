/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 11:13:49
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.Framework
{
	internal class ResourcesAsync : AsyncLoaderBase
	{
		#region 对象池
		private static ObjectPool<ResourcesAsync> pool = new ObjectPool<ResourcesAsync>();

		public static ResourcesAsync Get() => pool.Get();

		/// <summary> 回收 </summary>
		public void Recycle() => pool.Add(this);
		#endregion

		public override void Load(AsyncLoadTask loadTask)
		{
			base.Load(loadTask);


		}
	}
}