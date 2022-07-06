/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/14 11:12:27
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	public abstract class ResourceBase
	{
		/// <summary>
		/// 卸载释放资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="asset"></param>
		internal virtual void Unload<T>(T asset) where T : UnityEngine.Object { }
	}
}