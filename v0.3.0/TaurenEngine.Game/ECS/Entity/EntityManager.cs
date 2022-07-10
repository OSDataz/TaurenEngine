/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 9:21:08
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.ECS
{
	/// <summary>
	/// 实体管理器
	/// </summary>
	public class EntityManager : Singleton<EntityManager>
	{
		private uint _makerID = 0;

		/// <summary>
		/// 创建一个新实体
		/// </summary>
		/// <returns></returns>
		public uint CreateEntity()
		{
			return _makerID++;
		}

		/// <summary>
		/// 实体数量
		/// </summary>
		public uint count => _makerID;
	}
}