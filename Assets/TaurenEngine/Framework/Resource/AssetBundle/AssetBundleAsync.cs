/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 11:20:53
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.Framework
{
	internal class AssetBundleAsync : AsyncLoaderBase
	{
		#region 对象池
		private static ObjectPool<AssetBundleAsync> pool = new ObjectPool<AssetBundleAsync>();

		public static AssetBundleAsync Get() => pool.Get();

		/// <summary> 回收 </summary>
		public void Recycle() => pool.Add(this);
		#endregion

		public override void Load(AsyncLoadTask loadTask)
		{
			base.Load(loadTask);


		}
	}
}