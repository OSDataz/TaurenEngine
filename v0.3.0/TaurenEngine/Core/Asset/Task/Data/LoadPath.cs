/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.1
 *│　Time    ：2021/12/19 16:43:37
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 加载地址数据
	/// </summary>
	internal class LoadPath : IRecycle
	{
		#region 对象池
		public static LoadPath Get() => SuperPool.Instance.Get<LoadPath>();

		public void Recycle() => SuperPool.Instance.Add(this);
		#endregion

		/// <summary>
		/// 加载器类型
		/// </summary>
		public int loaderType;
		/// <summary>
		/// 资源路径（Key）
		/// </summary>
		public string path;

		public bool Equals(LoadPath data)
		{
			return loaderType == data.loaderType && path == data.path;
		}

		public void Clear()
		{
			
		}

		public void Destroy()
		{
			
		}
	}
}