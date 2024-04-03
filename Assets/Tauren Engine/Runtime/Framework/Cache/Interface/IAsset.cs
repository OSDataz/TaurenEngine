/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/3 20:42:51
 *└────────────────────────┘*/

namespace Tauren.Framework.Runtime
{
	public interface IAsset
	{
		/// <summary>
		/// 获取转化指定类型的资源
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="asset"></param>
		/// <returns></returns>
		bool TryGetAsset<T>(out T asset) where T : UnityEngine.Object;
	}
}