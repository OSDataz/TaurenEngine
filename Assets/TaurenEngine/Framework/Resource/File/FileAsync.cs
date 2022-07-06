/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 11:16:08
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.Framework
{
	internal class FileAsync : AsyncLoaderBase
	{
		#region 对象池
		private static ObjectPool<FileAsync> pool = new ObjectPool<FileAsync>();

		public static FileAsync Get() => pool.Get();

		/// <summary> 回收 </summary>
		public void Recycle() => pool.Add(this);
		#endregion

		public override void Load(AsyncLoadTask loadTask)
		{
			base.Load(loadTask);


		}
	}
}