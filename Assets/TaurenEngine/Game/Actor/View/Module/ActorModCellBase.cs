/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 20:49:23
 *└────────────────────────┘*/

namespace TaurenEngine.Game
{
	/// <summary>
	/// 角色单个模块
	/// </summary>
	public abstract class ActorModCellBase : LoadObject
	{
		/// <summary>
		/// 模块数据
		/// </summary>
		public ActorModuleItem Data { get; private set; }

	}
}