/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/2 21:18:56
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	public interface ITypePool
	{
		/// <summary>
		/// 对象池中是否有该对象
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		bool Contains(IRecycle item);

		/// <summary>
		/// 向对象池放入一个对象
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		bool Add(IRecycle item);

		/// <summary>
		/// 从对象池取出一个对象
		/// </summary>
		/// <returns></returns>
		object Get();

		/// <summary>
		/// 清理对象池
		/// </summary>
		void Clear();

		/// <summary>
		/// 销毁对象池
		/// </summary>
		void Destroy();

		/// <summary>
		/// 最大缓存对象数
		/// </summary>
		int Maximum { get; set; }

		/// <summary>
		/// 当前缓存对象数
		/// </summary>
		int Count { get; }
	}
}