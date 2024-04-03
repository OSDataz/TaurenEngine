/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/13 22:04:22
 *└────────────────────────┘*/

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// 角色部位项
	/// </summary>
	public class ActorPartItem
	{
		/// <summary>
		/// 部位ID
		/// </summary>
		public int id;

		private ActorModuleItem _fixedMod;
		/// <summary>
		/// 固定部位模型ID
		/// </summary>
		public ActorModuleItem FixedMod
		{
			get => _fixedMod;
			set
			{
				_fixedMod = value;
				IsFixed = value != null;
			}
		}

		/// <summary>
		/// 是否是固定模块
		/// </summary>
		public bool IsFixed { get; private set; }

		private string _skinMesh;
		/// <summary>
		/// 皮肤Mesh
		/// </summary>
		public string SkinMesh 
		{
			get => _skinMesh;
			set
			{
				_skinMesh = value;
				IsSkin = !string.IsNullOrEmpty(value);
			}
		}

		/// <summary>
		/// 是否是皮肤
		/// </summary>
		public bool IsSkin { get; private set; }
	}
}