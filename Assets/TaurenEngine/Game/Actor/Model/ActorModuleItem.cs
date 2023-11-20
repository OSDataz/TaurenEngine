/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 22:05:50
 *└────────────────────────┘*/

namespace TaurenEngine.Game
{
	/// <summary>
	/// 角色模块项
	/// </summary>
	public class ActorModuleItem
	{
		/// <summary>
		/// 资源地址
		/// </summary>
		public string path;

		/// <summary>
		/// 是否是固定模块
		/// </summary>
		public bool IsFixed => false;

		/// <summary>
		/// 是否是皮肤模块
		/// </summary>
		public bool IsSkin => false;
	}
}